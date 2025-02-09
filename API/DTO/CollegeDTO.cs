using System.ComponentModel.DataAnnotations;

namespace Vidya.API.DTO
{
    public class CollegeDTO
    {
        public int CollegeId { get; set; } 
        public string CollegeName { get; set; } 
        public string ShortName { get; set; } 
        public string? AuthAdd { get; set; } 
        public string? AuthLstEdt { get; set; } 
        public string? AuthDel { get; set; } 
        public DateTime? AddOnDt { get; set; } 
        public DateTime? EditOnDt { get; set; } 
        public DateTime? DelOnDt { get; set; } 
        public int? DelStatus { get; set; }

    }
}
