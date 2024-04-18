using Dapper;
using System.Data;

namespace Repository.Data
{
    public class FacturaRepository : IFactura
    {
        private IDbConnection conexionDB;

        public FacturaRepository(string _connectionString)
        {
            conexionDB = new DbConection(_connectionString).dbConnection();
        }
        public string add(FacturaModel factura)
        {
            try
            {
                int insert = conexionDB.Execute("insert into factura (id_cliente, nro_factura, fecha_hora, total, total_iva5, total_iva10, total_iva, total_letras, sucursal) " +
                    "values(@idCliente, @nroFactura, @fechaHora, @total, @totalIva5, @totalIva10, @totalIva, @totalLetras, @sucursal)", factura);
                return (insert > 0) ? "Se inserto correctamente..." : "No se pudo insertar...";
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public FacturaModel get(int id)
        {
            try
            {
                return conexionDB.QueryFirst<FacturaModel>($"SELECT id, id_cliente as idCliente, nro_factura as nroFactura, fecha_hora as fechaHora, total, total_iva5 as totalIva5, total_iva10 as totalIva10, total_iva as totalIva, total_letras as totalLetras, sucursal from factura where id = {id}");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<FacturaModel> list()
        {
            try
            {
                return conexionDB.Query<FacturaModel>($"SELECT id, id_cliente as idCliente, nro_factura as nroFactura, fecha_hora as fechaHora, total, total_iva5 as totalIva5, total_iva10 as totalIva10, total_iva as totalIva, total_letras as totalLetras, sucursal from factura");
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
                int delete = conexionDB.Execute($"DELETE FROM factura WHERE id = {id}");
                return (delete > 0) ? "Se eliminó correctamente el registro..." : "No se pudo eliminar...";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string update(FacturaModel factura, int id)
        {
            try
            {
                int update = conexionDB.Execute($"UPDATE factura SET " +
                    "id_cliente  = @idCliente, " +
                    "nro_factura = @nroFactura, " +
                    "fecha_hora  = @fechaHora, " +
                    "total       = @total, " +
                    "total_iva5  = @totalIva5, " +
                    "total_iva10 = @totalIva10, " +
                    "total_iva   = @totalIva, " +
                    "total_letras= @totalLetras, " +
                    "sucursal    = @sucursal " +
                    $"WHERE id = {id}", factura);
                return (update > 0) ? "Se modificaron los datos correctamente..." : "No se pudo modificar...";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
