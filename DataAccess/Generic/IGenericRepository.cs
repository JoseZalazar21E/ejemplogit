using Entities.DataContext;
using Entities.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Generic
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<bool> CreateAsync(T entity);
        Task<bool> DeleteByIdAsync(int id);
        Task<bool> UpdateAsync(int id, T entity);
    }
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public readonly IUnitOfWork _unitOfWork;

        public GenericRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _unitOfWork.Context.Set<T>().ToListAsync();
        }
        public async Task<T> GetByIdAsync(int id)
        {
            try
            {
                return await _unitOfWork.Context.Set<T>().FindAsync(id);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> CreateAsync(T entity)
        {
            try
            {
                await _unitOfWork.Context.Set<T>().AddAsync(entity);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> DeleteByIdAsync(int id)
        {
            try
            {
                var entity = await _unitOfWork.Context.Set<T>().FindAsync(id);
                if (entity != null)
                {
                    _unitOfWork.Context.Set<T>().Remove(entity);
                    return true;
                }
                else { return false; }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> UpdateAsync(int id, T entity)
        {
            try
            {
                T existingEntity = await GetByIdAsync(id);
                if (existingEntity != null)
                {
                    PropertyInfo[] properties = entity.GetType().GetProperties();
                    foreach (PropertyInfo property in properties)
                    {
                        var newValue = property.GetValue(entity);
                        if (newValue != null && property.Name != "Id")
                        {
                            property.SetValue(existingEntity, newValue);
                        }
                    }
                    _unitOfWork.Context.Set<T>().Update(existingEntity);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
