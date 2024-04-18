using Repository.Data;
using System.Text.RegularExpressions;

namespace Service
{
    public class FacturaService
    {
        private FacturaRepository facturaRepository;
        private ClienteRepository clienteRepository;
        public FacturaService(string connectionString)
        {
            this.facturaRepository = new FacturaRepository(connectionString);
            this.clienteRepository = new ClienteRepository(connectionString);
        }

        public string insertarFactura(FacturaModel factura)
        {
            return validarDatosFactura(factura) ? facturaRepository.add(factura) : throw new Exception("Error en la validación");
        }

        public string modificarFactura(FacturaModel factura, int id)
        {
            if (facturaRepository.get(id) != null)
                return validarDatosFactura(factura) ?
                  facturaRepository.update(factura, id) :
                    throw new Exception("Error en la validación");
            else
                return "No se encontraron los datos de esta factura";
        }
        public string remove(int id)
        {
            return facturaRepository.remove(id);
        }

        public FacturaModel consultarFactura(int id)
        {
            FacturaModel model = facturaRepository.get(id);
            model.Cliente = clienteRepository.get(model.IdCliente);
            return model;
        }

        public IEnumerable<FacturaModel> list()
        {
            IEnumerable<FacturaModel> facturas = facturaRepository.list();
            foreach (var factura in facturas)
            {
                factura.Cliente = clienteRepository.get(factura.IdCliente);
            }
            return facturas;
        }

        private bool validarDatosFactura(FacturaModel factura)
        {
            if (!Regex.IsMatch(factura.NroFactura, @"^\d{3}-\d{3}-\d{6}$"))
            {
                return false;
            }

            if (!Regex.IsMatch(factura.Total.ToString(), @"^\d+(\.\d+)?$"))
            {
                return false;
            }

            if (!Regex.IsMatch(factura.TotalIva5.ToString(), @"^\d+(\.\d+)?$"))
            {
                return false;
            }

            if (!Regex.IsMatch(factura.TotalIva10.ToString(), @"^\d+(\.\d+)?$"))
            {
                return false;
            }

            if (!Regex.IsMatch(factura.TotalIva.ToString(), @"^\d+(\.\d+)?$"))
            {
                return false;
            }

            if(factura.TotalLetras.Length < 6)
            {
                return false;
            }
            return true;
        }
    }
}
