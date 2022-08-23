using Fiap.Api.AspNet3.Models;

namespace Fiap.Api.AspNet3.Repository.Interface
{
    public interface IMarcaRepository
    {
        public IList<MarcaModel> FindAll();
        public IList<MarcaModel> FindAll(int pagina, int tamanho);
        public int Count();
        public MarcaModel FindById(int id);
        public int Insert(MarcaModel marcaModel);
        public void Delete(int id);
        public void Update(MarcaModel marcaModel);
    }
}
