using PracticeLog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace musicians_practice_log_api.Models
{
    public static class DbInitializer
    {
        public static void Initialize(PracticeLogContext context)
        {
            context.Database.EnsureCreated();

            // Look for any users.
            if (!context.Users.Any())
            {
                var students = new User[]
                {
                    new User { Name = "Free"},
                    new User { Name = "Pro"},
                    new User { Name = "Admin"}
                };
                context.AddRange(students);
                context.SaveChanges();
            }

            if (!context.Instruments.Any())
            {
                var experiencePoints = new Experience[]
                {
                    new Experience { Points = 0 },
                    new Experience { Points = 1000 },
                    new Experience { Points = 450 },
                };

                var instruments = new Instrument[]
                {
                    new Instrument { Name = "Guitar", UserId = 1, Experience = experiencePoints[0] },
                    new Instrument { Name = "Piano", UserId = 2, Experience = experiencePoints[1] },
                    new Instrument { Name = "Bass", UserId = 3, Experience = experiencePoints[2] },
                };
                context.AddRange(instruments);
                context.SaveChanges();
            }

            
            if (!context.Categorys.Any())
            {
                var categories = new Category[]
                {
                    new Category { Name = "Lead", InstrumentId = 1 },
                    new Category { Name = "Rhythm", InstrumentId = 2 },
                    new Category { Name = "Theory", InstrumentId = 3 },
                };
                context.AddRange(categories);
                context.SaveChanges();
            }

            if (!context.Exercises.Any())
            {
                var exercises = new Exercise[]
                {
                    new Exercise { Name = "Arpeggio", CategoryId = 1 },
                    new Exercise { Name = "Palm Muting", CategoryId = 2 },
                    new Exercise { Name = "Alphabet", CategoryId = 3 },
                };
                context.AddRange(exercises);
                context.SaveChanges();
            }

            if (!context.PracticeSessions.Any())
            {
                var practiceSessions = new PracticeSession[]
                {
                    new PracticeSession { Date = DateTime.Now, Comment = "Sloppy leads bro.", Tempo = 150, Time = 30, ExerciseId = 1 },
                    new PracticeSession { Date = DateTime.Now, Comment = "Perfect palm muting.", Tempo = 120, Time = 15, ExerciseId = 2 },
                    new PracticeSession { Date = DateTime.Now, Comment = "Can't remember where C# is.", Tempo = 60, Time = 20, ExerciseId = 3 },
                };
                context.AddRange(practiceSessions);
                context.SaveChanges();
            }
        }
    }
}
