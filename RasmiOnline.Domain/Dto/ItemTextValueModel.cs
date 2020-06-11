namespace RasmiOnline.Domain.Dto
{
    public class ItemTextValueModel<TText, TValue>
    {
        public TText Key { get; set; }
        public TValue Value { get; set; }
        public object CustomeValue { get; set; }
    }
}