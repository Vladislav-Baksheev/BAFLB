using System.ComponentModel.DataAnnotations.Schema;

namespace BAFLB
{
    /// <summary>
    /// Содержит пользователя.
    /// </summary>
    [Table("users")]
    public class User
    {
        /// <summary>
        /// Id пользователя.
        /// </summary>
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        [Column("name")]
        public string? Name { get; set; }

        /// <summary>
        /// Максимальный выстрел.
        /// </summary>
        [Column("maxShot")]
        public int MaxShot { get; set; }

        /// <summary>
        /// Текущий выстрел.
        /// </summary>
        [Column("currentShot")]
        public int CurrentShot { get; set; } = 0;

        /// <summary>
        /// Смерть пользователя.
        /// </summary>
        [Column("isDead")]
        public bool IsDead { get; set; }
    }
}
