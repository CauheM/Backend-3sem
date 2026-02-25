using FilmesMoura.WebAPI.BdContextFilme;
using FilmesMoura.WebAPI.interfaces;
using FilmesMoura.WebAPI.Models;
using System.Data;

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
            try
            {
                Genero generoBuscado = _context.Generos.Find(generoatualizado.Idgenero)!;
                if(generoBuscado != null)
                {
                    generoBuscado.Nome = generoatualizado.Nome;
                }

                _context.Generos.Update(generoBuscado!);
                _context.SaveChanges(); 
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void AtualizarIdURL(Guid id, Genero generoAtualizado)
        {
            try 
            {
                Genero generoBuscado = _context.Generos.Find(id.ToString())!;

                if(generoBuscado != null)
                {
                    generoBuscado.Nome = generoAtualizado.Nome;

                }

                _context.Generos.Update(generoBuscado!);
                _context.SaveChanges();
            }
            catch(Exception) 
            {
                throw;
            }
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
            try
            {
                Genero generobuscado = _context.Generos.Find(id.ToString())!;

                if(generobuscado != null)
                {
                    _context.Generos.Remove(generobuscado);
                }
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
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
