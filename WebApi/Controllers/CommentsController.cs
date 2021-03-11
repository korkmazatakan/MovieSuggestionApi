using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;
using Entities.Dtos;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : Controller
    {
        private ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("getbymovieid")]
        public IActionResult GetByMovieId(int id)
        {
            var result = _commentService.GetByMovieId(id);
            return Ok(result.Data);
        }

        // GET api/<CommentsController>/5
        [HttpGet("getbysubid")]
        public IActionResult GetBySubId(int id)
        {
            var result = _commentService.GetBySubId(id);
            return Ok(result.Data);
        }
        [HttpGet("getpublished")]
        public IActionResult GetAllPublished()
        {
            var result = _commentService.GetAllPublished();
            return Ok(result.Data);
        }

        // POST api/<CommentsController>
        [HttpPost("add")]
        public string Add(CommentAddDto comment)
        {
            //getting current user
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            // getting username of current user
            var username = currentUser.Identity.IsAuthenticated ? currentUser.Identity.Name : "Ziyaretçi"; 
            
            // creating comment
            Comment nComment = new Comment();
            nComment.Text = comment.Text;
            nComment.Username = username;
            nComment.MovieId = comment.MovieId;
            nComment.SubCommentOf = comment.SubCommentOf;

            var result = _commentService.Add(nComment);
            return result.Message;
        }

        // PUT api/<CommentsController>/5
        [HttpPost("update")]
        public string Update(Comment comment)
        {
            var result = _commentService.Update(comment);
            return result.Message;
        }

        // DELETE api/<CommentsController>/5
        [HttpDelete("delete")]
        public string Delete(Comment comment)
        {
            var result = _commentService.Delete(comment);
            return result.Message;
        }
    }
}
