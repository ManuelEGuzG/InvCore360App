using System.Collections.Generic;
using System.Threading.Tasks;
using InvCore360_App.DA.Repositories;
using InvCore360_App.Model.Models;

namespace InvCore360_App.BL.Services
{
    public class CategoriaService
    {
        private readonly CategoriaRepository _repo;
        public CategoriaService(CategoriaRepository repo) => _repo = repo;

        public Task<IEnumerable<Categoria>> GetAllAsync() => _repo.GetAllAsync();
        public Task<Categoria?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
        public Task<Categoria> AddAsync(Categoria entity) => _repo.AddAsync(entity);
        public Task<Categoria?> UpdateAsync(Categoria entity) => _repo.UpdateAsync(entity);
        public Task<bool> DeleteAsync(int id) => _repo.DeleteAsync(id);
    }
}