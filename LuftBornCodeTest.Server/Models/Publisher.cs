using System.ComponentModel.DataAnnotations.Schema;

namespace LuftBornCodeTest.Server.Models
{
    public class Publisher
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte Id { get; set; }


        [MaxLength(100)]
        public string Name { get; set; }

        


    }
}
