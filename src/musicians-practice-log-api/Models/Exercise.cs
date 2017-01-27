﻿using System.Collections.Generic;

namespace PracticeLog.Models
{
    public class Exercise
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public virtual ICollection<PracticeSession> PracticeSessions { get; set; } = new HashSet<PracticeSession>();
    }
}
