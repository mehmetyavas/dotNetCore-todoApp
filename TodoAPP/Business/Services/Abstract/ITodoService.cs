using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract
{
    public interface ITodoService
    {
        List<Todo> GetAll();
        List<Todo> GetTodoByUserId(int userId);
        List<Todo> GetTodoByUsername(string name);
        Todo GetTodoById(int id);

        Task<Todo> AddAsync(Todo todo);
        Task<Todo> UpdateAsync(Todo todo);
        IResult Delete(Todo todo);


    }
}
