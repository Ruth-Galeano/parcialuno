using Repository.Data;
using System.Text.RegularExpressions;

namespace Service
{
    public class ClienteService
    {
        private ClienteRepository clienteRepository;

        public ClienteService(string connectionString)
        {
            this.clienteRepository = new ClienteRepository(connectionString);
        }

        public string insertarCliente(ClienteModel cliente)
        {
            return validarDatosCliente(cliente) ? clienteRepository.add(cliente) : throw new Exception("Error en la validación");
        }

        public string modificarCliente(ClienteModel cliente, int id)
        {
            if (clienteRepository.get(id) != null)
                return validarDatosCliente(cliente) ?
                  clienteRepository.update(cliente, id) :
                    throw new Exception("Error en la validación");
            else
                return "No se encontraron los datos de este cliente";
        }
        public string remove(int id)
        {
            return clienteRepository.remove(id);
        }

        public ClienteModel consultarCliente(int id)
        {
            return clienteRepository.get(id);
        }

        public IEnumerable<ClienteModel> list()
        {
            return clienteRepository.list();
        }

        private bool validarDatosCliente(ClienteModel cliente)
        {
            if (cliente.Nombre.Trim().Length < 3)
            {
                return false;
            }

            if (cliente.Apellido.Trim().Length < 3)
            {
                return false;
            }

            if (cliente.Documento.Trim().Length < 3)
            {
                return false;
            }

            if(!string.IsNullOrEmpty(cliente.Celular) && (cliente.Celular.Length != 10 || !Regex.IsMatch(cliente.Celular, @"^\d+$")))
            {
                return false;
            }
            return true;
        }
    }
}
