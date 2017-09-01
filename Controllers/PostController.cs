using BlogSphere.Models;
using BlogSphere.Repositories;
using BlogSphere.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogSphere.Controllers
{
    [Route("api/post")]
    public class PostController : Controller
    {
        private IPostRepository _postRepository;
        private ITagRepository _tagRepository;

        public PostController(IPostRepository postRepository, ITagRepository tagRepository)
        {
            _postRepository = postRepository;
            _tagRepository = tagRepository;
        }

        [Route("")]
        [HttpGet]
        public ActionResult GetAllPosts()
        {
            var allPosts = _postRepository.GetAll();

            return Ok( AutoMapper.Mapper.Map<ICollection<PostViewModel>>(allPosts) );
        }

        [Route("")]
        [HttpPost]
        public IActionResult InsertPost([FromBody] PostCreateModel postViewModel)
        {
            var post = AutoMapper.Mapper.Map<Post>(postViewModel);
            post.PostDate = DateTime.UtcNow;

            if (_postRepository.Insert(post))
            {
                return Ok("Post added succesfully!");
            }
            else
            {
                return BadRequest("Invalid post object");
            }
        }

        [Route("{id:int}")]
        [HttpGet]
        public ActionResult GetPostById(int id)
        {
            var post = _postRepository.GetById(id);
            return Ok( AutoMapper.Mapper.Map<PostViewModel>(post) );
        }

        [Route("{id:int}")]
        [HttpPost]
        public ActionResult TagPost(int id, [FromBody] TagViewModel tagViewModel)
        {
            var tag = AutoMapper.Mapper.Map<Tag>(tagViewModel);

            if(_tagRepository.GetByName(tag.Name) == null)
            {
                _tagRepository.Insert(tag);
            }

            _postRepository.InsertTag(id, tag);

            return Ok();
        }
    }
}
