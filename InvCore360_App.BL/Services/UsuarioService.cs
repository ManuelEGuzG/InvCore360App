using System.Collections.Generic;
using System.Threading.Tasks;
using InvCore360_App.DA.Repositories;
using InvCore360_App.Model.Models;

namespace InvCore360_App.BL.Services
{
    public class UsuarioService
    {
        private readonly UsuarioRepository _repo;
        public UsuarioService(UsuarioRepository repo) => _repo = repo;

        public Task<IEnumerable<Usuario>> GetAllAsync() => _repo.GetAllAsync();
        public Task<Usuario?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
        public Task<Usuario> AddAsync(Usuario entity) => _repo.AddAsync(entity);
        public Task<Usuario?> UpdateAsync(Usuario entity) => _repo.UpdateAsync(entity);
        public Task<bool> DeleteAsync(int id) => _repo.DeleteAsync(id);
    }
}