using System;

namespace Evidences.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public DateTimeOffset Added { get; set; }
    }
}