using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DeviceManagement_WebApp.Data;
using DeviceManagement_WebApp.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;



namespace DeviceManagement_WebApp.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public readonly ConnectedOfficeContext _context;

        public GenericRepository(ConnectedOfficeContext context)
        {
            _context = context;
        }

        //Create an item
        public void Create(T item)
        {
            _context.Set<T>().Add(item);
            
        }

        //delete an existing Item    
        public void DeleteConfirmed(Guid? id)
        {
            T existingItem =  GetById(id);
            if (existingItem != null)
            {
                _context.Set<T>().Remove(existingItem);
                
            }
        }

        //Edit the specified item
        public T Edit(T item)
        {
            if (item != null)  
            {
                
                _context.Set<T>().Update(item);
                
            }
            return item;
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            
            return  _context.Set<T>().Where(expression);
        }

        //Return all the data of T type instantiated 
        public IEnumerable<T> GetAll()
        {

            return _context.Set<T>().ToList();
        }

        //Return one specified item
        public T GetById(Guid? id)
        {
            if (id == null)
            {
                return null;
            }

            T item =  _context.Set<T>().Find(id);
            if (item == null)
            {
                return null;
            }

            return item;
            
        }

        //Check if an item exists by using the GetById method
        public bool CheckIfItemExists(Guid? id)
        {
            var category =  GetById(id);
            if (category != null)
            {
                return true;
            }
            return false;
        }
    }
}
