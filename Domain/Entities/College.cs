using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vidya.Domain.Entities
{
    [Table("College")] // Maps to the actual table name in the database
    public class College
    {
        [Key] // Defines UserId as the primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CollegeId { get; set; } // Unique College ID

        [Required]
        [StringLength(500)]
        public string CollegeName { get; set; } // Name of the College
     
        [StringLength(500)]
        public string ShortName { get; set; } // Name of the ShortName

        public string? AuthAdd { get; set; } // Nullable if not all users have add permissions

        public string? AuthLstEdt { get; set; } // Nullable for last edit permissions

        public string? AuthDel { get; set; } // Nullable for delete permissions

        public DateTime? AddOnDt { get; set; } // Nullable if date is not always provided

        public DateTime? EditOnDt { get; set; } // Nullable if not always edited

        public DateTime? DelOnDt { get; set; } // Nullable if not always deleted

        public int? DelStatus { get; set; } // Nullable if delete status isn't always tracked


    }
}
