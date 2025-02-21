using EduLearnAPI;
using EduLearnAPI.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace EduLearnPortal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnrollmentController : ControllerBase
    {
        private readonly IMongoCollection<Enrollment> _enrollmentCollection;

        public EnrollmentController(EduLearnDbServices dbServices)
        {
            _enrollmentCollection = dbServices.GetEnrollmentCollection();
        }

        [HttpGet("{userId}")]
        public IActionResult GetUserEnrolledCourses(int userId)
        {
            try
            {
                if (userId < 0)
                {
                    return BadRequest(new { Message = "Invalid user ID provided." });
                }

                var enrollments = _enrollmentCollection.Find(x => x.UserId == userId).ToList();

                if (enrollments == null || !enrollments.Any())
                {
                    return NotFound(new { Message = "No enrollments found for the provided user ID." });
                }

                return Ok(enrollments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving enrollments.", Details = ex.Message });
            }
        }

        [HttpPost()]
        public IActionResult EnrollCourses(Enrollment enrollment)
        {
            try
            {
                if (enrollment.UserId < 0)
                {
                    return BadRequest(new { Message = "Invalid user ID provided." });
                }

                var userEnrollments = _enrollmentCollection.Find(x => x.UserId == enrollment.UserId).ToList();
                var userCourseEnrollments = userEnrollments.Find(x => x.CourseId == enrollment.CourseId);

                if (userCourseEnrollments == null)
                {
                    _enrollmentCollection.InsertOne(enrollment);
                    return Ok("Course enrolled successfully!");
                }
                return Ok("Course already enrolled!");
                
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while enrolling.", Details = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetEnrollmentsCount()
        {
            var count = _enrollmentCollection.CountDocuments(FilterDefinition<Enrollment>.Empty);
            return Ok(count);
        }
    }
}
