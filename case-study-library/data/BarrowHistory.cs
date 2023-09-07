using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace case_study_library.data
{
    [Table("Table_BarrowHistory", Schema = "public")]
    public class BarrowHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("BookId")]
        public int BookId { get; set; }

        [Required]
        [Column("PersonName")]
        public string PersonName { get; set; }

        [Required]
        [Column("PersonSurname")]
        public string PersonSurname { get; set; }

        [Required]
        [Column("BarrowStartDate")]
        public DateTime BarrowStartDate { get; set; }

        [Required]
        [Column("BarrowEndDate")]
        public DateTime BarrowEndDate { get; set; }

        [Column("IsDeleted")]
        public bool? IsDeleted { get; set; }

        [ForeignKey("BookId")]
        public Book Book { get; set; }
    }
}