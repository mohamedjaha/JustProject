using System.ComponentModel.DataAnnotations;

namespace FamilyDataCollector.Data.Models
{
    public abstract class Person
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Age { get; set; }
    }
}
