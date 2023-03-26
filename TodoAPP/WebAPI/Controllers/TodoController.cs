using Business.Services.Abstract;
using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IResult = Core.Utilities.Results.IResult;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {

        ITodoService _service;

        public TodoController(ITodoService service)
        {
            _service = service;
        }


        [HttpGet]
        public IDataResult<List<Todo>> Get()
        {
            return new SuccessDataResult<List<Todo>>(_service.GetAll());
        }

        [HttpPost("add")]
        public async Task<IDataResult<Todo>> Add(Todo todo)
        {
            return new SuccessDataResult<Todo>(await _service.AddAsync(todo));
        }

        [HttpPost("delete")]
        public IResult Delete(int id)
        {
            var req = _service.GetTodoById(id);
            _service.Delete(req);
            return new SuccessResult("Silindi!");
        }


        [HttpPost("update")]
        public async Task<IDataResult<Todo>> Update(Todo todo)
        {
            var result = await _service.UpdateAsync(todo);

            return new SuccessDataResult<Todo>(result);
        }


        [Authorize]
        [HttpGet("user/{username}")]
        public IDataResult<List<Todo>> GetByUsername(string username)
        {
            var currentUser = User.Identity.Name;

            return currentUser == username
                ? new SuccessDataResult<List<Todo>>(_service.GetTodoByUsername(username))
                : throw new UnauthorizedAccessException();

        }
    }
}
