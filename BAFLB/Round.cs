using System.ComponentModel.DataAnnotations.Schema;

namespace BAFLB
{
    /// <summary>
    /// Содержит раунд.
    /// </summary>
    [Table("round")]
    public class Round
    {
        /// <summary>
        /// Id раунда.
        /// </summary>
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// Название раунда.
        /// </summary>
        [Column("name")]
        public string Name { get; set; }
    }
}
