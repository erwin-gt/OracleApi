using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oracle.DataAccess.Modelos;
using Microsoft.EntityFrameworkCore;
using Oracle.Services;
using OracleApi.WebApi.Mappings;
using Oracle.DTO;

namespace OracleApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
    

        private readonly IUsuarioService _servicio;
        
        public UsuarioController(IUsuarioService servicio)
        {
            
            _servicio = servicio;
        }



        // Lista la informacion de los usuarios
        [HttpGet]
        public async Task<ActionResult<List<UsuarioDTO>>> Listar()
        {
            {
                /*  Utilizado directamente por la conexion a la DB
                 * 
                            return await _context.Usuarios.ToListAsync();
                */
            }

            //Utilizado por el servicio creado IUsuarioService

            var retorno = await _servicio.Listar();

            //validacion del servicio
            if (retorno.Objeto != null)
                // return retorno.Objeto; Sin aplicar el uso del servicio
                return retorno.Objeto.Select(Mapper.ToDTO).ToList();
            else
                return StatusCode(retorno.Status, retorno.Error);

           
        }


        // Lista la informacion de los usuarios segun la ID ingresado
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDTO>> BuscarPorId(decimal id)
        {
            { 
            /* UTILIZADO DIRECTAMENTE A LA BASE DE DAT0S
             * 
            var retorno = await _context.Usuarios.FirstOrDefaultAsync(x => x.UserId == id);

            if (retorno != null)
                return retorno;
            else
                return NotFound(); */
            }

            var retorno = await _servicio.BuscarPorId(id);

            //validacion del servicio
            if (retorno.Objeto != null)
                return retorno.Objeto.ToDTO();
            else
                return StatusCode(retorno.Status, retorno.Error);

        }


        // Ingresa de datos
        [HttpPost]
        public async Task<ActionResult<UsuarioDTO>> Guardar(UsuarioDTO u)
        {
            {/* Uso anterior
            try
            {
                await _context.Usuarios.AddAsync(u);
                await _context.SaveChangesAsync();
                u.UserId = await _context.Usuarios.MaxAsync(u => u.UserId);

                return u;
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, "Se encontró un error");
            }*/
            }

            var retorno = await _servicio.Guardar(u.ToDatabase());

            if (retorno.Objeto != null)
                return retorno.Objeto.ToDTO();
            else
                return StatusCode(retorno.Status, retorno.Error);

        }


        // Actualiza datos
        [HttpPut]
        public async Task<ActionResult<UsuarioDTO>> Actualizar(UsuarioDTO u)
        {
            /* USO ANTERIOR HACIA LA BASE DE DATOS
             * 
            if (u == null || u.UserId == 0)
                return BadRequest("Faltan datos");

            Usuario cat = await _context.Usuarios.FirstOrDefaultAsync(x => x.UserId == u.UserId);

            if (cat == null)
                return NotFound();

            try
            {
                cat.Pnombre = u.Pnombre;
                _context.Usuarios.Update(cat);
                await _context.SaveChangesAsync();

                return cat;
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, "Se encontró un error");
            }*/

            var retorno = await _servicio.Actualizar(u.ToDatabase());

            if (retorno.Objeto != null)
                return retorno.Objeto.ToDTO();
            else
                return StatusCode(retorno.Status, retorno.Error);
        }


        // Elimina segun el ID ingresado
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Eliminar(decimal id)
        {
            /*USO ANTERIOR HACIA LA BASE DE DATOS
            Usuario cat = await _context.Usuarios.FirstOrDefaultAsync(x => x.UserId == id);

            if (cat == null)
                return NotFound();

            try
            {
                _context.Usuarios.Remove(cat);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, "Se encontró un error");
            }*/

            var retorno = await _servicio.Eliminar(id);

            if (retorno.Exito)
                return true;
            else
                return StatusCode(retorno.Status, retorno.Error);
        }
    }
}
