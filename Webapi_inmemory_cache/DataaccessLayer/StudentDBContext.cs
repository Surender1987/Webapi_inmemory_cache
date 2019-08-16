using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webapi_inmemory_cache.DataaccessLayer.Models;

namespace Webapi_inmemory_cache.DataaccessLayer
{
    /// <summary>
    /// Student db context
    /// </summary>
    public class StudentDBContext : DbContext
    {
        public StudentDBContext()
        {

        }
        public StudentDBContext(DbContextOptions<StudentDBContext> option)
            :base(option)
        { }

        public DbSet<Student> Students { get; set; }
    }
}
