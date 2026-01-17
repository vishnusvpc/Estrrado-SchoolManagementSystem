using System.ComponentModel.DataAnnotations;

namespace Estrrado_SchoolManagementSystem.Models
{
    public class RegistrationModel
    {
        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(100, ErrorMessage = "Full Name cannot exceed 100 characters")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Password must be 6-50 characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        [Phone(ErrorMessage = "Invalid phone number")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Class is required")]
        [Range(1, 12, ErrorMessage = "Class must be between 1 and 12")]
        public int Class { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string Address { get; set; }
        public List<QualificationModel> Qualifications { get; set; } = new();
    }
}
