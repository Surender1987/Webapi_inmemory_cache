using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webapi_inmemory_cache.Models;

namespace Webapi_inmemory_cache.BusinessLayer.Interfaces
{
    /// <summary>
    /// Student service
    /// </summary>
    public interface IStudentService
    {
        /// <summary>
        /// Get all students
        /// </summary>
        /// <returns></returns>
        Task<List<StudentDTO>> Get();

        /// <summary>
        /// Replace entity
        /// </summary>
        /// <returns></returns>
        Task<int> Put(StudentDTO student);

        /// <summary>
        /// Get student by id
        /// </summary>
        /// <returns></returns>
        Task<StudentDTO> Get(int id);

        /// <summary>
        /// Add new entity
        /// </summary>
        /// <returns></returns>
        Task<int> Post(StudentDTO student);
    }
}
