using Microsoft.AspNetCore.Mvc;
using Repository.Data;
using Service;

namespace ParcialUno.Controllers
{
    [ApiController]
    [Route("api/parcialuno/[controller]")]
    public class ClienteController : Controller
    {
        private ClienteService clienteServices;
        private IConfiguration configuration;

        public ClienteController(IConfiguration _configuration)
        {
            this.configuration = _configuration;
            this.clienteServices = new ClienteService(configuration.GetConnectionString("postgresDB"));
        }

        [HttpGet("listarCliente")]
        public ActionResult<List<ClienteModel>> ListarCliente()
        {
            var resultado = clienteServices.list();
            return Ok(resultado);
        }
        [HttpGet("consultarCliente/{id}")]
        public ActionResult<ClienteModel> ConsultarCliente(int id)
        {
            var resultado = this.clienteServices.consultarCliente(id);
            return Ok(resultado);
        }
        [HttpPost("insertarCliente")]
        public ActionResult<string> insertarCliente(ClienteModel modelo)
        {
            var resultado = this.clienteServices.insertarCliente(new ClienteModel
            {
                Nombre = modelo.Nombre,
                Apellido = modelo.Apellido,
                Documento = modelo.Documento,
                Direccion = modelo.Direccion,
                Mail = modelo.Mail,
                Celular = modelo.Celular,
                Estado = modelo.Estado,
                IdBanco = modelo.IdBanco
            });
            return Ok(resultado);
        }
        [HttpPut("modificarCliente/{id}")]
        public ActionResult<string> modificarCliente(ClienteModel modelo, int id)
        {
            var resultado = this.clienteServices.modificarCliente(new ClienteModel
            {
                Nombre = modelo.Nombre,
                Apellido = modelo.Apellido,
                Documento = modelo.Documento,
                Direccion = modelo.Direccion,
                Mail = modelo.Mail,
                Celular = modelo.Celular,
                Estado = modelo.Estado,
                IdBanco = modelo.IdBanco
            }, id);
            return Ok(resultado);
        }
        [HttpDelete("eliminarCliente/{id}")]
        public ActionResult<string> eliminarCliente(int id)
        {
            var resultado = this.clienteServices.remove(id);
            return Ok(resultado);
        }
    }
}
