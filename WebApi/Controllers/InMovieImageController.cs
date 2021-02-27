using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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
                    
                    string path = _webHostEnvironment.WebRootPath + "\\uploads\\moviecontent\\";
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
    }
}
