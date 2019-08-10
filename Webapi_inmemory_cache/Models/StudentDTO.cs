namespace Webapi_inmemory_cache.Models
{
    /// <summary>
    /// Data transfer model for student
    /// </summary>
    public class StudentDTO
    {
        /// <summary>
        /// Get or set for student id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Get or set for first name
        /// </summary>
        public string FirstName{ get; set; }

        /// <summary>
        /// Get or set for first name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Get or set for first name
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// Get or set for father name
        /// </summary>
        public string FatherName { get; set; }

        /// <summary>
        /// Get or set for father name
        /// </summary>
        public string MotherName { get; set; }

        /// <summary>
        /// Get or set for first name
        /// </summary>
        public AddressDTO Address { get; set; }
    }
}
