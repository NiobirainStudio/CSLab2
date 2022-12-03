using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DB.Models;

namespace DB.Services
{
    public abstract class BaseDataService<T> where T : IModel
    {
        protected AppDbContext _db;
        public Dictionary<string, Type> typesInfo { get; } = new Dictionary<string, Type>();

        public BaseDataService(AppDbContext appContext) { 
            _db = appContext;

            foreach (PropertyInfo prop in typeof(T).GetProperties())
                typesInfo.Add(prop.Name, prop.PropertyType);
        }

        public abstract IEnumerable<T> GetAll();
        public abstract T Create(T entity);
        public abstract T Read(int id);
        public abstract T Update(T entity);
        public abstract bool Delete(T entity);
    }
}
