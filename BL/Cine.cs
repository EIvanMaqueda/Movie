using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Cine
    {
      
        public static ML.Result GetAll() { 
        
            ML.Result result = new ML.Result();
            try
            {
                using (DL.EmaquedaMovieContext context=new DL.EmaquedaMovieContext())
                {
                    var query = context.Cines.FromSqlRaw($"CineGetAll").ToList();
                    if (query!=null)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in query)
                        {
                            ML.Cine cine=new ML.Cine();
                            cine.IdCine= obj.IdCine;
                            cine.Venta = obj.Venta.Value;
                            cine.Descripcion = obj.Descripcion;
                            cine.Latitud = obj.Latitud;
                            cine.Longitud = obj.Longitud;
                            cine.Zona=new ML.Zona();
                            cine.Zona.IdZona = obj.IdZona.Value;
                            cine.Zona.Nombre = obj.NombreZona;

                            result.Objects.Add(cine);
                        }
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {

                result.Correct = false;
                result.Message= ex.Message;
            }
            return result;
        }
    }
}
