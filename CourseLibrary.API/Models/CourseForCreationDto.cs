using CourseLibrary.API.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Models
{
    [CourseTitleMustBeDifferentFromDescription(ErrorMessage = "title != desc")]
    public class CourseForCreationDto // : IValidatableObject
    {
        [Required(ErrorMessage = "A course cannot have an empty title, otherwise how would people find it?")]
        [MaxLength(100)]
        public string Title { get; set; }
        [MaxLength(1500)]
        public string Description { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (Title == Description)
        //    {
        //        yield return new ValidationResult(
        //            "The Description should be different from the Title.",
        //            new[] { nameof(CourseForCreationDto) });
        //    }
        //}
    }
}
