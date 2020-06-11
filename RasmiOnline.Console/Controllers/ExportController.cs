namespace RasmiOnline.Console.Controllers
{
    using System;
    using System.IO;
    using System.Text;
    using System.Linq;
    using System.Web.Mvc;
    using Gnu.Framework.Core;
    using RasmiOnline.Domain.Dto;
    using RasmiOnline.Domain.Enum;
    using System.ComponentModel;
    using System.Collections.Generic;
    using RasmiOnline.Console.Properties;
   

    public partial class ExportController : Controller
    {
        public ExportController()
        {

        }

        #region Private Methods
        [NonAction]
        private void GetTabelName(string tabelName = nameof(LocalMessage.PleaseSelect))
        {
            var list = new List<ItemTextValueModel<string, string>>();
            list.Add(new ItemTextValueModel<string, string> { Key = LocalMessage.PleaseSelect, Value = nameof(LocalMessage.PleaseSelect) });

            var result = EnumConvertor.GetEnumElements<EntityName>().ToList();
            result.ForEach(item =>
            {
                list.Add(new ItemTextValueModel<string, string>() { Key = ((EntityName)Enum.Parse(typeof(EntityName), item.Name)).GetLocalizeDescription(), Value = item.Name });
            });

            ViewBag.TabelsName = list.Select(s => new SelectListItem
            {
                Text = s.Key,
                Value = s.Value.ToString(),
                Selected = s.Value == tabelName
            }).ToList();
        }

        private void WriteTsv<T>(IEnumerable<T> data, TextWriter output)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            foreach (PropertyDescriptor prop in props)
            {
                output.Write(prop.DisplayName);
                output.Write("\t");
            }
            output.WriteLine();
            foreach (T item in data)
            {
                foreach (PropertyDescriptor prop in props)
                {
                    output.Write(prop.Converter.ConvertToString(
                         prop.GetValue(item)));
                    output.Write("\t");
                }
                output.WriteLine();
            }
        }
        #endregion

        [HttpGet]
        public virtual ActionResult ToExcel()
        {
            GetTabelName();
            return View();
        }




        [HttpPost]
        public virtual void ToExcel(EntityName entityName, string dateTimeFrom, string dateTimeTo)
        {
            try
            {
                var result = ExportToExcelFactory.DownloadExcelFile(entityName, dateTimeFrom, dateTimeTo);

                if (!result.IsSuccessful) throw new Exception(result.Message);

                var currentDate = PersianDateTime.Now;
                string fileName = $"{entityName.ToString()}_{currentDate.Year.ToString()}{currentDate.Month.ToString()}{currentDate.DaysInMonth.ToString()}_{currentDate.Hour}{currentDate.Minute}{currentDate.Second}";
                Response.Clear();
                Response.AddHeader("content-disposition", $"attachment;filename={fileName}.xls");
                Response.AddHeader("Content-Type", "application/vnd.ms-excel");
                Response.ContentEncoding = Encoding.Unicode;
                Response.BinaryWrite(Encoding.Unicode.GetPreamble());

                switch (entityName)
                {
                    case EntityName.User:
                        WriteTsv((IEnumerable<ExportUserToExcelModel>)result.Result.FileResult, Response.Output);
                        break;
                    case EntityName.Order:
                        WriteTsv((IEnumerable<ExportOrderToExcelModel>)result.Result.FileResult, Response.Output);
                        break;
                }

            }
            catch {
                Response.Redirect(Url.Action(MVC.Export.ActionNames.ToExcel, MVC.Export.Name));
            }
            finally {
                Response.End();
            }
        }
    }
}