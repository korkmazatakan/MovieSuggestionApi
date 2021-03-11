using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorsController : ControllerBase
    {
        private IDirectorService _directorService;

        public DirectorsController(IDirectorService directorService)
        {
            _directorService = directorService;
        }

        [HttpGet ("getall")]
        public IActionResult GetList()
        {
            var result = _directorService.GetAll();
            if (result.Success)
            {
                return Ok(_directorService.GetAll());
            }

            return BadRequest(result.Message);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _directorService.GetById(id);
            if (result.Success)
            {
                return Ok(_directorService.GetById(id));
            }

            return BadRequest(result.Message);
        }
        [HttpPost("add")]
       // [Authorize(Roles = "Director.Add")]
        public IActionResult Add(Director director)
        {
            var result = _directorService.Add(director);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }
        [HttpPost("update")]
        public IActionResult Update(Director director)
        {
            var result = _directorService.Update(director);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }
        [HttpPost("delete")]
        public IActionResult Delete(Director director)
        {
            var result = _directorService.Delete(director);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }
    }
}
