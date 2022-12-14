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

        public abstract IEnumerable<object> GetAllVisible();
        public abstract IEnumerable<IModel> GetAll();
        public abstract void Create(IModel entity);
        public abstract IModel Read(int id);
        public abstract void Update(IModel entity);
        public abstract void Delete(IModel entity);
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
