using FamilyDataCollector.Data.Models;
using System.ComponentModel.DataAnnotations;

public class Family
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int FatherId { get; set; }

    [Required]
    public int MotherId { get; set; }

    // Navigation properties
    public Father Father { get; set; }
    public Mother Mother { get; set; }
    public ICollection<Child>? Children { get; set; } = new List<Child>();
}
