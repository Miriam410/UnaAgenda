using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnaAgenda
{
    static class Agenda
    {
        private static readonly Dictionary<int, Persona> entradas;
        const string nombreArchivo = "agenda.txt";

        static Agenda()
        {
            //1) Al inicializar (la primera vez que se utiliza la clase Agenda) cargar los datos a partir de un archivo
            //File => permite manejar archivos como un todo
            //StreamReader => leer archivos
            //Streamwriter => escribir archivos

            entradas = new Dictionary<int, Persona>();
            // se pregunta si existe el nombre de archivo que ingresó el usuario por consola
            if (File.Exists(nombreArchivo))
            {
                // si existe el archivo, va a leer el archivo
                using (var reader = new StreamReader(nombreArchivo))
                {
                    while (!reader.EndOfStream)
                    {
                        // se va a leer por linea
                        var linea = reader.ReadLine();
                        // se crea una persona con parámetros de la linea(son las propiedades de persona parseada)
                        var persona = new Persona(linea);
                        // se agrega la persona el dni y al objeto persona
                        entradas.Add(persona.DNI, persona);
                    }
                }

            }
        }
        
        // Agregar una PERSONA
        public static void Agregar(Persona persona)
        {
            entradas.Add(persona.DNI, persona);
            Grabar();
        }

        // Dar de baja una PERSONA
        public static void Baja(Persona persona)
        {
            entradas.Remove(persona.DNI);
            Grabar();
        }

        // Preguntar si existe el DNI
        public static bool Existe(int dni)
        {
            return entradas.ContainsKey(dni);
        }

        // Seleccionar la persona comparandolo con un modelo
        public static Persona Seleccionar()
        {
            // se crea un modelo de busqueda
            var modelo = Persona.CrearModeloBusqueda();
            foreach (var persona in entradas.Values)
            {
                // si la persona coincide con el modelo
                if (persona.CoincideCon(modelo))
                {
                    return persona;
                }
            }

            Console.WriteLine("No se ha encontrado una persona que coincida");
            return null;
        }

        // Se va a grabar en un archivo .txt
        public static void Grabar()
        {
            // se va a escribir el archivo, pide el nombre del archivo a escribir 
            using (var writer = new StreamWriter(nombreArchivo, append: false))
            {
                foreach (var persona in entradas.Values)
                {
                    var linea = persona.ObtenerLineaDatos();
                    writer.WriteLine(linea);
                }
            }
        }
    }
}
