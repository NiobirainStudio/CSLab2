using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DB.Models;

namespace DB.Services
{
    public class GenreDataService : DataServiceBase<Genre>
    {
        public GenreDataService(AppDbContext appContext) : base(appContext)
        {
        }

        public override IEnumerable<string> GetAllVisible()
        {
            var res = _db.Genres.Select(a => a.GenreName).ToList();
            return res;
        }

        public override IEnumerable<Genre> GetAll()
        {
            var res = _db.Genres.ToList();
            return res;
        }

        public override void Create(IModel entity)
        {
            var res = _db.Genres.Add((Genre)entity);
            _db.SaveChanges();
        }

        public override IModel Read(int id)
        {
            return _db.Genres.Find(id);
        }

        public override void Update(IModel entity)
        {
            _db.Genres.Update((Genre)entity);
            _db.SaveChanges();
        }

        public override void Delete(IModel entity)
        {
            _db.Genres.Remove((Genre)entity);
            _db.SaveChanges();
        }
    }
}
