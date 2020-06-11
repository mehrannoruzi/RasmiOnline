namespace RasmiOnline.Domain.Dto
{
    public class SearchPartialModel
    {
        public string PartialViewName { get; set; }
        public object Model { get; set; }
        public string EntityName { get; set; }
        public string ActionUrl { get; set; }
    }
}
