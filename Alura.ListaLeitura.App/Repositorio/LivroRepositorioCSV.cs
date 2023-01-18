using System;
using System.Collections.Generic;
using System.Text;
using Alura.ListaLeitura.App.Negocio;
using System.IO;
using System.Linq;

namespace Alura.ListaLeitura.App.Repositorio
{
    public class LivroRepositorioCSV : ILivroRepositorio
    {
        private static readonly string nomeArquivoCSV = "Repositorio\\livros.csv";

        private ListaDeLeitura _paraLer;
        private ListaDeLeitura _lendo;
        private ListaDeLeitura _lidos;

        public LivroRepositorioCSV()
        {
            var arrayParaLer = new List<Livro>();
            var arrayLendo = new List<Livro>();
            var arrayLidos = new List<Livro>();

            using (var file = File.OpenText(LivroRepositorioCSV.nomeArquivoCSV))
            {
                while (!file.EndOfStream)
                {
                    var textoLivro = file.ReadLine();
                    if (string.IsNullOrEmpty(textoLivro))
                    {
                        continue;
                    }
                    var infoLivro = textoLivro.Split(';');
                    var livro = new Livro
                    {
                        Id = Convert.ToInt32(infoLivro[1]),
                        Titulo = infoLivro[2],
                        Autor = infoLivro[3]
                    };
                    switch (infoLivro[0])
                    {
                        case "para-ler":
                            arrayParaLer.Add(livro);
                            break;
                        case "lendo":
                            arrayLendo.Add(livro);
                            break;
                        case "lidos":
                            arrayLidos.Add(livro);
                            break;
                        default:
                            break;
                    }
                }
            }

            _paraLer = new ListaDeLeitura("Para Ler", arrayParaLer.ToArray());
            _lendo = new ListaDeLeitura("Lendo", arrayLendo.ToArray());
            _lidos = new ListaDeLeitura("Lidos", arrayLidos.ToArray());
        }

        public ListaDeLeitura ParaLer => _paraLer;
        public ListaDeLeitura Lendo => _lendo;
        public ListaDeLeitura Lidos => _lidos;

        public IEnumerable<Livro> Todos => _paraLer.Livros.Union(_lendo.Livros).Union(_lidos.Livros);

        public void Incluir(Livro livro)
        {
            var id = Todos.Select(l => l.Id).Max();
            using (var file = File.AppendText(LivroRepositorioCSV.nomeArquivoCSV))
            {
                file.WriteLine($"para-ler;{id+1};{livro.Titulo};{livro.Autor}");
            }
        }
    }
}
