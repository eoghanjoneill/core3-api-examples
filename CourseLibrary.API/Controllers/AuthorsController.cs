using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseLibrary.API.Models;
using CourseLibrary.API.Services;
using CourseLibrary.API.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseLibrary.API.Controllers
{
    [ApiController]
    [Route("api/authors")]
    public class AuthorsController : ControllerBase
    {
        private readonly ICourseLibraryRepository _courseLibraryRepository;

        public AuthorsController(ICourseLibraryRepository courseLibraryRepository)
        {
            _courseLibraryRepository = courseLibraryRepository ??
                    throw new ArgumentNullException(nameof(courseLibraryRepository));
        }

        [HttpGet()]
        public IActionResult GetAuthors()
        {
            var authorsFromRepo = _courseLibraryRepository.GetAuthors();
            var authors = new List<AuthorDto>();

            foreach (var author in authorsFromRepo)
            {
                authors.Add(new AuthorDto()
                {
                    Id = author.Id,
                    Name = $"{author.FirstName} {author.LastName}",
                    MainCategory = author.MainCategory,
                    Age = author.DateOfBirth.GetCurrentAge(DateTimeOffset.Now)
                });
            }
            return Ok(authors);
        }

        //public IActionResult GetAuthors()

        [HttpGet("{authorId}")]
        public IActionResult GetAuthor(Guid authorId)
        {
            var authorFromRepo = _courseLibraryRepository.GetAuthor(authorId);
            if (authorFromRepo == null)
            {
                return NotFound();
            }
            return Ok(authorFromRepo);
        }

        [HttpGet("HiThere")]
        public IActionResult HiThere() =>
            Ok(new[]
                {
                    "yo",
                    "dude"
                });        
    }
}