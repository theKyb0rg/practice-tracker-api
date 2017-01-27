using musicians_practice_log_api.Models;
using System.Collections.Generic;

namespace PracticeLog.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Instrument> Instruments { get; set; } = new HashSet<Instrument>();
        public virtual ICollection<Riff> Riffs { get; set; } = new HashSet<Riff>();
    }
}
