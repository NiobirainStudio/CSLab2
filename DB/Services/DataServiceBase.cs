using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DB.Models;

namespace DB.Services
{
    public abstract class DataServiceBase
    {
        protected AppDbContext _db;
        protected Type mainType;
        public Dictionary<string, Type> typesInfo { get; } = new Dictionary<string, Type>();

        protected abstract object TransformById(object[] entity);
        protected abstract object Transform(object[] entity);

        public abstract IEnumerable<string> GetAllVisible();
        public abstract IEnumerable<object> GetAll();
        public abstract int GetIdByVisible(string data);

        public abstract void Create(object[] entity);
        public abstract object? Read(int id);
        public abstract void Update(object[] entity);
        public abstract void Delete(int id);
    }

    public abstract class DataServiceBase<T> : DataServiceBase where T : IModel
    {
        public DataServiceBase(AppDbContext appContext) { 
            _db = appContext;
            mainType = typeof(T);

            foreach (PropertyInfo prop in typeof(T).GetProperties())
                typesInfo.Add(prop.Name, prop.PropertyType);
        }
    }
}
