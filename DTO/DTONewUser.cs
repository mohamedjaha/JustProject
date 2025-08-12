using System.ComponentModel.DataAnnotations;

namespace FamilyDataCollector.DTO
{
    public class DTONewUser
    {
        [Required]
        public string userName {  get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string email { get; set; }
        public string? phoneNumber { get; set; }
        [Required]
        [RegularExpression("^(Collector|Admin)$", ErrorMessage = "Role must be either 'Collector' or 'Admin'.")]
        public string role { get; set; }
    }
}
