using Fiap.Api.AspNet3.Models;

namespace Fiap.Api.AspNet3.Repository.Interface
{
    public interface IUsuarioRepository
    {
        public IList<UsuarioModel> FindAll();
        public UsuarioModel FindById(int id);
        public UsuarioModel FindByName(string name);
        public IList<UsuarioModel> FindByRegra(string regra);
        public int Insert(UsuarioModel usuarioModel);
        public void Delete(int id);
        public void Update(UsuarioModel usuarioModel);
    }
}
