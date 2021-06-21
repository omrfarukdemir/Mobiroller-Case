namespace Mobiroller.Data.Entity
{
    public class Incident : BaseEntity<int>
    {
        public int Order { get; set; }
        public string Time { get; set; }
        public string Category { get; set; }
        public string Event { get; set; }
        public int LocaleId { get; set; } = 1;
        public virtual Locale Locale { get; set; }
    }
}