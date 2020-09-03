using System.Web.Http;
using WebActivatorEx;
using Api;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace Api
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {
                        c.IncludeXmlComments(string.Format(@"{0}\bin\Swagger.XML", System.AppDomain.CurrentDomain.BaseDirectory));
                        c.Schemes(new[] { "http", "https" });
                        c.SingleApiVersion("v1", "Documentação da API")
                        .Description("API de cadastro de Alunos e Turmas.")
                        .Contact(cc => cc
                            .Name("Matheus Zeitune")
                            .Email("matheus.zeitune.developer@gmail.com"));
                        c.ApiKey("JWT")
                               .Description("Para usar a API é necessario a autenticação usando JWT")
                               .Name("JWT")
                               .In("header");
                    })
                .EnableSwaggerUi(c =>
                    {
                        c.EnableApiKeySupport("Api-Key", "header");
                    });
        }
    }
}
