
namespace Fluentx
{
    public enum SequentialGuidType
    {
        /// <summary>
        /// Useful for MySQL and Postgre
        /// </summary>
        SequentialAsString,
        /// <summary>
        /// Useful for Oracle database
        /// </summary>
        SequentialAsBinary,
        /// <summary>
        /// Useful for SQL Server Based Guids as it has its own way of sorting the Guids
        /// </summary>
        SequentialAtEnd
    }
}
