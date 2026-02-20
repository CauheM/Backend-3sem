using FilmesMoura.WebAPI.BdContextFilme;
using FilmesMoura.WebAPI.interfaces;
using FilmesMoura.WebAPI.Models;

namespace FilmesMoura.WebAPI.Repositories
{
    public class GeneroRepository : IGeneroRepository
    {
        private readonly FilmeContext _context;

        public GeneroRepository(FilmeContext context)
        {
            _context = context; 
        }

        public void AtualizarIdCorpo(Genero generoatualizado)
        {
            throw new NotImplementedException();
        }

        public void AtualizarIdURL(Guid id, Genero genero)
        {
            throw new NotImplementedException();
        }

        public Genero BuscarPorId(Guid id)
        {
            try
            {
                Genero generoBuscado = _context.Generos.Find(id.ToString())!;
                return generoBuscado;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void Cadastrar(Genero novoGenero)
        {
            try
            {
                novoGenero.Idgenero = Guid.NewGuid().ToString();
                _context.Generos.Add(novoGenero);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Deletar(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Genero> Listar()
        {
            try
            {
                List<Genero> listaGeneros = _context.Generos.ToList();
                return listaGeneros;
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
