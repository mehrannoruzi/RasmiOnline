namespace RasmiOnline.Domain.Dto
{
    public class ViewsFullPathModel
    {
        public int ViewId { get; set; }
        public int ParentId { get; set; }
        public string FullPath { get; set; }
    }
}