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

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace musicians_practice_log_api.Controllers
{
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private PracticeLogContext _context;

        public CategoryController(PracticeLogContext context)
        {
            _context = context;
        }

        // POST api/category/deleteById
        [HttpPost]
        [Route("deleteById")]
        public ResultViewModel DeleteById([FromBody]int categoryId)
        {
            var category = _context.Categorys.SingleOrDefault(s => s.Id == categoryId);
            if (category != null)
            {
                try
                {
                    if (category != null)
                    {
                        _context.Remove(category);
                        _context.SaveChanges();
                    }
                    return new ResultViewModel(Result.Success, category.Name + " was successfully deleted.");
                }
                catch (Exception ex)
                {
                    return new ResultViewModel(Result.Error, "Could not delete " + category.Name + ". Try again later.", ex);
                }
            }
            else
            {
                return new ResultViewModel(Result.Error, "Could not find " + category.Name + ". Try again later.");
            }
        }

        // POST api/category/insert
        [HttpPost]
        [Route("insert")]
        public ResultViewModel Insert([FromBody]Category category)
        {
            try
            {
                _context.Add(category);
                _context.SaveChanges();
                return new ResultViewModel(Result.Success, category.Name + " was successfully created.");
            }
            catch (Exception ex)
            {
                return new ResultViewModel(Result.Error, "Could not create " + category.Name + ". Try again later.", ex);
            }
        }

        // GET: api/category
        [HttpGet]
        [Route("selectAll")]
        public string SelectAll()
        {
            return Helper.ParseToJsonString(_context.Categorys.Include(s => s.Instrument));
        }

        // POST api/category/selectById
        [HttpPost]
        [Route("selectById")]
        public ResultViewModel SelectById([FromBody]int categoryId)
        {
            var category = _context.Categorys.SingleOrDefault(s => s.Id == categoryId);
            if (category != null)
            {
                return new ResultViewModel(Result.Success, category);
            }
            else
            {
                return new ResultViewModel(Result.Error, "Could not find " + category.Name + ". Try again later.");
            }
        }

        // POST api/category/update
        [HttpPost]
        [Route("update")]
        public ResultViewModel Update([FromBody]Category newCategory)
        {
            var oldCategory = _context.Categorys.SingleOrDefault(s => s.Id == newCategory.Id);
            if (oldCategory != null)
            {
                try
                {
                    oldCategory.Name = newCategory.Name;
                    oldCategory.InstrumentId = newCategory.InstrumentId;
                    _context.SaveChanges();
                    return new ResultViewModel(Result.Success, newCategory.Name + " was successfully updated.");
                }
                catch (Exception ex)
                {

                    return new ResultViewModel(Result.Error, "Could not update " + newCategory.Name + ". Try again later.", ex);
                }
            }
            else
            {
                return new ResultViewModel(Result.Error, "Could not find " + newCategory.Name + ". Try again later.");
            }

        }
    }
}
