using Business.Services.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concrete
{
    public class TodoManager : ITodoService
    {

        ITodoDal _todoDal;

        public TodoManager(ITodoDal todoDal)
        {
            _todoDal = todoDal;
        }

        public async Task<Todo> AddAsync(Todo todo)
        {
            var result = await _todoDal.AddAsync(todo);

            return result;
        }

        public IResult Delete(Todo todo)
        {
            _todoDal.Delete(todo);

            return new SuccessResult("Todo Silindi!");

        }

        public List<Todo> GetAll()
        {
            var result = _todoDal.GetAll();
            return result;
        }

        public Todo GetTodoById(int id)
        {
            return _todoDal.Get(x => x.Id == id);
        }

        public List<Todo> GetTodoByUserId(int userId)
        {
            return _todoDal.GetAll(x => x.UserId == userId);
        } 
        public List<Todo> GetTodoByUsername(string name)
        {
            return _todoDal.GetAll(x => x.User.Username == name);
        }

        public async Task<Todo> UpdateAsync(Todo todo)
        {
            var result = await _todoDal.UpdateAsync(todo);
            return result;
        }
    }
}
