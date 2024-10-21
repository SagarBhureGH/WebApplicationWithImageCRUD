using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication_WithImage.Models
{
    public class DetailsModal
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Name  { get; set; }
        public string DocumentName { get; set; }
        public string Path { get; set; }

        [NotMapped]
        [Display(Name ="Choose Image")]
        public IFormFile ImagePath { get; set; }

    }
}
