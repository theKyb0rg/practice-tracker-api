using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PracticeLog.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;
using MidiSharp;
using musicians_practice_log_api.RiffGenerator;

namespace musicians_practice_log_api.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Microsoft.AspNetCore.Mvc.Controller
    {
        private PracticeLogContext _context;
        private IHostingEnvironment _env;

        public ValuesController(PracticeLogContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {

            //for (int i = 0; i < 10; i++)
            //{
            //    // get file location for saving and reading
            //    string saveLocation = Path.Combine(_env.ContentRootPath + "/Files/", "GOGOGOGO" + i + ".mid");

            //    // generate the midi sequence
            //    MidiSequence midi = RiffGenerator.RiffGenerator.Randomize("Track Name", 1000, GeneralMidiInstrument.AcousticGrand);

            //    // save to file
            //    using (Stream file = System.IO.File.Create(saveLocation))
            //    {
            //        midi.Save(file);
            //    }
            //}

            for (int i = 0; i < 10; i++)
            {
                // get file location for saving and reading

                string saveLocation = Path.Combine(_env.ContentRootPath + "/Files/", "breakdown" + i + ".mid");

                List<int> allowableOctaves = new List<int>() { 3 };




                List<int> allowablePitches = new List<int>() { 8 };
                List<int> allowableDurations = new List<int>() { 2, 3 };

                // generate the midi sequence
                MidiSequence midi = RiffGenerator.RiffGenerator.Generate(allowableOctaves, allowablePitches, allowableDurations, "Track Name", 400, GeneralMidiInstrument.DistortionGuitar, new TimeSignature(4, 4), 140);

                // save to file
                using (Stream file = System.IO.File.Create(saveLocation))
                {
                    midi.Save(file);
                }
            }

            var users = _context.Users.ToList();
            return new string[] { "value1", "value2" };


        }

    }
}
