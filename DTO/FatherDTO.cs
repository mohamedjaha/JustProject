using System.ComponentModel.DataAnnotations;

namespace FamilyDataCollector.DTO
{
    public class FatherDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Age { get; set; }
        public string Work { get; set; }
    }
}
