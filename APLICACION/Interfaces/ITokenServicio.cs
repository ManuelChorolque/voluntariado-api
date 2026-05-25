using DOMINIO.Entidades;

namespace APLICACION.Interfaces
{
    public interface ITokenServicio
    {
        string GenerarToken(Usuario usuario);
    }
}
