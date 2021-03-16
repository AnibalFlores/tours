using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Viajes365RestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BackendInfoController : ControllerBase
    {
        private readonly ILogger<BackendInfoController> _logger;

        public BackendInfo Info { get; }

        public BackendInfoController(ILogger<BackendInfoController> logger)
        {
            _logger = logger;
            Info = new BackendInfo();
        }

        [HttpGet]
        public ContentResult Index()
        {
            var Content = "<!DOCTYPE html><html><head><meta charset='UTF-8'><title>Backend Viajes 365º</title></head><body><h3>Backend Ejecutandose</h3><h4>Resumen:</h4><ul>";
            Content += "<li> Plataforma: "+ Info.OsPlatform;
            Content += "</li><li> Framework: " + Info.framework;
            Content += "</li><li> Título de Ensamblado: " + Info.Title;
            Content += "</li><li> Nombre del Producto: " + Info.Product;
            Content += "</li><li> Copyright: " + Info.Copyright;
            Content += "</li><li> Descripción: " + Info.Description;
            Content += "</li><li> Compania: " + Info.Company;
            Content += "</li><li> Versión: " + Info.Version;
            Content += "</li><li> Endpoints: <a href='./swagger/'>Ver con Swagger</a>";
            Content += "</li></ul></body></html>";
            
            return new ContentResult
            {
                ContentType = "text/html",
                StatusCode = (int)HttpStatusCode.OK,
                Content = Content
            };
        }
    }
}
