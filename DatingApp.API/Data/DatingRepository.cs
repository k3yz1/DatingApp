using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.API.Model;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class DatingRepository : IDatingRepository
    {
        private DataContext _contex;

        public DatingRepository(DataContext context)
        {
            _contex = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _contex.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _contex.Remove(entity);
        }

        public async Task<User> GetUser(int id)
        {
            return await _contex.Users.Include(p=> p.Photos).FirstOrDefaultAsync(x=> x.Id == id);
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _contex.Users.Include(p=> p.Photos).ToListAsync();
        }

        public async Task<bool> SaveAll()
        {
            return await _contex.SaveChangesAsync() > 0;
        }
    }
}