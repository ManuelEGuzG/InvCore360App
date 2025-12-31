using System.Collections.Generic;
using System.Threading.Tasks;
using InvCore360_App.DA.Repositories;
using InvCore360_App.Model.Models;

namespace InvCore360_App.BL.Services
{
    public class ConfiguracionNegocioService
    {
        private readonly ConfiguracionNegocioRepository _repo;
        public ConfiguracionNegocioService(ConfiguracionNegocioRepository repo) => _repo = repo;

        public Task<IEnumerable<ConfiguracionNegocio>> GetAllAsync() => _repo.GetAllAsync();
        public Task<ConfiguracionNegocio?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
        public Task<ConfiguracionNegocio> AddAsync(ConfiguracionNegocio entity) => _repo.AddAsync(entity);
        public Task<ConfiguracionNegocio?> UpdateAsync(ConfiguracionNegocio entity) => _repo.UpdateAsync(entity);
        public Task<bool> DeleteAsync(int id) => _repo.DeleteAsync(id);
    }
}