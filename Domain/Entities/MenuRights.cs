using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vidya.Domain.Entities
{
    [Table("tblUserRights")] // Maps to the actual table name in the database
    public class MenuRights
    {
        [Key] // Defines UserId as the primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDCode { get; set; }

        public int MenuID { get; set; } // Foreign key to MenuItem table

        public int UserId { get; set; } // Foreign key to Users table

        public string? AuthAdd { get; set; } // Nullable if not all users have add permissions

        public string? AuthLstEdt { get; set; } // Nullable for last edit permissions

        public string? AuthDel { get; set; } // Nullable for delete permissions

        public DateTime? AddOnDt { get; set; } // Nullable if date is not always provided

        public DateTime? EditOnDt { get; set; } // Nullable if not always edited

        public DateTime? DelOnDt { get; set; } // Nullable if not always deleted

        public int? DelStatus { get; set; } // Nullable if delete status isn't always tracked
    }
}
