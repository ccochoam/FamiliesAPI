using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace FamiliesAPI.Entities.DTOs
{
    public class UserDTO : IValidatableObject
    {
        public long UserId { get; private set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Invalid Email Address")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression("^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*[!@#$%^&*()_+])[A-Za-z\\d!@#$%^&*()_+]{8,}$",
            ErrorMessage = "The Password must contain at least one uppercase letter, one lowercase letter, one number, one special character, and must not be less than 8 characters.")]
        [SwaggerSchema(Description = "")]
        public string Password { private get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "NumberId is required")]
        public string NumberId { get; set; }
        public int GenderId { get; set; }
        public string Relationship { get; set; }
        public int Age { get; set; }
        public bool UnderAge { get; private set; }
        public DateOnly Birthdate { get; set; }
        public int FamilyGroupId { get; set; }

        public string GetPassword()
        {
            return Password;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Age < 18)
            {
                UnderAge = true;
                var date = DateOnly.FromDateTime(DateTime.Now);
                if (Birthdate == date)
                    yield return new ValidationResult("Birthdate is required.", new[] { nameof(Birthdate) });
            }
        }
    }
}
