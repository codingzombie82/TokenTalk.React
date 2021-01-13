using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TokenTalk.Models
{

    public interface ICrudRepositoryBase<T, TIdentifier>
    {
        Task<T> AddAsync(T model); //입력

        Task<List<T>> GetAllAsync(); // 출력

        Task<T> GetByIdAsync(TIdentifier id); // 상세

        Task<bool> UpdateAsync(T model); //수정

        Task<bool> DeleteAsync(TIdentifier id); // 삭제

    }

    public interface ITalkCrudRepository<T> : ICrudRepositoryBase<T, int>
    {     

    }
}
