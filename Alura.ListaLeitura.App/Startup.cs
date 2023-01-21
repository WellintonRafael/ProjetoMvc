using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Alura.ListaLeitura.App
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }

        public void Configure(IApplicationBuilder app)
        {
            var builder = new RouteBuilder(app);
            builder.MapRoute("Livros/Add", Add);
            builder.MapRoute("Livros/ParaLer", LivrosParaLer);
            builder.MapRoute("Livros/Lendo", LivrosLendo);
            builder.MapRoute("Livros/ParaLer", LivrosLidos);

            app.UseRouter(builder.Build());

            app.Run(Error);
        }

        public Task Error(HttpContext context)
        {
            context.Response.StatusCode = 404;
            return context.Response.WriteAsync("Rota inexistente");
        }

        public async Task Add(HttpContext context)
        {
            var _repo = new LivroRepositorioCSV();
            _repo.Incluir(new Negocio.Livro()
            {
                Autor = " RWELEAJDS",
                Titulo = Guid.NewGuid().ToString()
            });

            await context.Response.WriteAsync("BOA SAFADO REGISTRADO COM SUCESSO");
        }

        public async Task LivrosParaLer(HttpContext context)
        {
            var _repo = new LivroRepositorioCSV();

            await context.Response.WriteAsync(_repo.ParaLer.ToString());
        }

        public async Task LivrosLendo(HttpContext context)
        {
            var _repo = new LivroRepositorioCSV();

            await context.Response.WriteAsync(_repo.Lendo.ToString());
        }

        public async Task LivrosLidos(HttpContext context)
        {
            var _repo = new LivroRepositorioCSV();

            await context.Response.WriteAsync(_repo.Lidos.ToString());
        }
    }
}