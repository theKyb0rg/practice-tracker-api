using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PracticeLog.Models;
using musicians_practice_log_api.ViewModels;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace musicians_practice_log_api.Controllers
{
    [Route("api/[controller]")]
    public class PracticeSessionController : Controller
    {
        private PracticeLogContext _context;

        public PracticeSessionController(PracticeLogContext context)
        {
            _context = context;
        }

        // POST api/practiceSession/deleteById
        [HttpPost]
        [Route("deleteById")]
        public ResultViewModel DeleteById([FromBody]int practiceSessionId)
        {
            var practiceSession = _context.PracticeSessions.SingleOrDefault(s => s.Id == practiceSessionId);
            if (practiceSession != null)
            {
                try
                {
                    if (practiceSession != null)
                    {
                        _context.Remove(practiceSession);
                        _context.SaveChanges();
                    }
                    return new ResultViewModel(Result.Success, "Practice Session recorded on " + practiceSession.Date + " was successfully deleted.");
                }
                catch (Exception ex)
                {
                    return new ResultViewModel(Result.Error, "Could not delete Practice Session recorded on " + practiceSession.Date + ". Try again later.", ex);
                }
            }
            else
            {
                return new ResultViewModel(Result.Error, "Could not find Practice Session recorded on " + practiceSession.Date + ". Try again later.");
            }
        }

        // POST api/practiceSession/insert
        [HttpPost]
        [Route("insert")]
        public ResultViewModel Insert([FromBody]PracticeSession practiceSession)
        {
            try
            {
                _context.Add(practiceSession);
                _context.SaveChanges();
                return new ResultViewModel(Result.Success, "Practice Session was successfully created.");
            }
            catch (Exception ex)
            {
                return new ResultViewModel(Result.Error, "Could not create Practice Session. Try again later.", ex);
            }
        }

        // GET: api/practiceSession
        [HttpGet]
        [Route("selectAll")]
        public string SelectAll()
        {
            return Helper.ParseToJsonString(_context.PracticeSessions);
        }

        // POST api/practiceSession/selectById
        [HttpPost]
        [Route("selectById")]
        public ResultViewModel SelectById([FromBody]int practiceSessionId)
        {
            var practiceSession = _context.PracticeSessions.SingleOrDefault(s => s.Id == practiceSessionId);
            if (practiceSession != null)
            {
                return new ResultViewModel(Result.Success, practiceSession);
            }
            else
            {
                return new ResultViewModel(Result.Error, "Could not find Practice Session recorded on " + practiceSession.Date + ". Try again later.");
            }
        }

        // POST api/practiceSession/update
        [HttpPost]
        [Route("update")]
        public ResultViewModel Update([FromBody]PracticeSession newPracticeSession)
        {
            var oldPracticeSession = _context.PracticeSessions.SingleOrDefault(s => s.Id == newPracticeSession.Id);
            if (oldPracticeSession != null)
            {
                try
                {
                    oldPracticeSession.Comment = newPracticeSession.Comment;
                    oldPracticeSession.Date = newPracticeSession.Date;
                    oldPracticeSession.ExerciseId = newPracticeSession.ExerciseId;
                    oldPracticeSession.Tempo = newPracticeSession.Tempo;
                    oldPracticeSession.Time = newPracticeSession.Time;
                    _context.SaveChanges();
                    return new ResultViewModel(Result.Success, "Practice Session recorded on " + oldPracticeSession.Date + " was successfully updated.");
                }
                catch (Exception ex)
                {

                    return new ResultViewModel(Result.Error, "Could not update Practice Session recorded on " + oldPracticeSession.Date + ". Try again later.", ex);
                }
            }
            else
            {
                return new ResultViewModel(Result.Error, "Could not find Practice Session recorded on " + oldPracticeSession.Date + ". Try again later.");
            }

        }
    }
}
