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

        public static ML.Result Add(ML.Cine cine) {

            ML.Result result = new ML.Result();
            try
            {
                using (DL.EmaquedaMovieContext context = new DL.EmaquedaMovieContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"CineAdd '{cine.Latitud}','{cine.Longitud}'" +
                        $",'{cine.Descripcion}',{cine.Venta},{cine.Zona.IdZona}");
                    if (query>0)
                    {
                        result.Correct =  true;
                        result.Message = "Cine Agregado Correctamente";
                    }
                }
            }
            catch (Exception ex)
            {

                result.Correct = true;
                result.Message = ex.Message;
            }
            return result;
        }

        public static ML.Result Update(ML.Cine cine) { 
        
            ML.Result result= new ML.Result();
            try
            {
                using (DL.EmaquedaMovieContext context=new DL.EmaquedaMovieContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"CineUpdate '{cine.Latitud}','{cine.Longitud}'" +
                        $",'{cine.Descripcion}',{cine.Venta},{cine.Zona.IdZona},{cine.IdCine}");
                    if (query>0)
                    {
                        result.Message = "Cine Actualizado Correctamente";
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {

                result.Message= ex.Message;
                result.Correct = false;
            }
            return result;
        }

        public static ML.Result GetById(int IdCine) { 
        
            ML.Result result= new ML.Result();
            try
            {
                using (DL.EmaquedaMovieContext context=new DL.EmaquedaMovieContext())
                {
                    var query = context.Cines.FromSqlRaw($"CineGetById {IdCine}").AsEnumerable().FirstOrDefault();
                    if (query!=null)
                    {
                        ML.Cine cine = new ML.Cine();
                        cine.IdCine = query.IdCine;
                        cine.Latitud = query.Latitud;
                        cine.Longitud = query.Longitud;
                        cine.Venta = query.Venta.Value;
                        cine.Descripcion = query.Descripcion;
                        cine.Zona = new ML.Zona();
                        cine.Zona.IdZona = query.IdZona.Value;
                        cine.Zona.Nombre = query.NombreZona;
                        result.Object = cine;

                        result.Correct = true;  
                 

                    }
                }
            }
            catch (Exception ex)
            {

                result.Message = ex.Message;
                result.Correct= false;
            }
            return result;
        }
        public static ML.Result Delete(int IdCine) { 
        
            ML.Result result = new ML.Result();
            try
            {
                using (DL.EmaquedaMovieContext context=new DL.EmaquedaMovieContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"CineDelete {IdCine}");
                    if (query>0)
                    {
                        result.Correct = true;
                        result.Message = "Cine Eliminado Exitosamente";
                    }
                }
            }
            catch (Exception ex)
            {

                result.Message = ex.Message;
                result.Correct=false;
            }
            return result;
        }
    }
}
