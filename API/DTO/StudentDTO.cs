namespace Vidya.API.DTO
{
    public class StudentDTO
    {
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
        public string? AuthAdd { get; set; } 

        public string? AuthLstEdt { get; set; } 

        public string? AuthDel { get; set; } 

        public DateTime? AddOnDt { get; set; } 

        public DateTime? EditOnDt { get; set; } 

        public DateTime? DelOnDt { get; set; } 

        public int? DelStatus { get; set; }

    }
}
