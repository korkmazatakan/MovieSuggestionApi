using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.Extensions.Logging;
using Core.Entities;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Dtos;
using Microsoft.AspNetCore.Hosting;

namespace WebApi.Controllers
{
    [Route( "api/[controller]")]
    [ApiController]
    public class MoviesController : Controller,IController
    {
        private IMovieService _movieService;
        private IWebHostEnvironment _webHostEnvironment;


        public MoviesController(IMovieService movieService, IWebHostEnvironment webHostEnvironment)
        {
            _movieService = movieService;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _movieService.GetAll();
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        [HttpGet("getbycount")]
        public IActionResult GetByCount(int count)
        {
            var result = _movieService.GetByCount(count);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _movieService.GetById(id);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        [HttpGet("getbygenre")]
        public IActionResult GetByGenre(int genre_id)
        {
            var result = _movieService.GetListByGenre(genre_id);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        [HttpGet("getbydirector")]
        public IActionResult GetByDirectorId(int director_id)
        {
            var result = _movieService.GetListByDirector(director_id);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }
        
        [HttpGet("getlastmovies")]
        public IActionResult GetLastReleased(int count)
        {
            var result = _movieService.GetLastReleased(count);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        [HttpGet("gettopboxoffice")]
        public IActionResult GetTopBoxOffice(int count)
        {
            var result = _movieService.GetTopBoxOffice(count);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("add")]
        public IActionResult Add([FromForm]MovieAddDto movie)
        {
            
            IResult result = new SuccessResult();
            try
            {
                if (!movie.PosterFile.Equals(null))
                {
                    string path = _webHostEnvironment.WebRootPath + "/uploads/moviecontent/posters/";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    Movie nMovie = new Movie();
                        nMovie.Name = movie.Name;
                        nMovie.Description = movie.Description;
                        nMovie.Poster = Guid.NewGuid().ToString("N")  + "." + movie.PosterFile.FileName.Split(".")[1];
                        nMovie.DirectorId = movie.DirectorId;
                        nMovie.GenreId = movie.GenreId;
                        nMovie.ReleaseDate = movie.ReleaseDate;
                        
                        using (FileStream fileStream = System.IO.File.Create(path + nMovie.Poster))
                        {
                            result = _movieService.Add(nMovie);
                            movie.PosterFile.CopyTo(fileStream);
                            fileStream.Flush();
                        }
                }
                return Ok(result.Message);
            }
            catch (Exception)
            {
                return BadRequest(result.Message);
            }
            
/*
            var result = _movieService.Add(movie);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
*/
        }
        [HttpPost("update")]
        public IActionResult Update(Movie movie)
        {
            var result = _movieService.Update(movie);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }
        [HttpPost("delete")]
        public IActionResult Delete(Movie movie)
        {
            var result = _movieService.Delete(movie);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }
        [HttpPost("transactiontest")]
        public IActionResult Transaction(Movie movie)
        {
            var result = _movieService.TransactionTest(movie);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }
    }
}
