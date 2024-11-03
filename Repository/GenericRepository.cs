using EventTicketBookingApi.Data;
using hotelListing.API.Contract;
using Microsoft.EntityFrameworkCore;

namespace EventTicketBookingApi.Repository
{
    public class GenericRepository<T>: hotelListing.API.Contract.IGenericRepository<T> where T : class
    {
        private readonly EventTicketBookingDbContext _context;
        public GenericRepository(EventTicketBookingDbContext context)
        {
            this._context = context;
        }

        public async Task<T> AddAsync(T entity)
        {
            //await _context.AddAsync(entity);
            //await _context.SaveChangesAsync();
            //return entity;
            try
            {
                await _context.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                // Log the error or throw a more meaningful exception
                Console.WriteLine($"Error: {ex.Message} - Inner Exception: {ex.InnerException?.Message}");

                throw new InvalidOperationException("An error occurred while saving the entity.", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetAsync(id);
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Exists(int id)
        {
            var entity = await GetAsync(id);
            return entity != null;
        }

        public async Task<List<T>> GetAllAsync()
        {

            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetAsync(int? id)
        {
            if (id is null)
            {
                return null;
            }
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }


    }

}
