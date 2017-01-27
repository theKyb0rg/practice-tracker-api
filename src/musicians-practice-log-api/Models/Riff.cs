using PracticeLog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace musicians_practice_log_api.Models
{
    public class Riff
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FilePath { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
