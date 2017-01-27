using MidiSharp;
using MidiSharp.Events;
using MidiSharp.Events.Meta;
using MidiSharp.Events.Meta.Text;
using MidiSharp.Events.Voice;
using MidiSharp.Events.Voice.Note;
using musicians_practice_log_api.Generator;
using musicians_practice_log_api.RiffGenerator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace musicians_practice_log_api.RiffGenerator
{
    public static class RiffGenerator
    {
        //http://www.deluge.co/?q=midi-tempo-bpm
        /// <summary>
        /// Converts tempo into the value the library accepts as a tempo.
        /// </summary>
        /// <param name="tempo"></param>
        /// <returns></returns>
        public static int ConvertToTempo(int tempo)
        {
            return 60000000 / tempo;
        }

        /// <summary>
        /// Sets the tempo.
        /// </summary>
        /// <param name="tempo"></param>
        /// <returns></returns>
        public static TempoMetaMidiEvent SetTempo(int tempo)
        {
            return new TempoMetaMidiEvent(0, ConvertToTempo(tempo));
        }

        /// <summary>
        /// Sets time signature.
        /// </summary>
        /// <param name="time">Use the numerator to set the time signature.</param>
        /// <returns></returns>
        public static TimeSignatureMetaMidiEvent SetTimeSignature(TimeSignature time)
        {
            return new TimeSignatureMetaMidiEvent(0, time.Numerator, time.Denominator, 24, 8);
        }

        /// <summary>
        /// Sets instrument.
        /// </summary>
        /// <param name="instrument"></param>
        /// <returns></returns>
        public static ProgramChangeVoiceMidiEvent SetInstrument(GeneralMidiInstrument instrument)
        {
            return new ProgramChangeVoiceMidiEvent(0, 0, instrument);
        }

        /// <summary>
        /// Sets the end of the track.
        /// </summary>
        /// <returns></returns>
        public static EndOfTrackMetaMidiEvent SetEndOfTrack()
        {
            return new EndOfTrackMetaMidiEvent(0);
        }

        /// <summary>
        /// Sets the name of the track.
        /// </summary>
        /// <param name="trackName"></param>
        /// <returns></returns>
        public static SequenceTrackNameTextMetaMidiEvent SetTrackName(string trackName)
        {
            return new SequenceTrackNameTextMetaMidiEvent(0, trackName);
        }

        /// <summary>
        /// Creates the MIDI sequence.
        /// </summary>
        /// <param name="tracks"></param>
        /// <returns></returns>
        public static MidiSequence CreateSequence(List<MidiTrack> tracks)
        {
            MidiSequence sequence = new MidiSequence(MidiSharp.Format.One, 480);

            foreach (var track in tracks)
            {
                sequence.Tracks.Add(track);
            }

            return sequence;
        }

        /// <summary>
        /// Creates the midi track to add to the sequence.
        /// </summary>
        /// <param name="trackName"></param>
        /// <param name="instrument"></param>
        /// <param name="tempo"></param>
        /// <param name="time"></param>
        /// <param name="noteEvents"></param>
        /// <returns></returns>
        private static MidiTrack CreateTrack(string trackName, GeneralMidiInstrument instrument, int tempo, TimeSignature time, List<MidiEvent> noteEvents)
        {
            MidiTrack track = new MidiTrack();
            MidiEventCollection events = track.Events;

            // Set the track name
            events.Add(SetTrackName(trackName));

            // Set the instrument
            events.Add(SetInstrument(instrument));

            // Set the tempo
            events.Add(SetTempo(tempo));

            // Set the time signature
            events.Add(SetTimeSignature(time));

            // add on off note events to track
            foreach (var note in noteEvents)
            {
                events.Add(note);
            }

            // Set the end of track flag
            events.Add(SetEndOfTrack());

            return track;
        }

        /// <summary>
        /// Completely random.
        /// </summary>
        /// <param name="trackName"></param>
        /// <param name="amountToGenerate"></param>
        /// <returns></returns>
        public static MidiSequence Randomize(string trackName, int amountToGenerate, GeneralMidiInstrument instrument)
        {
            try
            {
                // create list of tracks
                List<MidiTrack> tracks = new List<MidiTrack>();

                // randomized values for notes
                Random random = new Random();

                // event list for on and off notes
                List<MidiEvent> events = new List<MidiEvent>();

                for (int i = 0; i < amountToGenerate; i++)
                {
                    // create the not and set the pitch and octave
                    Note note = new Note();
                    note.Octave = random.Next(1, 8);
                    note.Pitch = note.Pitches[random.Next(0, 11)];
                    note.Duration = note.Durations[random.Next(0, 13)];

                    // create the note on and off from properties inside class
                    OnNoteVoiceMidiEvent on = note.CreateNoteOn();
                    OffNoteVoiceMidiEvent off = note.CreateNoteOff();

                    // add the events to the midievent list
                    events.Add(on);
                    events.Add(off);
                }

                // list of denoms
                int[] denominators = { 1, 2, 4, 8, 16, 32 };
                // create track with the randomly generate midi events
                MidiTrack track = CreateTrack(trackName, instrument, random.Next(30, 300), new TimeSignature((byte)random.Next(1, 32), denominators[random.Next(0, 5)]), events);

                // add track to list
                tracks.Add(track);

                // create sequence
                MidiSequence midi = CreateSequence(tracks);

                return midi;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public static MidiSequence Generate(List<int> allowableOctaves, List<int> allowablePitches, List<int> allowableDurations, string trackName, int amountToGenerate, GeneralMidiInstrument instrument, TimeSignature time, int tempo)
        {
            try
            {
                // create list of tracks
                List<MidiTrack> tracks = new List<MidiTrack>();

                // randomized values for notes
                Random octaveRandom = new Random();
                Random pitchRandom = new Random();
                Random durationRandom = new Random();

                // event list for on and off notes
                List<MidiEvent> events = new List<MidiEvent>();

                for (int i = 0; i < amountToGenerate; i++)
                {
                    // create the not and set the pitch and octave
                    Note note = new Note();
                    note.Octave = allowableOctaves[octaveRandom.Next(0, allowableOctaves.Count)];
                    note.Pitch = note.Pitches[allowablePitches[pitchRandom.Next(0, allowablePitches.Count)]];
                    note.Duration = note.Durations[allowableDurations[durationRandom.Next(0, allowableDurations.Count)]];

                    // create the note on and off from properties inside class
                    OnNoteVoiceMidiEvent on = note.CreateNoteOn();
                    OffNoteVoiceMidiEvent off = note.CreateNoteOff();

                    // add the events to the midievent list
                    events.Add(on);
                    events.Add(off);
                }

                // create track with the randomly generate midi events
                MidiTrack track = CreateTrack(trackName, instrument, tempo, time, events);

                // add track to list
                tracks.Add(track);

                // create sequence
                MidiSequence midi = CreateSequence(tracks);

                return midi;

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
