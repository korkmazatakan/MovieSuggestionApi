using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;

namespace WebApi.Controllers
{
    [Route( "api/[controller]")]
    [ApiController]
    public class MoviesController : Controller
    {
        private IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
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

        [HttpPost("add")]
        public IActionResult Add(Movie movie)
        {
            var result = _movieService.Add(movie);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
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
