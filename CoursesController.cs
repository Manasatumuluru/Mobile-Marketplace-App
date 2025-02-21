using EduLearnAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace EduLearnAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly IMongoCollection<Course> _courseCollection;

        public CoursesController(EduLearnDbServices dbServices)
        {
            _courseCollection = dbServices.GetCourseCollection();
        }

        [HttpGet]
        public IActionResult GetCourses()
        {
            try
            {
                var courses = _courseCollection.Find(_ => true).ToList();
                return Ok(courses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving courses.", Details = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetCourseById(int id)
        {
            try
            {
                var course = _courseCollection.Find(x => x.Id == id).FirstOrDefault();

                if (course == null)
                {
                    return NotFound(new { Message = $"Course with ID {id} not found." });
                }

                return Ok(course);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving the course.", Details = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult AddCourse([FromBody] Course course)
        {
            try
            {
                if (course == null || string.IsNullOrEmpty(course.Title))
                {
                    return BadRequest(new { Message = "Invalid course data provided." });
                }

                _courseCollection.InsertOne(course);
                return CreatedAtAction(nameof(GetCourseById), new { id = course.Id }, course);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while adding the course.", Details = ex.Message });
            }
        }
    }
}
