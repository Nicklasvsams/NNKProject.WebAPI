using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NNKProject.DAL.Database.Entities
{
    public class AccountEntity
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; } = string.Empty;
        [Column(TypeName = "nvarchar(50)")]
        public string Password { get; set; } = string.Empty;
        [Column(TypeName = "nvarchar(500)")]
        public string SaveData { get; set; } = string.Empty;

    }
}
