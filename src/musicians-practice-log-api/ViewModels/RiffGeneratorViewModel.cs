using MidiSharp;
using musicians_practice_log_api.RiffGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace musicians_practice_log_api.ViewModels
{
    public class RiffGeneratorViewModel
    {
        public int AmountToGenerate { get; set; }
        public List<int> AllowableDurations { get; set; } = new List<int>();
        public List<int> AllowableOctaves { get; set; } = new List<int>();
        public List<int> AllowablePitches { get; set; } = new List<int>();
        public GeneralMidiInstrument Instrument { get; set; }
        public int Tempo { get; set; }
        public TimeSignature Time { get; set; }
        public string TrackName { get; set; }
    }
}
