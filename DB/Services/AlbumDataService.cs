using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DB.Models;

namespace DB.Services
{
    public class AlbumDataService : DataServiceBase<Album>
    {
        public AlbumDataService(AppDbContext appContext) : base(appContext)
        {
        }

        public override IEnumerable<string> GetAllVisible()
        {
            var res = _db.Albums.Select(a => a.AlbumName).ToList();
            return res;
        }

        public override IEnumerable<Album> GetAll()
        {
            var res = _db.Albums.ToList();
            return res;
        }

        public override void Create(IModel entity)
        {
            var res = _db.Albums.Add((Album)entity);
            _db.SaveChanges();
        }

        public override IModel Read(int id)
        {
            return _db.Albums.Find(id);
        }

        public override void Update(IModel entity)
        {
            _db.Albums.Update((Album)entity);
            _db.SaveChanges();
        }

        public override void Delete(IModel entity)
        {
            _db.Albums.Remove((Album)entity);
            _db.SaveChanges();
        }
    }
}
