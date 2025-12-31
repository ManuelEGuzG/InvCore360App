using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InvCore360_App.Model.Models;
using InvCore360_App.DA.Data;

namespace InvCore360_App.DA.Repositories
{
    public class ConfiguracionNegocioRepository
    {
        private readonly DataContext _context;
        public ConfiguracionNegocioRepository(DataContext context) => _context = context;

        public async Task<IEnumerable<ConfiguracionNegocio>> GetAllAsync() => await _context.ConfiguracionNegocio.ToListAsync();

        public async Task<ConfiguracionNegocio?> GetByIdAsync(int id) => await _context.ConfiguracionNegocio.FindAsync(id);

        public async Task<ConfiguracionNegocio> AddAsync(ConfiguracionNegocio entity)
        {
            _context.ConfiguracionNegocio.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<ConfiguracionNegocio?> UpdateAsync(ConfiguracionNegocio entity)
        {
            var existing = await _context.ConfiguracionNegocio.FindAsync(entity.ConfigID);
            if (existing == null) return null;
            _context.Entry(existing).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.ConfiguracionNegocio.FindAsync(id);
            if (existing == null) return false;
            _context.ConfiguracionNegocio.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}