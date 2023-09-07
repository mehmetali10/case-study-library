using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace case_study_library.Data
{
    [Table("table_Book", Schema = "public")]
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("BookName")]
        public string BookName { get; set; }

        [Required]
        [Column("AboutBook")]
        public string AboutBook { get; set; }

        [Required]
        [Column("Author")]
        public string Author { get; set; }

        [Required]
        [Column("Publisher")]
        public string Publisher { get; set; }

        [Column("PublicationYear")]
        public DateTime? PublicationYear { get; set; }

        [Column("IsAvaliable")]
        public bool? IsAvaliable { get; set; }

        [Required]
        [Column("CreatedDate")]
        public DateTime CreatedDate { get; set; }

        [Column("IsDeleted")]
        public bool? IsDeleted { get; set; }

        [Required]
        [Column("ImageLink")]
        public string ImageLink { get; set; }
    }
}
