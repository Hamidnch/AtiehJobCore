using System;
using AtiehJobCore.Common.Contracts;

namespace AtiehJobCore.Domain.Entities.Identity.Plus
{
    public class LogItem : IAuditableEntity
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string Url { get; set; }
        public string LogLevel { get; set; }
        public string Logger { get; set; }
        public string Message { get; set; }
        public string StateJson { get; set; }
        public DateTimeOffset? CreatedDateTime { get; set; }
    }
}