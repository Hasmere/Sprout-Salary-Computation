using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sprout.Exam.WebApp.Models
{
    [Table("Employee")]
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string FullName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }

        [Required]
        [MaxLength(15)]
        public string Tin { get; set; }

        [Required]
        public int EmployeeTypeId { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
    }
}
