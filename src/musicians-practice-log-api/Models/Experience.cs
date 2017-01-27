namespace PracticeLog.Models
{
    public class Experience
    {
        public int Id { get; set; }
        public int Points { get; set; }

        public int InstrumentId { get; set; }
        public virtual Instrument Instrument { get; set; }
    }
}
