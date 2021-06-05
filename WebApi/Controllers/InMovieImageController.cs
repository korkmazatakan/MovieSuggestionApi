using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using Business.Abstract;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Hosting;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InMovieImageController : Controller
    {
        private IInMovieImageService _inMovieImageService;
        private IWebHostEnvironment _webHostEnvironment;

        public InMovieImageController(IInMovieImageService inMovieImageService, IWebHostEnvironment webHostEnvironment)
        {
            _inMovieImageService = inMovieImageService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost("add")]
        public IActionResult Add([FromForm]InMovieImageDto image)
        {
            IResult result = new SuccessResult();
            try
            {
                if (image.files.Count() > 0)
                {
                    
                    string path = _webHostEnvironment.WebRootPath + "/uploads/moviecontent/";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    foreach (var item in image.files)
                    {
                        InMovieImage img = new InMovieImage();
                        img.ImageName = Guid.NewGuid().ToString("N") + "-id-" + image.movieId + "." + item.FileName.Split(".")[1];
                        img.MovieId = image.movieId;
                        img.Id = Guid.NewGuid().ToString("D");
                        using (FileStream fileStream = System.IO.File.Create(path + img.ImageName))
                        {
                            result = _inMovieImageService.Add(img);
                            item.CopyTo(fileStream);
                            fileStream.Flush();
                            
                        }
                    }
                }

                return Ok(result.Message);
            }
            catch (Exception)
            {
                return BadRequest(result.Message);
            }
        }

        [HttpGet("getbymovieid")]
        public IActionResult GetByMovieId(int movieId)
        {
            var result = _inMovieImageService.GetByMovieId(movieId);
            return Ok(result);
        }
        [HttpGet("getbyid")]
        public IActionResult GetById(string id)
        {
            var result = _inMovieImageService.GetById(id);
            string imgName = result.Data.ImageName;
            var image = System.IO.File.OpenRead($"wwwroot/uploads/moviecontent/{imgName}");
            
            return File(image, "image/jpeg");
        }
    }
}
