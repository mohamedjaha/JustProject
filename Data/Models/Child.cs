namespace FamilyDataCollector.Data.Models
{
    public class Child : Person
    {
        public string EducationLevel { get; set; }

        // Navigation property
        public Family Family { get; set; }
    }

}
