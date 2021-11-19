using System;
using System.Collections.Generic;

namespace StudyAssignmentManager.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public String FullName { get; set; }
        public List<Guid> Roles { get; set; }
    }
}