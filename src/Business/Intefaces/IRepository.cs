using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Business.Intefaces
{
    public interface IRepository<T> : IDisposable where T : class
    {
        Task Adicionar(T entity);
        Task<List<T>> ObterTodos();
        Task Atualizar(T entity);
        Task<IEnumerable<T>> Buscar(Expression<Func<T, bool>> predicate);
        Task<int> SaveChanges();
    }
}
