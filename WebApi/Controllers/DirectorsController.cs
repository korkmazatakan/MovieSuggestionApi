using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Entities.Dtos;
using Core.Utilities.Results;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Core.Aspects.Autofac.Caching;
using Business.BusinessAspect.Autofac;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorsController : ControllerBase
    {
        private IDirectorService _directorService;
        private IWebHostEnvironment _webHostEnvironment;


        public DirectorsController(IDirectorService directorService, IWebHostEnvironment webHostEnvironment)
        {
            _directorService = directorService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("getall")]
        public IActionResult GetList()
        {
            var result = _directorService.GetAll();
            if (result.Success)
            {
                return Ok(_directorService.GetAll());
            }

            return BadRequest(result.Message);
        }

        [HttpGet("search")]
        public IActionResult Index(string searchQuery)
        {
            var result = _directorService.GetByQuery(searchQuery);
            if (result.Success)
            {
                return Ok(result.Data);
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
        public IActionResult Add([FromForm]DirectorAddDto director)
        {

            IResult result = new SuccessResult();
            try
            {
                if (!director.Portre.Equals(null))
                {
                    string path = _webHostEnvironment.WebRootPath + "/uploads/directorcontent/posters/";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    Director nDirector = new Director();
                    nDirector.Name = director.Name;
                    nDirector.BornAt = director.BornAt;
                    nDirector.BornIn = director.BornIn;
                    nDirector.Description = director.Description;
                    nDirector.Portre = Guid.NewGuid().ToString("N") + "." + director.Portre.FileName.Split(".")[1];

                    using (FileStream fileStream = System.IO.File.Create(path + nDirector.Portre))
                    {
                        result = _directorService.Add(nDirector);
                        director.Portre.CopyTo(fileStream);
                        fileStream.Flush();
                    }
                }
                return Ok(result.Message);
            }
            catch (Exception)
            {
                return BadRequest(result.Message);
            }
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
