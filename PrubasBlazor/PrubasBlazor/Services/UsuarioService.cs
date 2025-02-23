using Microsoft.EntityFrameworkCore;
using PrubasBlazor.Data;
using PrubasBlazor.Data.Entities;

namespace PrubasBlazor.Services
{
    public interface IUsuariosService
    {
        Task<List<Usuario>> GetUsuarios();
        Task<Usuario?> GetUsuarioById(int id);
        Task<Usuario> AddUsuario(Usuario usuario);
        Task UpdateUsuario(Usuario usuario);
        Task DeleteUsuario(int id);
    }

    public class UsuarioService(ApplicationDbContext context) : IUsuariosService
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<List<Usuario>> GetUsuarios()
        {
            return await _context.usuarios.ToListAsync();
        }

        public async Task<Usuario?> GetUsuarioById(int id)
        {
            return await _context.usuarios.FindAsync(id);
        }

        public async Task <Usuario> AddUsuario(Usuario usuario)
        {
            if(await _context.usuarios.AnyAsync(u=>u.Id == usuario.Id))
            {
                throw new InvalidOperationException("Este Usuario ya existe");

            }
            _context.usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

   
        public async Task UpdateUsuario(Usuario usuario)
        {
           
            _context.usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUsuario(int id)
        {
            var usuario = await _context.usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
            }
        }
    }
}
