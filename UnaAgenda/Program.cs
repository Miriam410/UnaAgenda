using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnaAgenda
{
    //Conocer los caminos de ejecución del sistema => casos de prueba
    //  - Alta de persona válida
    //      - DNI inválido (línea en blanco)
    //      - DNI inválido (no numérico) 
    //      - DNI inválido (< mínimo)
    //      - DNI inválido (> máximo)
    //      - Apellido en blanco
    //      - Apellido con números
    //      - etc...

    //  - Baja de una persona existente
    //  - Modificación de una persna con datos válidos
    //  - Búsqueda de una persona existente

    internal class Program
    {
        static void Main(string[] args)
        {
            //AGENDA: 
            // - Almacenar una cantidad indeterminada de personas en archivos de texto
            // - Guardar, de cada persona: DNI (único), Apellido, Nombre, Dirección, Teléfono, Fecha de Nacimiento
            // - Baja (borrar) personas
            // - Modificar una persona existente
            // - Buscar por nombre y/o apellido

            bool salir = false;
            do
            {
                Console.WriteLine();
                Console.WriteLine("MENU PRINCIPAL");
                Console.WriteLine("-------------");

                Console.WriteLine("1 - Alta");
                Console.WriteLine("2 - Modificar");
                Console.WriteLine("3 - Baja");
                Console.WriteLine("4 - Buscar");
                Console.WriteLine("9 - Salir");

                Console.WriteLine("Ingrese una opción y presione [Enter]");
                var opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        Alta();
                        break;

                    case "2":
                        Modificar();
                        break;
                    case "3":
                        Baja();
                        break;
                    case "4":
                        Buscar();
                        break;
                    case "5":
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("No ha ingresado una opción del menú");
                        break;
                }
            } while (!salir);
        }

        // 1- Alta
        private static void Alta()
        {
            var persona = Persona.IngresarNueva();
            Agenda.Agregar(persona);
        }
        
        // 2- Modificar
        private static void Modificar()
        {
            var persona = Agenda.Seleccionar();
            if (persona == null)
            {
                return;
            }
            persona.Mostrar();
            persona.Modificar();
        }
        
        // 3- Baja
        private static void Baja()
        {
            var persona = Agenda.Seleccionar();
            if (persona == null)
            {
                return;
            }
            persona.Mostrar();
            Console.WriteLine($"Se dispone a dar de baja a {persona.TituloEntrada}. Está ud. seguro? S/N");
            var key = Console.ReadKey(intercept: true);
            if (key.Key == ConsoleKey.S)
            {
                Agenda.Baja(persona);
                Console.WriteLine($"{persona.TituloEntrada} ha sido dada de baja");
            }
        }
        
        // 4- Buscar
        private static void Buscar()
        {
            var persona = Agenda.Seleccionar();
            persona?.Mostrar();
        } 
    }
}
