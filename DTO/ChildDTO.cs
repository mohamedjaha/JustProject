using System.ComponentModel.DataAnnotations;

namespace FamilyDataCollector.DTO
{
    public class ChildDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Age { get; set; }
        public string EductionLevel { get; set; }
    }
}
