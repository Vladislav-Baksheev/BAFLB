using System.ComponentModel.DataAnnotations.Schema;

namespace BAFLB
{
    [Table("round")]
    public class Round
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }
    }
}
