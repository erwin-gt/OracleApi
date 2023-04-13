using Microsoft.EntityFrameworkCore;
using Oracle.DataAccess.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oracle.Services
{
    public class UsuarioService : IUsuarioService
    {
        private ModelContext _context;

        public UsuarioService(ModelContext contect)
        {
            _context = contect;   
        }


        public async Task<RespuestaService<Usuario>> Actualizar(Usuario u)
        {
            var resp = new RespuestaService<Usuario>();
            var user = await _context.Usuarios.FirstOrDefaultAsync(x => x.UserId == u.UserId);

            if (user == null)
                resp.AgregarBadRequest("ID recibido no esta registrado");
            else
                user.Pnombre = u.Pnombre;
                user.Snombre = u.Snombre;
                user.Tnombre = u.Tnombre;
                user.Papellido = u.Papellido;
                user.Sapellido = u.Sapellido;
            try
            {
                _context.Usuarios.Update(user);
                await _context.SaveChangesAsync();

                resp.Objeto = user;
            }
            catch (DbUpdateException ex) 
            {
                resp.AgregarInternalServerError(ex.Message);            
            }

            return resp;

        }

        public  async Task<RespuestaService<Usuario>> BuscarPorId(decimal id)
        {
            var resp = new RespuestaService<Usuario>();
            var user = await _context.Usuarios.FirstOrDefaultAsync(x => x.UserId == id);

            // valida la existencia del ID del usuario
            if (user == null)
                resp.AgregarBadRequest("ID ingresado no esta registrado");
            else
                resp.Objeto = user;
            return resp;
        }

        public async Task<RespuestaService<bool>> Eliminar(decimal id)
        {
            var resp = new RespuestaService<bool>();
            var user = await _context.Usuarios.FirstOrDefaultAsync(x => x.UserId == id);

            if (user == null)
                resp.AgregarBadRequest("ID ingresado no esta registrado");
            else
            {
                try
                {
                    _context.Usuarios.Remove(user);
                    await _context.SaveChangesAsync();
                    resp.Exito = true;
                }
                catch(DbUpdateException ex)
                {
                    resp.AgregarInternalServerError(ex.Message);
                }
            }

            return resp;

        }

        public async Task<RespuestaService<Usuario>> Guardar(Usuario u)
        {
            var resp = new RespuestaService<Usuario>();

            try
            {
                await _context.Usuarios.AddAsync(u);
                await _context.SaveChangesAsync();
                u.UserId = await _context.Usuarios.MaxAsync(us => us.UserId);

                resp.Objeto = u;
            }
            catch (DbUpdateException ex)
            {
                resp.AgregarBadRequest(ex.Message);
            }

            return resp;
        }

        public async Task<RespuestaService<List<Usuario>>> Listar()
        {
            //implementa y despliega el resultado de la lista 
            var resp = new RespuestaService<List<Usuario>>();
            var lista = await _context.Usuarios.ToListAsync();

            if (lista != null)
                resp.Objeto = lista;
            else
                resp.AgregarInternalServerError("Se encontro un Erro");

            return resp;
        }
    }
}
