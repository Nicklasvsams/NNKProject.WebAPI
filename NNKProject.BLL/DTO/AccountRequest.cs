using System.ComponentModel.DataAnnotations;

namespace NNKProject.BLL.DTO
{
    public class AccountRequest
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [StringLength(50)]
        public string Password { get; set; } = string.Empty;
        [StringLength(500)]
        public string SaveData { get; set; } = "UNDEFINED";
    }
}
