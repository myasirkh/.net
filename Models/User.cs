using System.ComponentModel.DataAnnotations;

namespace TempEmbeddin2302C2.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string username { get; set; }

        [RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$")]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
    }
}
