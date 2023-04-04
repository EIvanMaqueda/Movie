using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Movie.Controllers
{
    public class MovieController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public MovieController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpGet]
        public IActionResult GetPopularMovies()
        {

            PL.Models.Movie movie = new PL.Models.Movie();
            try
            {
                using (var client = new HttpClient())
                {
                    string urlApi = _configuration["UrlApi"];
                    client.BaseAddress = new Uri(urlApi);

                    var responseTask = client.GetAsync("popular?api_key=4f6094a772c4c10e787eb59bf60f828e&language=es-ES&page=1");
                    responseTask.Wait();

                    var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {

                        var readTask = result.Content.ReadAsStringAsync();
                        dynamic ResultJson = JObject.Parse(readTask.Result.ToString());
                        readTask.Wait();
                        movie.Peliculas = new List<object>();
                        foreach (var resultItem in ResultJson.results)
                        {
                            PL.Models.Movie movieitem = new PL.Models.Movie();
                            movieitem.IdMovie = resultItem.id;
                            movieitem.Nombre = resultItem.title;
                            movieitem.Descripcion = resultItem.overview;
                            movieitem.Foto = "https://image.tmdb.org/t/p/w500/" + resultItem.poster_path;

                            movie.Peliculas.Add(movieitem);
                        }
                    }
                    // usuario.Usuarios = result.Objects;
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return View(movie);
        }
        [HttpGet]
        public IActionResult FavoriteMovie() {
            PL.Models.Movie movie = new PL.Models.Movie();
            try
            {
                using (var client = new HttpClient())
                {
                    string urlApi = _configuration["MovieFavorite"];
                   
                    
                    client.BaseAddress = new Uri(urlApi);

                    var responseTask = client.GetAsync("movies?api_key=4f6094a772c4c10e787eb59bf60f828e&session_id=0f0322e3459dbc569cba1b016e9b848b5c129783&language=es-ES&sort_by=created_at.asc&page=1");
                    responseTask.Wait();

                    var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {

                        var readTask = result.Content.ReadAsStringAsync();
                        dynamic ResultJson = JObject.Parse(readTask.Result.ToString());
                        readTask.Wait();
                        movie.Peliculas = new List<object>();
                        foreach (var resultItem in ResultJson.results)
                        {
                            PL.Models.Movie movieitem = new PL.Models.Movie();
                            movieitem.IdMovie = resultItem.id;
                            movieitem.Nombre = resultItem.title;
                            movieitem.Descripcion = resultItem.overview;
                            movieitem.Foto = "https://image.tmdb.org/t/p/w500/" + resultItem.poster_path;

                            movie.Peliculas.Add(movieitem);
                        }
                    }
                    // usuario.Usuarios = result.Objects;
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return View(movie);
        }

        [HttpGet]
        public IActionResult AddFavoriteMovie(int idMovie) {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["AddFavorite"]);
                PL.Models.Favoritos favorito= new PL.Models.Favoritos();
                favorito.media_type = "movie";
                favorito.media_id = idMovie.ToString();
                favorito.favorite = true;
                   
                //HTTP POST
                var postTask = client.PostAsJsonAsync("favorite?api_key=4f6094a772c4c10e787eb59bf60f828e&session_id=0f0322e3459dbc569cba1b016e9b848b5c129783", favorito);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Message = "Pelicula Agregada correctamente";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Message = "Error al agregar la pelicula";
                    return PartialView("Modal");
                }
            }
            return null;
        }

        [HttpGet]
        public IActionResult DeleteFavoriteMovie(int idMovie)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["AddFavorite"]);
                PL.Models.Favoritos favorito = new PL.Models.Favoritos();
                favorito.media_type = "movie";
                favorito.media_id = idMovie.ToString();
                favorito.favorite = false;

                //HTTP POST
                var postTask = client.PostAsJsonAsync("favorite?api_key=4f6094a772c4c10e787eb59bf60f828e&session_id=0f0322e3459dbc569cba1b016e9b848b5c129783", favorito);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Message = "Pelicula Agregada correctamente";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Message = "Error al agregar la pelicula";
                    return PartialView("Modal");
                }
            }
            return null;
        }
    }
}

