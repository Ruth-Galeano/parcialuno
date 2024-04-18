using Dapper;
using System.Data;

namespace Repository.Data
{
    public class ClienteRepository: ICliente
    {
        private IDbConnection conexionDB;

        public ClienteRepository(string _connectionString)
        {
            conexionDB = new DbConection(_connectionString).dbConnection();
        }
        public string add(ClienteModel cliente)
        {
            try
            {
                int insert = conexionDB.Execute("insert into cliente(nombre, apellido, documento, direccion, mail, celular, estado, id_banco) " +
                    "values(@nombre, @apellido, @documento, @direccion, @mail, @celular, @estado, @idBanco)", cliente);
                return (insert > 0) ? "Se inserto correctamente..." : "No se pudo insertar...";
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public ClienteModel get(int id)
        {
            try
            {
                return conexionDB.QueryFirst<ClienteModel>($"SELECT id, nombre, apellido, documento, direccion, mail, celular, estado, id_banco as idBanco from cliente where id = {id}");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<ClienteModel> list()
        {
            try
            {
                return conexionDB.Query<ClienteModel>($"SELECT id, nombre, apellido, documento, direccion, mail, celular, estado, id_banco as idBanco from cliente");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string remove(int id)
        {
            try
            {
                int delete = conexionDB.Execute($"DELETE FROM cliente WHERE id = {id}");
                return (delete > 0) ? "Se eliminó correctamente el registro..." : "No se pudo eliminar...";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string update(ClienteModel cliente, int id)
        {
            try
            {
                int update = conexionDB.Execute($"UPDATE cliente SET " +
                    "nombre     = @nombre, " +
                    "apellido   = @apellido, " +
                    "documento  = @documento, " +
                    "direccion   = @direccion, " +
                    "mail       = @mail, " +
                    "celular    = @celular, " +
                    "estado     = @estado, " +
                    "id_banco   = @idBanco " +
                    $"WHERE id = {id}", cliente);
                return (update > 0) ? "Se modificaron los datos correctamente..." : "No se pudo modificar...";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
