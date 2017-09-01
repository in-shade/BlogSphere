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
    [Route("api/tag")]
    public class TagController : Controller
    {
        private ITagRepository _tagRepository;

        public TagController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        [Route("")]
        [HttpGet]
        public ActionResult GetAllTags()
        {
            var tags = _tagRepository.GetAll();
            return Ok( AutoMapper.Mapper.Map<ICollection<TagViewModel>>(tags));
        }

        [Route("")]
        [HttpPost]
        public ActionResult InsertTag([FromBody] TagViewModel tagViewModel)
        {
            var tag = AutoMapper.Mapper.Map<Tag>(tagViewModel);

            if (_tagRepository.Insert(tag))
            {
                return Ok("Tag " + tagViewModel.Name + " added succesfully!");
            }

            return BadRequest("Invalid tag object.");
        }

        [Route("{id:int}")]
        [HttpGet]
        public ActionResult GetTagById(int id)
        {
            var tag = _tagRepository.GetById(id);
            return Ok( AutoMapper.Mapper.Map<TagViewModel>(tag) );
        }
    }
}
