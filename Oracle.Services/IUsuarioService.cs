using Oracle.DataAccess.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oracle.Services
{
    public interface IUsuarioService
    {
        Task<RespuestaService<List<Usuario>>> Listar();
        Task<RespuestaService<Usuario>> BuscarPorId(decimal id);
        Task<RespuestaService<Usuario>> Guardar(Usuario u);
        Task<RespuestaService<Usuario>> Actualizar(Usuario u);
        Task<RespuestaService<bool>> Eliminar(decimal id);

    }
}
