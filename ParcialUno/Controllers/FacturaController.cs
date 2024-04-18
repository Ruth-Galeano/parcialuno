using Microsoft.AspNetCore.Mvc;
using Repository.Data;
using Service;

namespace ParcialUno.Controllers
{
    [ApiController]
    [Route("api/parcialuno/[controller]")]
    public class FacturaController : Controller
    {
        private FacturaService facturaServices;
        private IConfiguration configuration;


        public FacturaController(IConfiguration _configuration)
        {
            this.configuration = _configuration;
            this.facturaServices = new FacturaService(configuration.GetConnectionString("postgresDB"));
        }

        [HttpGet("listarFactura")]
        public ActionResult<List<FacturaModel>> ListarFactura()
        {
            var resultado = facturaServices.list();
            return Ok(resultado);
        }
        [HttpGet("consultarFactura/{id}")]
        public ActionResult<FacturaModel> ConsultarFactura(int id)
        {
            var resultado = this.facturaServices.consultarFactura(id);
            return Ok(resultado);
        }
        [HttpPost("insertarFactura")]
        public ActionResult<string> insertarFactura(FacturaModel modelo)
        {
            var resultado = this.facturaServices.insertarFactura(new FacturaModel
            {
                IdCliente = modelo.IdCliente,
                NroFactura = modelo.NroFactura,
                FechaHora = modelo.FechaHora,
                Total = modelo.Total,
                TotalIva5 = modelo.TotalIva5,
                TotalIva10 = modelo.TotalIva10,
                TotalIva = modelo.TotalIva10,
                TotalLetras = modelo.TotalLetras,
                Sucursal = modelo.Sucursal,
                

            });
            return Ok(resultado);
        }
        [HttpPut("modificarFactura/{id}")]
        public ActionResult<string> modificarFactura(FacturaModel modelo, int id)
        {
            var resultado = this.facturaServices.modificarFactura(new FacturaModel
            {
                IdCliente = modelo.IdCliente,
                NroFactura = modelo.NroFactura,
                FechaHora = modelo.FechaHora,
                Total = modelo.Total,
                TotalIva5 = modelo.TotalIva5,
                TotalIva10 = modelo.TotalIva10,
                TotalIva = modelo.TotalIva10,
                TotalLetras = modelo.TotalLetras,
                Sucursal = modelo.Sucursal,
            }, id);
            return Ok(resultado);
        }
        [HttpDelete("eliminarFactura/{id}")]
        public ActionResult<string> eliminarFactura(int id)
        {
            var resultado = this.facturaServices.remove(id);
            return Ok(resultado);
        }
    }
}
