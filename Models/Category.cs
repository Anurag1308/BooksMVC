using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BooksMVC.Models
{
    public class Category
    {
        [Key] //primary key
        public int Id { get; set; }

        [Required]
        [DisplayName("Category Name")]
        public string Name { get; set; }

        [DisplayName("Display Order")]
        [Range(1, 100, ErrorMessage = "Display Order must be between 1-100")]
        public int DisplayOrder { get; set; }

        [DisplayName("Status")]
        public string Status { get; set; } = "Active";
    }
}