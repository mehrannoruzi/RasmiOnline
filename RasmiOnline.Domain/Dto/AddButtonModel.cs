namespace RasmiOnline.Domain.Dto
{
    public class AddButtonModel
    {
        bool _isDisableModalSaveButton;
        public AddButtonModel(bool isDisableModalSaveButton = false)
        {
            _isDisableModalSaveButton = isDisableModalSaveButton;
        }

        public string ModalTitle { get; set; }
        public string EntityName { get; set; }
        public string FormUrl { get; set; }
        public string Title { get; set; }
        public bool IsDisableAddButton { get; set; } = false;
        public CustomModalModel ModalConfig => new CustomModalModel { EntityName = EntityName, IsDisableSaveButton = _isDisableModalSaveButton };
    }
}