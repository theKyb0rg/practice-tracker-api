using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PracticeLog.Models;
using musicians_practice_log_api.ViewModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using musicians_practice_log_api.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace musicians_practice_log_api.Controllers
{
    [Route("api/[controller]")]
    public class RiffGeneratorController : Controller
    {
        private PracticeLogContext _context;

        public RiffGeneratorController(PracticeLogContext context)
        {
            _context = context;
        }

        // POST api/riffgenerator/deleteById
        [HttpPost]
        [Route("deleteById")]
        public ResultViewModel DeleteById([FromBody]int riffId)
        {
            var riff = _context.Riffs.SingleOrDefault(s => s.Id == riffId);
            if (riff != null)
            {
                try
                {
                    if (riff != null)
                    {
                        _context.Remove(riff);
                        _context.SaveChanges();
                    }
                    return new ResultViewModel(Result.Success, riff.Name + " was successfully deleted.");
                }
                catch (Exception ex)
                {
                    return new ResultViewModel(Result.Error, "Could not delete " + riff.Name + ". Try again later.", ex);
                }
            }
            else
            {
                return new ResultViewModel(Result.Error, "Could not find " + riff.Name + ". Try again later.");
            }
        }

        // POST api/riffgenerator/insert
        [HttpPost]
        [Route("insert")]
        public ResultViewModel Insert([FromBody]Riff riff)
        {
            try
            {
                _context.Add(riff);
                _context.SaveChanges();
                return new ResultViewModel(Result.Success, riff.Name + " was successfully created.");
            }
            catch (Exception ex)
            {
                return new ResultViewModel(Result.Error, "Could not create " + riff.Name + ". Try again later.", ex);
            }
        }

        // GET: api/riffgenerator
        [HttpGet]
        [Route("selectAll")]
        public string SelectAll()
        {
            return Helper.ParseToJsonString(_context.Riffs);
        }

        // POST api/riffgenerator/selectById
        [HttpPost]
        [Route("selectById")]
        public ResultViewModel SelectById([FromBody]int riffId)
        {
            var riff = _context.Riffs.SingleOrDefault(s => s.Id == riffId);
            if (riff != null)
            {
                return new ResultViewModel(Result.Success, riff);
            }
            else
            {
                return new ResultViewModel(Result.Error, "Could not find " + riff.Name + ". Try again later.");
            }
        }

        // POST api/riffgenerator/update
        [HttpPost]
        [Route("update")]
        public ResultViewModel Update([FromBody]Riff newRiff)
        {
            var oldRiff = _context.Riffs.SingleOrDefault(s => s.Id == newRiff.Id);
            if (oldRiff != null)
            {
                try
                {
                    oldRiff.Name = newRiff.Name;
                    oldRiff.FilePath = newRiff.FilePath;
                    _context.SaveChanges();
                    return new ResultViewModel(Result.Success, newRiff.Name + " was successfully updated.");
                }
                catch (Exception ex)
                {

                    return new ResultViewModel(Result.Error, "Could not update " + newRiff.Name + ". Try again later.", ex);
                }
            }
            else
            {
                return new ResultViewModel(Result.Error, "Could not find " + newRiff.Name + ". Try again later.");
            }
        }


        [HttpPost]
        [Route("generate")]
        public string Generate([FromBody]RiffGeneratorViewModel newRiff)
        {
            return Helper.ParseToJsonString(_context.Riffs);
        }
    }
}
