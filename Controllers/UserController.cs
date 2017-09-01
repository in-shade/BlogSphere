using BlogSphere.Data;
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

    [Route("api/user")]
    public class UserController : Controller
    {
        private IUserRepository _userRepository;
        private IPostRepository _postRepository;

        public UserController(IUserRepository userRepository, IPostRepository postRepository)
        {
            _userRepository = userRepository;
            _postRepository = postRepository;
        }

        [Route("")]
        [HttpGet]
        public ActionResult GetAllUsers()
        {
            var allUsers = _userRepository.GetAll();
            return Ok(AutoMapper.Mapper.Map<ICollection<UserViewModel>>(allUsers));
        }

        [Route("")]
        [HttpPost]
        public ActionResult PostUser([FromBody] UserViewModel userViewModel)
        {
            var user = AutoMapper.Mapper.Map<User>(userViewModel);
            if (_userRepository.Insert(user))
            {
                return Ok("User " + userViewModel.Nickname + " created succesfully!");
            }
            return BadRequest("Invalid user object.");
        }

        [Route("{id:int}")]
        [HttpGet]
        public ActionResult GetUserById(int id)
        {
            var user = _userRepository.GetById(id);
            return Ok(AutoMapper.Mapper.Map<UserViewModel>(user));
        }

        [Route("{id:int}/delete")]
        [HttpGet]
        public ActionResult DeleteUser(int id)
        {
           if( _userRepository.Delete(id))
            {
                return Ok("Deleted succesfully!");
            }

            return BadRequest();
        }
        
        [Route("{id:int}/post")]
        [HttpGet]
        public ActionResult GetPostsByUser(int id)
        {
            //return Ok("test");

            var posts = _postRepository.GetAllByUserId(id);

            //return Ok(posts);
            return Ok(AutoMapper.Mapper.Map<ICollection<PostViewModel>>(posts));
        }

        [Route("{nickname}")]
        [HttpGet]
        public ActionResult GetUserByNickname(string nickname)
        {
            var user = _userRepository.GetByName(nickname);
            return Ok(AutoMapper.Mapper.Map<UserViewModel>(user));
        }
    }
}
