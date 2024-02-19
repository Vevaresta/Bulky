using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bulky.Models
{
	public class Category
	{
        [Key]
		public int Id { get; set; }
        [Required]
        [MaxLength(30)] // Data annotation
        [DisplayName("Category Name")]
        public string Name { get; set; }
        [DisplayName("Display Name")]
        [Range(1,100, ErrorMessage ="Display Order must be between 1-100")] // Data annotation
        public int DisplayOrder { get; set; }
    }
}
