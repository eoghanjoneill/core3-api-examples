using AutoMapper;
using CourseLibrary.API.Entities;
using CourseLibrary.API.Models;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Controllers
{
    [ApiController]
    [Route("api/authors/{authorId}/courses")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseLibraryRepository _courseLibraryRepository;
        private readonly IMapper _mapper;

        public CoursesController(ICourseLibraryRepository courseLibraryRepository,
            IMapper mapper)
        {
            _courseLibraryRepository = courseLibraryRepository ??
                throw new ArgumentNullException(nameof(courseLibraryRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public ActionResult<IEnumerable<CourseDto>> GetCoursesForAuthor(Guid authorId)
        {
            if (!_courseLibraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }
            var courses = _courseLibraryRepository.GetCourses(authorId);

            return Ok(_mapper.Map<IEnumerable<CourseDto>>(courses));
        }

        [HttpGet("{courseId}", Name = nameof(GetCourseForAuthor))]
        public ActionResult<CourseDto> GetCourseForAuthor(Guid authorId, Guid courseId)
        {
            var course = _courseLibraryRepository.GetCourse(authorId, courseId);
            if (course is null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CourseDto>(course));
        }

        [HttpPost]
        public ActionResult<CourseDto> CreateCourseForAuthor(Guid authorId, 
            CourseForCreationDto courseForCreationDto)
        {
            if (!_courseLibraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }
            var courseEntity = _mapper.Map<Entities.Course>(courseForCreationDto);
            _courseLibraryRepository.AddCourse(authorId, courseEntity);
            _courseLibraryRepository.Save();

            var courseDto = _mapper.Map<CourseDto>(courseEntity);
            return CreatedAtRoute(nameof(GetCourseForAuthor)
                , new { authorId = authorId, courseId = courseDto.Id }
                , courseDto);
        }

        [HttpPut("{courseId}")]
        public ActionResult UpdateCourseForAuthor(Guid authorId, Guid courseId, CourseForUpdateDto courseForUpdateDto)
        {
            if (!_courseLibraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }
            var courseEntity = _courseLibraryRepository.GetCourse(authorId, courseId);
            if (courseEntity == null)
            {
               //var courseToAdd = _mapper
            }

            //automapper will:
            //1) map the entity to a CourseForUpdateDto
            //2) apply the updated field values to that dto
            //3) map the CourseForUpdateDto back to an entity
            _mapper.Map(courseForUpdateDto, courseEntity);

            _courseLibraryRepository.UpdateCourse(courseEntity);
            _courseLibraryRepository.Save();

            return NoContent();
        }
    }
}
