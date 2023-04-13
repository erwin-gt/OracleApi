using Oracle.DataAccess.Modelos;
using Oracle.DTO;

namespace OracleApi.WebApi.Mappings
{
    public static partial class Mapper
    {
        public static UsuarioDTO ToDTO(this Usuario model) //Convierte de ModelContext a DTO 
        {
            return new UsuarioDTO()
            {
                Id = model.UserId,
                Pname = model.Pnombre,
                Sname = model.Snombre,
                Tname = model.Tnombre,
                Papellido = model.Papellido,
                Sapellido = model.Sapellido
            };
        }
    }

    public static partial class Mapper 
    { 
        public static Usuario ToDatabase(this UsuarioDTO dto) // Converte de DTO a ModelContext
        {
            return new Usuario()
            {
                UserId = dto.Id,
                Pnombre = dto.Pname,
                Snombre = dto.Sname,
                Tnombre = dto.Tname,
                Papellido = dto.Papellido,
                Sapellido = dto.Sapellido,
                
               
               
            };
        }
    }
}

