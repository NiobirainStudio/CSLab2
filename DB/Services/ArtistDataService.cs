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

        protected override object TransformById(object[] entity)
        {
            return new Artist { ArtistId = (int)entity[0], ArtistName = (string)entity[1] };
        }

        protected override object Transform(object[] entity)
        {
            return new Artist { ArtistName = (string)entity[0] };
        }

        public override IEnumerable<string> GetAllVisible()
        {
            var res = _db.Artists.Select(x => x.GetVisible()).ToList();
            return res;
        }

        public override IEnumerable<Artist> GetAll()
        {
            var res = _db.Artists.ToList();
            return res;
        }

        public override int GetIdByVisible(string data)
        {
            var res = _db.Artists.FirstOrDefault(x => x.ArtistName == data);
            return res.ArtistId;
        }



        public override void Create(object[] entity)
        {
            var res = _db.Artists.Add((Artist)Transform(entity));
            _db.SaveChanges();
        }

        public override object? Read(int id)
        {
            return _db.Artists.Find(id);
        }

        public override void Update(object[] entity)
        {
            var transformed = (Artist)TransformById(entity);

            var toUpdate = _db.Artists.FirstOrDefault(x => x.ArtistId == transformed.ArtistId);

            if (toUpdate != null)
            {
                toUpdate.ArtistName = transformed.ArtistName;

                _db.Artists.Update(toUpdate);
                _db.SaveChanges();
            }
        }

        public override void Delete(int id)
        {
            var toDelete = _db.Artists.FirstOrDefault(x => x.ArtistId == id);

            if (toDelete != null)
            {
                _db.Artists.Remove(toDelete);
                _db.SaveChanges();
            }
        }
    }
}
