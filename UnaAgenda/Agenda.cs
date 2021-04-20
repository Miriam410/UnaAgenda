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

            if (File.Exists(nombreArchivo))
            {
                using (var reader = new StreamReader(nombreArchivo))
                {
                    while (!reader.EndOfStream)
                    {
                        var linea = reader.ReadLine();
                        var persona = new Persona(linea);
                        entradas.Add(persona.DNI, persona);
                    }
                }

            }
        }
        public static void Agregar(Persona persona)
        {
            entradas.Add(persona.DNI, persona);
            Grabar();
        }

        public static void Baja(Persona persona)
        {
            entradas.Remove(persona.DNI);
            Grabar();
        }

        public static bool Existe(int dni)
        {
            return entradas.ContainsKey(dni);
        }

        public static Persona Seleccionar()
        {
            var modelo = Persona.CrearModeloBusqueda();
            foreach (var persona in entradas.Values)
            {
                if (persona.CoincideCon(modelo))
                {
                    return persona;
                }
            }

            Console.WriteLine("No se ha encontrado una persona que coincida");
            return null;
        }

        public static void Grabar()
        {
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
