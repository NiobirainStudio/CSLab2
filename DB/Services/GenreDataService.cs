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

        protected override object TransformById(object[] entity)
        {
            return new Genre { GenreId = (int)entity[0], GenreName = (string)entity[1] };
        }

        protected override object Transform(object[] entity)
        {
            return new Genre { GenreName = (string)entity[0] };
        }

        public override IEnumerable<string> GetAllVisible()
        {
            var res = _db.Genres.Select(x => x.GenreName).ToList();
            return res;
        }

        public override IEnumerable<Genre> GetAll()
        {
            var res = _db.Genres.ToList();
            return res;
        }

        public override int GetIdByVisible(string data)
        {
            var res = _db.Genres.FirstOrDefault(x => x.GenreName == data);
            return res.GenreId;
        }



        public override void Create(object[] entity)
        {
            var res = _db.Genres.Add((Genre)Transform(entity));
            _db.SaveChanges();
        }

        public override object? Read(int id)
        {
            return _db.Genres.Find(id);
        }

        public override void Update(object[] entity)
        {
            var transformed = (Genre)TransformById(entity);

            var toUpdate = _db.Genres.FirstOrDefault(x => x.GenreId == transformed.GenreId);

            if (toUpdate != null)
            {
                toUpdate.GenreName = transformed.GenreName;

                _db.Genres.Update(toUpdate);
                _db.SaveChanges();
            }
        }

        public override void Delete(int id)
        {
            var toDelete = _db.Genres.FirstOrDefault(x => x.GenreId == id);

            if (toDelete != null)
            {
                _db.Genres.Remove(toDelete);
                _db.SaveChanges();
            }
        }
    }
}
