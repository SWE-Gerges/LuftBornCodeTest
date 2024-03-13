using System.ComponentModel.DataAnnotations.Schema;

namespace LuftBornCodeTest.Server.Models
{
    public class Movie
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(2500)]
        public string Description { get; set; }

        public string Category { get; set; }
       

    }
}
