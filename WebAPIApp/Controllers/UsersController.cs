using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIApp.DAL;
using WebAPIApp.Models;
using WebAPIApp.ViewModel;

namespace WebAPIApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserDBContext _context;
        private readonly IMapper _mapper;

        public UsersController(UserDBContext context, IMapper mapper)
        {
            _context = context;
            if (!_context.Users.Any())
            {
                _context.Users.Add(new User { Name = "Mike", Age = 26 });
                _context.Users.Add(new User { Name = "Alice", Age = 31 });
                _context.SaveChangesAsync();
            }
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<UserListViewModel>> GetAll()
        {
            var command = await _context.Users.ToListAsync();

            return new UserListViewModel { Users = _mapper.Map<IEnumerable<UserViewModel>>(command) };
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserViewModel>> Get(Guid id)
        {
            if (ModelState.IsValid)
            {
                var command = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
                if (command == null)
                {
                    return NotFound();
                }
                var user = _mapper.Map<UserViewModel>(command);

                return new ObjectResult(user);
            }

            return BadRequest(ModelState);
        }

        [HttpPost]
        public async Task<ActionResult> Create(UserCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<UserCreateViewModel, User>(model);

                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return Ok(user);
            }

            return BadRequest(ModelState);

            
        }
        
        [HttpPut]
        public async Task<ActionResult<User>> Update(UserUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FindAsync(model.Id);
                if (model == null)
                {
                    return NotFound();
                }
                _mapper.Map<UserUpdateViewModel, User>(model, user);
                _context.Update(user);
                await _context.SaveChangesAsync();
                return Ok(user);
            }

            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]        
        public async Task<ActionResult<User>> Delete(Guid id)
        {
            User user = _context.Users.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return Ok(user);
        }
    }
}
