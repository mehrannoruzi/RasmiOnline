namespace RasmiOnline.Business.Implement
{
    using System;
    using System.Linq;
    using System.Data;
    using System.Data.Entity;
    using Gnu.Framework.Core;
    using RasmiOnline.Domain.Dto;
    using RasmiOnline.Domain.Entity;
    using RasmiOnline.Business.Protocol;
    using System.Collections.Generic;
    using RasmiOnline.Business.Properties;
    using Gnu.Framework.EntityFramework;
    using Gnu.Framework.EntityFramework.DataAccess;

    public class ViewBusiness : IViewBusiness
    {
        #region Constructor
        readonly IUnitOfWork _uow;
        readonly IDbSet<View> _view;

        public ViewBusiness(IUnitOfWork uow)
        {
            _uow = uow;
            _view = _uow.Set<View>();
        }
        #endregion

        public IActionResponse<int> Insert(View model)
        {
            model.ExpireDateMi = PersianDateTime.Parse(model.ExpireDateSh).ToDateTime();
            _view.Add(model);
            var result = _uow.SaveChanges();
            return new ActionResponse<int>
            {
                IsSuccessful = result.ToSaveChangeResult(),
                Message = result.ToSaveChangeMessageResult(BusinessMessage.Success, BusinessMessage.Error),
                Result = model.ViewId
            };
        }

        public IActionResponse<int> Update(View model)
        {
            model.ExpireDateMi = PersianDateTime.Parse(model.ExpireDateSh).ToDateTime();

            _view.Attach(model);
            _uow.Entry(model).State = EntityState.Modified;
            var result = _uow.SaveChanges();

            return new ActionResponse<int>
            {
                IsSuccessful = result.ToSaveChangeResult(),
                Message = result.ToSaveChangeMessageResult(BusinessMessage.Success, BusinessMessage.Error),
                Result = model.ViewId
            };
        }

        public IActionResponse<bool> Delete(int viewId)
        {
            var response = new ActionResponse<bool>();
            var view = Find(viewId);
            if (view == null)
            {
                response.Message = BusinessMessage.RecordNotFound;
                return response;
            }

            _uow.Set<View>().Remove(view);
            var result = _uow.SaveChanges();

            response.Message = result.ToSaveChangeMessageResult(BusinessMessage.Success, BusinessMessage.Error);
            response.IsSuccessful = result.ToSaveChangeResult();
            response.Result = response.IsSuccessful;
            return response;
        }

        public View Find(int viewId) => _view.Find(viewId);

        public IActionResponse<View> GetView(int viewId)
        {
            var response = new ActionResponse<View>();
            var result = _view.Find(viewId);
            if (result.IsNotNull())
            {
                response.IsSuccessful = true;
                response.Result = result;
            }
            else response.Message = BusinessMessage.RecordNotFound;
            return response;
        }

        public IActionResponse<List<View>> Search(FilterViewModel filterModel)
        {
            var response = new ActionResponse<List<View>>();
            var result = _view.Where(x =>
                               (!string.IsNullOrEmpty(filterModel.ActionName) ? x.ActionName.Contains(filterModel.ActionName) : true) &&
                               (!string.IsNullOrEmpty(filterModel.ActionNameFa) ? x.ActionNameFa.Contains(filterModel.ActionNameFa) : true) &&
                               (!string.IsNullOrEmpty(filterModel.ControllerName) ? x.Controller.Contains(filterModel.ControllerName) : true) &&
                               (filterModel.IsVisible != null ? x.IsVisible == filterModel.IsVisible : true) &&
                               (filterModel.ParentId != null ? x.ParentId == filterModel.ParentId : true)
                         )
                         .OrderBy(x => x.ViewId)
                         .Select(s => s)
                         .Take(filterModel.ItemsCount)
                         .ToList();

            if (result.Any())
                response.IsSuccessful = true;
            else
                response.Message = BusinessMessage.UserNotFound;

            response.Result = result;
            return response;
        }

        public IList<ItemTextValueModel<string, int>> GetParentViews()
        {
            var dateNow = DateTime.Now.Date;
            return _view.Where(x => x.ParentId == 0 && x.IsVisible && x.ExpireDateMi >= dateNow)
                        .Select(x => new ItemTextValueModel<string, int>
                        {
                            Key = x.ActionNameFa,
                            Value = x.ViewId,
                        }).OrderBy(x => x.Key)
                        .ToList();
         }
            
    }
}