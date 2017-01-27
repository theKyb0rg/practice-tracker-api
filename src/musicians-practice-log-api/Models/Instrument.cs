using System.Collections.Generic;

namespace PracticeLog.Models
{
    public class Instrument
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int ExperienceId { get; set; }
        public virtual Experience Experience { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
    }
}
