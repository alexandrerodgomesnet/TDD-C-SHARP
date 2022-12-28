using CursoOnline.Dominio.UseCases;

namespace CursoOnline.Dominio.Contracts
{
    public interface ICursoRepositorio
    {
        void Inserir(Curso curso);
        Curso ObterCursoPeloNome(string nome);
    }
}