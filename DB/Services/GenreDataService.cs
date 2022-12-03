using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DB.Models;

namespace DB.Services
{
    public class GenreDataService : BaseDataService<Genre>
    {
        public GenreDataService(AppDbContext appContext) : base(appContext)
        {
        }

        public override IEnumerable<Genre> GetAll()
        {
            var res = _db.Genres.ToList();
            return res;
        }

        public override Genre Create(Genre entity)
        {
            var res = _db.Genres.Add(entity);
            _db.SaveChanges();
            return res.Entity;
        }

        public override Genre Read(int id)
        {
            return _db.Genres.Find(id);
        }

        public override Genre Update(Genre entity)
        {
            _db.Genres.Update(entity);
            _db.SaveChanges();
            return entity;
        }

        public override bool Delete(Genre entity)
        {
            _db.Genres.Remove(entity);
            _db.SaveChanges();
            return true;
        }
    }
}
