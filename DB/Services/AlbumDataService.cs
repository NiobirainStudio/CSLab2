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

        protected override object TransformById(object[] entity)
        {
            var res = new Album
            {
                AlbumId = Convert.ToInt32(entity[0]),
                AlbumName = Convert.ToString(entity[1]),
                ReleaseYear = Convert.ToInt32(entity[2]),
                ArtistId = Convert.ToInt32(entity[3]),
                GenreId = Convert.ToInt32(entity[4])
            };
            if (res.ReleaseYear > 0 && res.ReleaseYear <= DateTime.Now.Year)
                return res;
            throw new Exception("Incorrect year!");
        }

        protected override object Transform(object[] entity)
        {
            var res = new Album
            {
                AlbumName = Convert.ToString(entity[0]),
                ReleaseYear = Convert.ToInt32(entity[1]),
                ArtistId = Convert.ToInt32(entity[2]),
                GenreId = Convert.ToInt32(entity[3])
            };
            if (res.ReleaseYear > 0 && res.ReleaseYear <= DateTime.Now.Year)
                return res;
            throw new Exception("Incorrect year!");
        }

        public override IEnumerable<string> GetAllVisible()
        {
            var res = _db.Albums.Select(x => x.AlbumName).ToList();
            return res;
        }

        public override IEnumerable<object> GetAll()
        {
            var res = _db.Albums
                .Include(x => x.Genre)
                .Include(x => x.Artist)
                .Select(x => new { 
                    x.AlbumId, 
                    x.AlbumName, 
                    x.ReleaseYear,
                    x.ArtistId,
                    Artist = x.Artist.GetVisible(),
                    x.GenreId, 
                    Genre = x.Genre.GetVisible()
                }).ToList();

            return res;
        }

        public override int GetIdByVisible(string data)
        {
            var res = _db.Albums.FirstOrDefault(x => x.AlbumName == data);
            return res.AlbumId;
        }



        public override void Create(object[] entity)
        {
            var res = _db.Albums.Add((Album)Transform(entity));
            _db.SaveChanges();
        }

        public override object? Read(int id)
        {
            return _db.Albums.Find(id);
        }

        public override void Update(object[] entity)
        {
            var transformed = (Album)TransformById(entity);

            var toUpdate = _db.Albums.FirstOrDefault(x => x.AlbumId == transformed.AlbumId);

            if (toUpdate != null)
            {
                toUpdate.AlbumName = transformed.AlbumName;
                toUpdate.ReleaseYear = transformed.ReleaseYear;
                toUpdate.ArtistId = transformed.ArtistId;
                toUpdate.GenreId = transformed.GenreId;

                _db.Albums.Update(toUpdate);
                _db.SaveChanges();
            }
        }

        public override void Delete(int id)
        {
            var toDelete = _db.Albums.FirstOrDefault(x => x.AlbumId == id);

            if (toDelete != null)
            {
                _db.Albums.Remove(toDelete);
                _db.SaveChanges();
            }
        }
    }
}
