namespace Portfolio.Data.Models
{
    using Portfolio.Data.Common.Models;

    public class Town : BaseDeletableModel<string>
    {
        public string TownName { get; set; }

        public string CountryId { get; set; }

        public virtual Country Country { get; set; }
    }
}
