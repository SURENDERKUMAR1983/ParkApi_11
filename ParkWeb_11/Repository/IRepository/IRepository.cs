using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkWeb_11.Repository.IRepository
{
  public interface IRepository<T>where T:class
  {
        Task<T> GetAsync(string Url, int id);
        Task<IEnumerable<T>> GetAllAsync(string Url);
        Task<bool> CreateAsync(string Url, T ObjToCreate);
        Task<bool> UpdateAsync(string Url, T ObjToUpdate);
        Task<bool> DeleteAsync(string Url, int id);
  }

   
}
