namespace FamilyDataCollector.Data.Models
{
    public class Mother : Person
    {
        public string? Work { get; set; }

        // Navigation property
        public Family Family { get; set; }
    }

}
