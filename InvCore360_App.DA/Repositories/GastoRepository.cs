using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InvCore360_App.Model.Models;
using InvCore360_App.DA.Data;

namespace InvCore360_App.DA.Repositories
{
    public class GastoRepository
    {
        private readonly DataContext _context;
        public GastoRepository(DataContext context) => _context = context;

        public async Task<IEnumerable<Gasto>> GetAllAsync() => await _context.Gastos.ToListAsync();

        public async Task<Gasto?> GetByIdAsync(int id) => await _context.Gastos.FindAsync(id);

        public async Task<Gasto> AddAsync(Gasto entity)
        {
            _context.Gastos.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Gasto?> UpdateAsync(Gasto entity)
        {
            var existing = await _context.Gastos.FindAsync(entity.GastoID);
            if (existing == null) return null;
            _context.Entry(existing).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Gastos.FindAsync(id);
            if (existing == null) return false;
            _context.Gastos.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}