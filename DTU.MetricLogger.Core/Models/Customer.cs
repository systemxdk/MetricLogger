namespace DTU.MetricLogger.Core.Models
{
    /// <summary>
    /// Model describing the csutomer.
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// The customers unique id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The firstname of the customer.
        /// </summary>
        public string? Firstname { get; set; }

        /// <summary>
        /// The lastname of the customer.
        /// </summary>
        public string? Lastname { get; set; }

        /// <summary>
        /// The email of the customer.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// The created tiemstamp of the customer.
        /// </summary>
        public DateTime Created { get; set; }
    }
}
