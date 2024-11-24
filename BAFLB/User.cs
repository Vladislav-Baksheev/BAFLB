using System.ComponentModel.DataAnnotations.Schema;

namespace BAFLB
{
    [Table("users")]
    public class User
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string? Name { get; set; }

        [Column("maxShot")]
        public int MaxShot { get; set; }

        [Column("currentShot")]
        public int CurrentShot { get; set; } = 0;

        [Column("isDead")]
        public bool IsDead { get; set; }
    }
}
