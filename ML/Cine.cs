using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Cine
    {
        public int IdCine { get; set; }

        public string Latitud { get; set; }

        public string Longitud { get; set; }

        public string Descripcion { get; set; }

        public decimal Venta { get; set; }

        public ML.Zona Zona { get; set; }
        public List<object> Cines { get; set; }


    }
}
