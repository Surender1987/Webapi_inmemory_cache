using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        /// Initialize instance for <see cref="StudentController"/>
        /// </summary>
        public StudentController()
        {

        }
    }
}