using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DB.Models;

namespace DB.Services
{
    public class ArtistDataService : BaseDataService<Artist>
    {
        public ArtistDataService(AppDbContext appContext) : base(appContext)
        {
        }

        public override IEnumerable<Artist> GetAll()
        {
            var res = _db.Artists.ToList();
            return res;
        }

        public override Artist Create(Artist entity)
        {
            var res = _db.Artists.Add(entity);
            _db.SaveChanges();
            return res.Entity;
        }

        public override Artist Read(int id)
        {
            return _db.Artists.Find(id);
        }

        public override Artist Update(Artist entity)
        {
            _db.Artists.Update(entity);
            _db.SaveChanges();
            return entity;
        }

        public override bool Delete(Artist entity)
        {
            _db.Artists.Remove(entity);
            _db.SaveChanges();
            return true;
        }
    }
}
