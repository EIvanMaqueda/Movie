using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Usuario
    {
        public static ML.Result Login(ML.Usuario usuario) { 
        
            ML.Result result = new ML.Result();
            try
            {
                using (DL.EmaquedaMovieContext context=new DL.EmaquedaMovieContext())
                {
                    var query = context.Usuarios.FromSqlRaw($"Login '{usuario.UserName}','{usuario.Password}'").AsEnumerable().FirstOrDefault();
                    if (query!=null)
                    {
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {

               result.Correct = false;
                result.Message = ex.Message;
            }
            return result;
        }

    }
}