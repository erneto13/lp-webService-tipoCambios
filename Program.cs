using System;
using System.Net;
using Newtonsoft.Json;

namespace AppTiposDeCambio
{
    class Program
    {
        static void Main(string[] args)
        {
            bool salir = false;
            Console.WriteLine("Consumo de un WebService de tipo de cambio\n");
            while (!salir)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("[1] Mostrar tipos de cambio");
                Console.WriteLine("[2] Verificar tipo de cambio \n    entre dos monedas");
                Console.WriteLine("[3] Cerrar el programa");
                Console.Write("\nElige una opción: ");

                string entrada = Console.ReadLine();

                switch (entrada)
                {
                    case "1":
                        MostrarTiposDeCambio();
                        break;
                    case "2":
                        VerificarTipoDeCambio();
                        break;
                    case "3":
                        salir = true;
                        break;
                    default:
                        System.Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nOpción inválida. Por favor, elige una opción válida.\n");
                        break;
                }
            }
        }

        static void MostrarTiposDeCambio()
        {
            string urlApi = "http://data.fixer.io/api/latest?access_key=6c96eece3101e76201404ace1e460776";

            using (WebClient clienteWeb = new WebClient())
            {
                try
                {
                    string json = clienteWeb.DownloadString(urlApi);
                    dynamic datos = JsonConvert.DeserializeObject(json);

                    if (datos.success == true)
                    {
                        Console.WriteLine("Tipos de cambio disponibles:\n");

                        foreach (var moneda in datos.rates)
                        {
                            Console.WriteLine($"Moneda {moneda.Name}: ${moneda.Value}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("La solicitud no fue exitosa. Mensaje de error: " + datos.error.info);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener los tipos de cambio: " + ex.Message);
                }
            }
            Console.WriteLine("");
        }

        static void VerificarTipoDeCambio()
        {
            Console.Write("Introduce la moneda base (por ejemplo, MXN): ");
            string monedaBase = Console.ReadLine().ToUpper();

            Console.Write("Introduce la moneda de destino (por ejemplo, USD): ");
            string monedaDestino = Console.ReadLine().ToUpper();

            string urlApi = $"http://data.fixer.io/api/latest?access_key=6c96eece3101e76201404ace1e460776&base={monedaBase}&symbols={monedaDestino}";

            using (WebClient clienteWeb = new WebClient())
            {
                try
                {
                    string json = clienteWeb.DownloadString(urlApi);
                    dynamic datos = JsonConvert.DeserializeObject(json);

                    if (datos.success == true)
                    {
                        decimal tipoCambio = datos.rates[monedaDestino];
                        Console.WriteLine($"El tipo de cambio de {monedaBase} a {monedaDestino} es: {tipoCambio}\n");
                    }
                    else
                    {
                        Console.WriteLine("La solicitud no fue exitosa. Mensaje de error: " + datos.error.info);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener el tipo de cambio: " + ex.Message);
                }
            }
        }
    }
}
    