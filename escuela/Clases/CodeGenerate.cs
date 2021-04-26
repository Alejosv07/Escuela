using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace escuela.Clases
{
    public class CodeGenerate
    {
        public string generarCode(int longitud) {
            string result = "";
            // Nuestro patrón de caracteres para formar el código
            string pattern = "+-_#!?0123456789abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ";
            // Creamos una instancia del generador de números aleatorios
            // cogemos como semilla los Ticks de reloj de esta forma nos 
            // aseguramos de no generar códigos con la misma semilla.
            Random myRndGenerator = new Random((int)DateTime.Now.Ticks);
            // Procedemos a conformar el código
            for (int i = 0; i < longitud; i++)
            {
                // Obtenemos un número aleatorio que se corresponde con una
                // posición dentro del pattern.
                int mIndex = myRndGenerator.Next(pattern.Length);
                // Vamos formando el código
                result += pattern[mIndex];
            }

            return result;
        }
    }
}