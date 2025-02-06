using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Vidya.Domain.Entities
{
    [Table("student")] // Maps to the actual table name in the database
    public class Student
    {
        [Key] // Defines UserId as the primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? FathersName { get; set; }
        public string? MothersName { get; set; }
        public DateTime DOB { get; set; }
        public string? BloodGrp { get; set; }
        public string Gender { get; set; }
        public string? Adhaar { get; set; }
        public string? PAN { get; set; }
        public string? EmailId { get; set; }
        public string? Mobile { get; set; }
        public string? FathersMobile { get; set; }
        public string? MothersMobile { get; set; }
        public string? FathersEmailId { get; set; }
        public string? MothersEmailId { get; set; }
        public string? Caste { get; set; }
        public string? Religion { get; set; }
        public int PhysicalDisability { get; set; }

        public string? Address { get; set; }
        public string? City { get; set; }
        public int? PINCode { get; set; }
        public string? States { get; set; }

        public string? Per_Address { get; set; }
        public string? Per_City { get; set; }
        public int? Per_PINCode { get; set; }
        public string? Per_States { get; set; }
        public string? Country { get; set; }
        public string? AuthAdd { get; set; } // Nullable if not all users have add permissions

        public string? AuthLstEdt { get; set; } // Nullable for last edit permissions

        public string? AuthDel { get; set; } // Nullable for delete permissions

        public DateTime? AddOnDt { get; set; } // Nullable if date is not always provided

        public DateTime? EditOnDt { get; set; } // Nullable if not always edited

        public DateTime? DelOnDt { get; set; } // Nullable if not always deleted

        public int? DelStatus { get; set; } // Nullable if delete status isn't always tracked

    }
}
