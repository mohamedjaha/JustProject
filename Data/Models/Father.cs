namespace FamilyDataCollector.Data.Models
{
    public class Father : Person
    {
        public string Work { get; set; }

        // Navigation property
        public Family Family { get; set; }
    }

}
