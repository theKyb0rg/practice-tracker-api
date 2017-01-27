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
    public class ExerciseController : Controller
    {
        private PracticeLogContext _context;

        public ExerciseController(PracticeLogContext context)
        {
            _context = context;
        }

        // POST api/exercise/deleteById
        [HttpPost]
        [Route("deleteById")]
        public ResultViewModel DeleteById([FromBody]int exerciseId)
        {
            var exercise = _context.Exercises.SingleOrDefault(s => s.Id == exerciseId);
            if (exercise != null)
            {
                try
                {
                    if (exercise != null)
                    {
                        _context.Remove(exercise);
                        _context.SaveChanges();
                    }
                    return new ResultViewModel(Result.Success, exercise.Name + " was successfully deleted.");
                }
                catch (Exception ex)
                {
                    return new ResultViewModel(Result.Error, "Could not delete " + exercise.Name + ". Try again later.", ex);
                }
            }
            else
            {
                return new ResultViewModel(Result.Error, "Could not find " + exercise.Name + ". Try again later.");
            }
        }

        // POST api/exercise/insert
        [HttpPost]
        [Route("insert")]
        public ResultViewModel Insert([FromBody]Exercise exercise)
        {
            try
            {
                _context.Add(exercise);
                _context.SaveChanges();
                return new ResultViewModel(Result.Success, exercise.Name + " was successfully created.");
            }
            catch (Exception ex)
            {
                return new ResultViewModel(Result.Error, "Could not create " + exercise.Name + ". Try again later.", ex);
            }
        }

        // GET: api/exercise
        [HttpGet]
        [Route("selectAll")]
        public string SelectAll()
        {
            return Helper.ParseToJsonString(_context.Exercises.Include(s => s.PracticeSessions));
        }

        // POST api/exercise/selectById
        [HttpPost]
        [Route("selectById")]
        public ResultViewModel SelectById([FromBody]int exerciseId)
        {
            var exercise = _context.Exercises.SingleOrDefault(s => s.Id == exerciseId);
            if (exercise != null)
            {
                return new ResultViewModel(Result.Success, exercise);
            }
            else
            {
                return new ResultViewModel(Result.Error, "Could not find " + exercise.Name + ". Try again later.");
            }
        }

        // POST api/exercise/update
        [HttpPost]
        [Route("update")]
        public ResultViewModel Update([FromBody]Exercise newExercise)
        {
            var oldExercise = _context.Exercises.SingleOrDefault(s => s.Id == newExercise.Id);
            if (oldExercise != null)
            {
                try
                {
                    oldExercise.Name = newExercise.Name;
                    oldExercise.CategoryId = newExercise.CategoryId;
                    _context.SaveChanges();
                    return new ResultViewModel(Result.Success, newExercise.Name + " was successfully updated.");
                }
                catch (Exception ex)
                {

                    return new ResultViewModel(Result.Error, "Could not update " + newExercise.Name + ". Try again later.", ex);
                }
            }
            else
            {
                return new ResultViewModel(Result.Error, "Could not find " + newExercise.Name + ". Try again later.");
            }

        }
    }
}
