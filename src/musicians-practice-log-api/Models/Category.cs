using System.Collections.Generic;

namespace PracticeLog.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int InstrumentId { get; set; }
        public virtual Instrument Instrument { get; set; }

        public virtual ICollection<Exercise> Exercises { get; set; } = new HashSet<Exercise>();
    }
}
