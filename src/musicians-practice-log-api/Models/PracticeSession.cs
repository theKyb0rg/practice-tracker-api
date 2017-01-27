using System;

namespace PracticeLog.Models
{
    public class PracticeSession
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public int Tempo { get; set; }
        public int Time { get; set; }
        public string Comment { get; set; }

        public int ExerciseId { get; set; }
        public virtual Exercise Exercise { get; set; }
    }
}
