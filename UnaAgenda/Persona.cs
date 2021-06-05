using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnaAgenda
{
    public class Persona
    {
        //Porpiedades de la clase PERSONA
        public int DNI { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaDeNacimiento { get; set; }

        // El título de entrada solo contiene: Apellido, Nombre - DNI
        public string TituloEntrada => $"{Apellido}, {Nombre} - DNI {DNI}";

        // Es un constructor vacio
        public Persona() {}
        
        // Es la linea de datos del archivo .txt
        public Persona (string linea)
        {
            var datos = linea.Split(';');
            DNI = int.Parse(datos[0]);
            Apellido = datos[1];
            Nombre = datos[2];
            Direccion = datos[3];
            Telefono = datos[4];
            FechaDeNacimiento = DateTime.Parse(datos[5]);
        }

        // Se va a obtener la linea de los datos del archivo .txt
        public string ObtenerLineaDatos() => $"{DNI};{Apellido};{Nombre};{Direccion};{Telefono};{FechaDeNacimiento}";

        // Ingresa una nueva PERSONA
        public static Persona IngresarNueva()
        {
            // Se crea una PERSONA
            var persona = new Persona();
            Console.WriteLine("Nueva persona");
            persona.DNI = IngresarDNI();
            persona.Apellido = Ingreso("Ingrese el apellido");
            persona.Nombre = Ingreso("Ingrese el nombre");
            persona.Direccion = Ingreso("Ingrese la dirección", permiteNumeros: true);
            persona.Telefono = Ingreso("Ingrese el teléfono", permiteNumeros: true);
            persona.FechaDeNacimiento = IngresarFecha("Ingrese la fecha de nacimiento");
            return persona;
        }

        
        public void Modificar()
        {
            // Modifica solo el APELLIDO
            Console.WriteLine($"Apellido: {Apellido} - S para modificar / Cualquier tecla para seguir");
            var tecla = Console.ReadKey(intercept: true);
            if(tecla.Key == ConsoleKey.S)
            {
                Apellido = Ingreso("Ingrese el nuevo apellido");
            }

            // Modifica solo el NOMBRE 
            Console.WriteLine($"Nombre: {Nombre} - S para modificar / Cualquier tecla para seguir");
            tecla = Console.ReadKey(intercept: true);
            if (tecla.Key == ConsoleKey.S)
            {
                Nombre = Ingreso("Ingrese el nuevo nombre");
            }

            // Modifica solo la DIRECCION 
            Console.WriteLine($"Direccion: {Direccion} - S para modificar / Cualquier tecla para seguir");
            tecla = Console.ReadKey(intercept: true);
            if (tecla.Key == ConsoleKey.S)
            {
                Direccion = Ingreso("Ingrese la nueva dirección", permiteNumeros: true);
            }

            // Modifica solo el TELEFONO
            Console.WriteLine($"Telefono: {Telefono} - S para modificar / Cualquier tecla para seguir");
            tecla = Console.ReadKey(intercept: true);
            if (tecla.Key == ConsoleKey.S)
            {
                Telefono = Ingreso("Ingrese el nuevo teléfono", permiteNumeros: true);
            }

            // Modifica solo la FECHA DE NACIMIENTO
            Console.WriteLine($"Fecha de Nacimiento: {FechaDeNacimiento} - S para modificar / Cualquier tecla para seguir");
            tecla = Console.ReadKey(intercept: true);
            if (tecla.Key == ConsoleKey.S)
            {
                FechaDeNacimiento = IngresarFecha("Ingrese la nueva fecha de nacimiento");
            }

            // Una vez modificado, se va a grabar en el archivo .txt
            Agenda.Grabar();
        }

        public void Mostrar()
        {
            // Se va a mostrar el DNI, APELLIDO, NOMBRE, DIRECCION, TELEFONO, FECHA DE NACIMIENTO(formato: dd/mm/yyyy)
            Console.WriteLine();
            Console.WriteLine($"DNi {DNI}");
            Console.WriteLine($"{Apellido}, {Nombre}");
            Console.WriteLine(Direccion);
            Console.WriteLine(Telefono);
            Console.WriteLine($"Nacido el {FechaDeNacimiento:dd/mm/yyyy}");
            Console.WriteLine();
        }

        public static Persona CrearModeloBusqueda()
        {
            // Se crea un modelo de PERSONA para hacer la busqueda
            var modelo = new Persona();
            // El modelo tiene todas las propiedades de PERSONA y se va a validar el ingreso de cada una de ellas
            modelo.DNI = IngresarDNI(obligatorio: false);
            modelo.Apellido = Ingreso("Ingrese el apellido", obligatorio: false);
            modelo.Nombre = Ingreso("Ingrese el nombre", obligatorio: false);
            modelo.Direccion = Ingreso("Ingrese la dirección", permiteNumeros: true, obligatorio: false);
            modelo.Telefono = Ingreso("Ingrese el teléfono", permiteNumeros: true, obligatorio: false);
            modelo.FechaDeNacimiento = IngresarFecha("Ingrese la fecha de nacimiento", obligatorio: false);
            return modelo;
        }

        // VALIDACION INGRESO DE DNI
        private static int IngresarDNI(bool obligatorio = true)
        {
            // Se pide ingresar un DNI
            var titulo = "Ingrese el DNI (Entero de 8 cifras)";
            if (!obligatorio)
            {
                titulo += " o presione [Enter] para continuar";
            }
            
            do
            {
                Console.WriteLine(titulo);
                var ingreso = Console.ReadLine();
                // si no pide ingresar dni y es un string vacio o nulo, devuelve 0
                if (!obligatorio && string.IsNullOrWhiteSpace(ingreso))
                {
                    return 0;
                }
                // si no ingresa un dni válido
                if (!int.TryParse(ingreso, out var dni))
                {
                    Console.WriteLine("No ha ingresado un DNI válido");
                    continue;
                }
                // no es válido si ingresa un dni menos de 10000000 o mas de 99999999
                if (dni < 10_000_000 || dni > 99_999_999)
                {
                    Console.WriteLine("Debe ser un número de 8 cifras");
                    continue;
                }
                // si en la agenda ya existe el dni
                if (Agenda.Existe(dni))
                {
                    Console.WriteLine("El DNI indicado ya existe en agenda");
                    continue;
                }
                return dni;
            } while (true);
        }

        public bool CoincideCon(Persona modelo)
        {
            // IF SON TODOS LOS CASOS QUE NO COINCIDEN EL ORIGINAL CON EL MODELO
            // si el dni el modelo de Persona  es diferente a 0 y es diferente al dni persona(original)
            if (modelo.DNI != 0 && modelo.DNI != DNI)
            {
                return false;
            }
            if (!string.IsNullOrWhiteSpace(modelo.Apellido) && !Apellido.Equals(modelo.Apellido, StringComparison.CurrentCultureIgnoreCase))
            {
                return false;
            }
            if (!string.IsNullOrWhiteSpace(modelo.Nombre) && !Nombre.Equals(modelo.Nombre, StringComparison.CurrentCultureIgnoreCase))
            {
                return false;
            }
            if (!string.IsNullOrWhiteSpace(modelo.Direccion) && !Direccion.Equals(modelo.Direccion, StringComparison.CurrentCultureIgnoreCase))
            {
                return false;
            }
            if (!string.IsNullOrWhiteSpace(modelo.Telefono) && !Telefono.Equals(modelo.Telefono, StringComparison.CurrentCultureIgnoreCase))
            {
                return false;
            }
            if (modelo.FechaDeNacimiento != DateTime.MinValue && FechaDeNacimiento != modelo.FechaDeNacimiento)
            {
                return false;
            }
            return true;
        }

        // VALIDACION FECHA
        private static DateTime IngresarFecha(string titulo, bool obligatorio = true)
        {
            do
            {
                if (!obligatorio)
                {
                    titulo += " o presione [Enter para continuar]";
                }

                Console.WriteLine(titulo);

                var ingreso = Console.ReadLine();

                if (!obligatorio && string.IsNullOrWhiteSpace(ingreso))
                {
                    return DateTime.MinValue;
                }

                if (!DateTime.TryParse(ingreso, out DateTime fechaNacimiento))
                {
                    Console.WriteLine("No es una fecha válida");
                    continue;
                }
                if (fechaNacimiento > DateTime.Now)
                {
                    Console.WriteLine("La fecha debe ser menor a la actual");
                    continue;
                }
                return fechaNacimiento;

            } while (true);
        }
        
        // VALIDACION DE TIPO STRING(APELLIDO, NOMBRE, DIRECCION, TELEFONO)
        private static string Ingreso(string titulo, bool permiteNumeros = false, bool obligatorio = true)
        {
            string ingreso;
            do
            {
                if (!obligatorio)
                {
                    titulo += " o presione [Enter] para continuar";
                }
                Console.WriteLine(titulo);
                ingreso = Console.ReadLine();     
                if (!obligatorio && string.IsNullOrWhiteSpace(ingreso))
                {
                    return null; 
                }

                if (obligatorio && string.IsNullOrWhiteSpace(ingreso))
                {
                    Console.WriteLine("Debe ingresar un valor");
                    continue;
                }

                if (!permiteNumeros && ingreso.Any(c => Char.IsDigit(c)))
                {
                    Console.WriteLine("El valor ingresado no debe contener números");
                    continue;
                }
                break;
            } while(true);
            return ingreso;
        }
        
    }
}
