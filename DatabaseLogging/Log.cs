using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseLogging
{
    [Table("Log")]
    public class Log
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
    }
}
