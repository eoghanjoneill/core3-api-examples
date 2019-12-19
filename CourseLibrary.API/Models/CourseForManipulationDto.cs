using CourseLibrary.API.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Models
{
    [CourseTitleMustBeDifferentFromDescription(ErrorMessage = "title != desc")]
    public abstract class CourseForManipulationDto
    {
        [Required(ErrorMessage = "A course cannot have an empty title, otherwise how would people find it?")]
        [MaxLength(100)]
        public string Title { get; set; }
        [MaxLength(1500)]
        public virtual string Description { get; set; }
    }
}
