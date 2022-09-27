using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DeviceManagement_WebApp.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {

        //GET: All items 
        IEnumerable<T> GetAll();
        //GET item by id 
        T GetById(Guid? id);
        //Create an item 
        void Create(T item);
        // Edit an item 
        T Edit(Guid id, T item);
        //delete an item 
        void DeleteConfirmed(Guid? id);
        
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
        // check if item exist 
        bool CheckIfItemExists(Guid? id);





    }
}
