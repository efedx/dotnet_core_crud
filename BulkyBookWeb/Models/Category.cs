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

        //We want it to have have display name "Display Name" with a space between and also just can range from 1 to 100.
        [DisplayName("Display Order")]
        [Range(1, 100, ErrorMessage = "Display order must be between 1 and 100")]
        public int DisplayOrder { get; set; }

        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}
