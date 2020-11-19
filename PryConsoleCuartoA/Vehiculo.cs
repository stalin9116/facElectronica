using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PryConsoleCuartoA
{
    public class Vehiculo
    {
        public int Codigo { get; set; }
        public string Placa { get; set; }
        public string Chasis { get; set; }
        public string Color { get; set; }
        public string Modelo { get; set; }
        public DateTime FechaMatricula { get; set; }
        public string Estado { get; set; }

        //Metodo Constructor
        public Vehiculo()
        {

        }

    }
}
