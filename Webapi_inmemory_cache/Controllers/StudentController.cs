using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Webapi_inmemory_cache.BusinessLayer.Interfaces;
using Webapi_inmemory_cache.Models;

namespace Webapi_inmemory_cache.Controllers
{
    /// <summary>
    /// Controller to handle requests for student
    /// </summary>
    [Route("v1/students")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        /// <summary>
        /// Student service
        /// </summary>
        private readonly IStudentService _studentService;

        /// <summary>
        /// Initialize instance for <see cref="StudentController"/>
        /// </summary>
        public StudentController(IStudentService studentService)
            => _studentService = studentService ?? throw new ArgumentNullException(nameof(studentService));

        /// <summary>
        /// Get all students
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public Task<List<StudentDTO>> Get() => _studentService.Get();

        /// <summary>
        /// Get student by id
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("{id: int}")]
        public Task<StudentDTO> Get(int id) => _studentService.Get(id);

        /// <summary>
        /// Add new student 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public Task<int> Post(StudentDTO studentDTO) => _studentService.Post(studentDTO);

        /// <summary>
        /// Replace student
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        public Task<int> Put(StudentDTO studentDTO) => _studentService.Put(studentDTO);
    }
}