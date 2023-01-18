using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alura.ListaLeitura.App.Negocio
{
    public class ListaDeLeitura
    {
        private List<Livro> _livros;

        public ListaDeLeitura(string titulo, params Livro[] livros)
        {
            Titulo = titulo;
            _livros = livros.ToList();
            _livros.ForEach(l => l.Lista = this);
        }

        public string Titulo { get; set; }
        public IEnumerable<Livro> Livros
        {
            get { return _livros; }
        }

        public override string ToString()
        {
            var linhas = new StringBuilder();
            linhas.AppendLine(Titulo);
            linhas.AppendLine("=========");
            foreach (var livro in Livros)
            {
                linhas.AppendLine(livro.ToString());
            }
            return linhas.ToString();
        }
    }
}
