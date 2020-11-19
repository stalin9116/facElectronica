using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PryConsoleCuartoA
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                List<Vehiculo> _listaVehiculos = new List<Vehiculo>();

                Vehiculo vehiculo = new Vehiculo();
                vehiculo.Codigo = 1;
                vehiculo.Placa = "PCW5425";
                vehiculo.Color = "Gris";
                vehiculo.Modelo = "Audi";
                vehiculo.FechaMatricula = DateTime.Now;
                vehiculo.Estado = "A";

                _listaVehiculos.Add(vehiculo);

                vehiculo = new Vehiculo();
                //vehiculo.Codigo = 2;
                //vehiculo.Placa = "PTR6570";
                //vehiculo.Color = "Amarillo";
                //vehiculo.Modelo = "Chevrolet";
                //vehiculo.FechaMatricula = Convert.ToDateTime("2020-11-18");
                //vehiculo.Estado = "I";

                Console.WriteLine("Ingrese codigo ");
                vehiculo.Codigo = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Ingrese Placa ");
                vehiculo.Placa = Console.ReadLine();
                Console.WriteLine("Ingrese Color ");
                vehiculo.Color = Console.ReadLine();
                Console.WriteLine("Ingrese Modelo ");
                vehiculo.Modelo = Console.ReadLine();
                Console.WriteLine("Ingrese Estado ");
                vehiculo.Estado = Console.ReadLine();

                _listaVehiculos.Add(vehiculo);

                foreach (var item in _listaVehiculos.Where(data=>data.Estado.Equals("A")))
                {
                    Console.WriteLine("Codigo: " + item.Codigo);
                    Console.WriteLine("Placa: " + item.Placa);
                    Console.WriteLine("Color: " + item.Color);
                    Console.WriteLine("Modelo: " + item.Modelo);
                }

                Console.ReadLine();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex.Message);
                //throw new ArgumentException("Error "+ ex.Message);
            }
        }
    }
}
