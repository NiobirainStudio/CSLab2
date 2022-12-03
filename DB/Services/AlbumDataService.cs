using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DB.Models;

namespace DB.Services
{
    public class AlbumDataService : BaseDataService<Album>
    {
        public AlbumDataService(AppDbContext appContext) : base(appContext)
        {
        }

        public override IEnumerable<Album> GetAll()
        {
            var res = _db.Albums.ToList();
            return res;
        }

        public override Album Create(Album entity)
        {
            var res = _db.Albums.Add(entity);
            _db.SaveChanges();
            return res.Entity;
        }

        public override Album Read(int id)
        {
            return _db.Albums.Find(id);
        }

        public override Album Update(Album entity)
        {
            _db.Albums.Update(entity);
            _db.SaveChanges();
            return entity;
        }

        public override bool Delete(Album entity)
        {
            _db.Albums.Remove(entity);
            _db.SaveChanges();
            return true;
        }
    }
}
