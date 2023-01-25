using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace OPTC_API.Models
{
    public class Character
    {
        [Required]
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Type { get; set; }
        public string Class1 { get; set; }
        public string? Class2 { get; set; }
        public int Cost { get; set; }
        public int Stars { get; set; }
        public string Image { get; set; }

    }
}
