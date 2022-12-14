using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DB.Models;

namespace DB.Services
{
    public class ArtistDataService : DataServiceBase<Artist>
    {
        public ArtistDataService(AppDbContext appContext) : base(appContext)
        {
        }

        public override IEnumerable<string> GetAllVisible()
        {
            var res = _db.Artists.Select(a => a.ArtistName).ToList();
            return res;
        }

        public override IEnumerable<Artist> GetAll()
        {
            var res = _db.Artists.ToList();
            return res;
        }

        public override void Create(IModel entity)
        {
            var res = _db.Artists.Add((Artist)entity);
            _db.SaveChanges();
        }

        public override Artist Read(int id)
        {
            return _db.Artists.Find(id);
        }

        public override void Update(IModel entity)
        {
            _db.Artists.Update((Artist)entity);
            _db.SaveChanges();
        }

        public override void Delete(IModel entity)
        {
            _db.Artists.Remove((Artist)entity);
            _db.SaveChanges();
        }
    }
}
