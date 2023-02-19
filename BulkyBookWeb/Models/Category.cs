using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyBookWeb.Models
{
    public class Category
    {
        [Key] // primary key, identity column
        public int Id { get; set; }
        [Required] // cannot be null
        public string Name { get; set; }
        [DisplayName("Display Name")]
        [Range(1, 100, ErrorMessage = "Display order must be between 1 and 100")]
        public int DisplayOrder { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}
