using System.ComponentModel.DataAnnotations;

namespace FamilyDataCollector.DTO
{
    public class DTOLogIn
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
