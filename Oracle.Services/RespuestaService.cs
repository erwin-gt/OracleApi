using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Clase utilizada para la creacion se servicios para no hacer los llamados directos a la base de datos


namespace Oracle.Services
{
    public class RespuestaService<T>
    {
        public RespuestaService()
        {
            Status = 200;
        }

        public T? Objeto { get; set; }
        public string? Error { get; set; }
        public int Status { get; set; }
        public bool Exito { get; set; }


        public void AgregarBadRequest(string mensaje)
        {
            Status = 400;
            Error = mensaje;
        }

        public void AgregarInternalServerError(string mensaje)
        {
            Status = 500;
            Error = mensaje;
        }
    }
}
