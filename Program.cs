using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Programa_Proyecto
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double[,] MatrizFinal = null; // Esta variable almacena la última matriz calculada
            string OperacionFinal = "";   // Esta variable almacena la última operación realizada

            while (true) // Bucle principal del programa que continúa hasta que el usuario elija salir
            {
                // Muestra el menú principal en la consola
                Console.Clear(); // Limpia la consola para empezar de nuevo
                Console.WriteLine("Bienvenido a este programa que realiza Operaciones de Matrices\n\n"); // Mensaje de bienvenida
                Console.WriteLine("Elige una opcion:\n"); // Instrucción para que el usuario elija una opción
                Console.WriteLine("1. Suma de Matrices"); // Realiza la  Sumar matrices
                Console.WriteLine("2. Resta de Matrices"); // Realiza la Restar matrices
                Console.WriteLine("3. Multiplicacion de Matrices"); // Realiza la Multiplicar matrices
                Console.WriteLine("4. Ultima Operacion Realizada"); //  Mostrar la última operación realizada
                Console.WriteLine("5. Salir\n"); // Opción 5: Salir del programa
                Console.Write("Elige una opción del 1 al 5 : "); // Solicita la opción del menú
                string opcion = Console.ReadLine(); // Lee la opción ingresada por el usuario

                if (opcion == "5") break; // Si la opción es 5, se rompe el bucle y el programa termina

                // Si la opción es 1, 2 o 3, el programa ejecuta una operación de matriz
                if (opcion == "1" || opcion == "2" || opcion == "3")
                {
                    // Obtiene las dimensiones (filas y columnas) de las matrices
                    int filas, columnas;
                    (filas, columnas) = DimensionesAobtener(); // Llama a la función que obtiene las dimensiones

                    // Se obtienen los valores de las dos matrices que el usuario va a operar
                    double[,] matriz1 = ObtenerMatriz(filas, columnas, "A"); // Solicita los valores para la matriz A
                    double[,] matriz2 = ObtenerMatriz(filas, columnas, "B"); // Solicita los valores para la matriz B

                    // Dependiendo de la opción elegida, se realiza la operación correspondiente
                    if (opcion == "1")
                    {
                        MatrizFinal = SumarMatrices(matriz1, matriz2); // Realiza la suma de matrices
                        OperacionFinal = "Suma"; // Registra la operación realizada
                    }
                    else if (opcion == "2")
                    {
                        MatrizFinal = RestarMatrices(matriz1, matriz2); // Realiza la resta de matrices
                        OperacionFinal = "Resta"; // Registra la operación realizada
                    }
                    else if (opcion == "3")
                    {
                        MatrizFinal = MultiplicarMatrices(matriz1, matriz2); // Realiza la multiplicación de matrices
                        OperacionFinal = "Multiplicación"; // Registra la operación realizada
                    }

                    // Muestra el resultado en la consola y lo guarda en un archivo de texto
                    MostrarMatriz(MatrizFinal, "Resultado"); // Llama a la función que muestra el resultado
                    GuardarMatrizEnArchivoDeTexto(MatrizFinal, "ResultadoMatriz.txt"); // Guarda el resultado en un archivo
                    Console.WriteLine("Resultado guardado en 'ResultadoMatriz.txt'. Presiona cualquier tecla para continuar...");
                    Console.ReadKey(); // Espera a que el usuario presione una tecla antes de continuar
                }
                // Si el usuario elige la opción 4, se muestra la última operación realizada, si existe
                else if (opcion == "4" && MatrizFinal != null)
                {
                    Console.WriteLine($"Última operación realizada: {OperacionFinal}"); // Muestra la última operación
                    MostrarMatriz(MatrizFinal, "Última Matriz Guardada"); // Muestra la última matriz calculada
                    Console.WriteLine("Presiona cualquier tecla para continuar...");
                    Console.ReadKey(); // Espera a que el usuario presione una tecla antes de continuar
                }
                else if (opcion == "4") // Si no hay operaciones previas, muestra un mensaje
                {
                    Console.WriteLine("No hay operaciones guardadas."); // Informa al usuario que no hay operaciones guardadas
                    Console.WriteLine("Presiona cualquier tecla para reintentar");
                    Console.ReadKey(); // Espera a que el usuario presione una tecla antes de continuar
                }
            }
            Console.WriteLine("¡Gracias por hacer uso del programa! Adiós."); // Mensaje final al cerrar el programa
        }

        // Función para obtener las dimensiones de las matrices
        static (int, int) DimensionesAobtener()
        {
            int filas = 0, columnas = 0; // Inicializa las variables para filas y columnas
            while (true) // Bucle para asegurar que el usuario introduce valores válidos
            {
                try
                {
                    // Solicita el número de filas y columnas
                    Console.Write("Introduce el número de filas que desees: ");
                    filas = int.Parse(Console.ReadLine()); // Lee el número de filas
                    Console.Write("Introduce el número de columnas que desees: ");
                    columnas = int.Parse(Console.ReadLine()); // Lee el número de columnas

                    // Verifica que las dimensiones sean mayores que 0
                    if (filas > 0 && columnas > 0) break; // Sale del bucle si ambas dimensiones son válidas
                    else Console.WriteLine("No puedes introducir dimensiones menores a 0."); // Muestra un error si no lo son
                }
                catch
                {
                    Console.WriteLine("Por favor, introduce números enteros válidos."); // Muestra un mensaje de error si hay una excepción
                }
            }
            return (filas, columnas); // Devuelve las dimensiones de la matriz
        }

        // Función para obtener los valores de una matriz introducidos por el usuario
        static double[,] ObtenerMatriz(int filas, int columnas, string nombre)
        {
            double[,] matriz = new double[filas, columnas]; // Crea una matriz vacía con las dimensiones especificadas
            Console.WriteLine($"Introduce los elementos de la matriz {nombre}:"); // Indica que se va a llenar la matriz
            for (int i = 0; i < filas; i++) // Itera sobre las filas de la matriz
            {
                for (int k = 0; k < columnas; k++) // Itera sobre las columnas de la matriz
                {
                    while (true) // Bucle para validar la entrada del usuario
                    {
                        try
                        {
                            // Solicita y almacena el valor de cada elemento de la matriz
                            Console.Write($"Elemento ({i + 1},{k + 1}): ");
                            matriz[i, k] = double.Parse(Console.ReadLine()); // Lee el valor introducido por el usuario
                            break; // Sale del bucle si la entrada es válida
                        }
                        catch
                        {
                            Console.WriteLine("Introduce un número válido."); // Muestra un mensaje de error si la entrada no es válida
                        }
                    }
                }
            }
            return matriz; // Devuelve la matriz llena
        }

        // Función para sumar dos matrices
        static double[,] SumarMatrices(double[,] a, double[,] b)
        {
            int filas = a.GetLength(0); // Obtiene el número de filas de las matrices
            int columnas = a.GetLength(1); // Obtiene el número de columnas de las matrices
            double[,] resultado = new double[filas, columnas]; // Crea una nueva matriz para almacenar el resultado

            for (int i = 0; i < filas; i++) // Itera sobre las filas
            {
                for (int j = 0; j < columnas; j++) // Itera sobre las columnas
                {
                    resultado[i, j] = a[i, j] + b[i, j]; // Suma los elementos correspondientes de las matrices
                }
            }
            return resultado; // Devuelve la matriz resultante
        }

        // Función para restar dos matrices
        static double[,] RestarMatrices(double[,] a, double[,] b)
        {
            int filas = a.GetLength(0); // Obtiene el número de filas de las matrices
            int columnas = a.GetLength(1); // Obtiene el número de columnas de las matrices
            double[,] resultado = new double[filas, columnas]; // Crea una nueva matriz para almacenar el resultado

            for (int i = 0; i < filas; i++) // Itera sobre las filas
            {
                for (int k = 0; k < columnas; k++) // Itera sobre las columnas
                {
                    resultado[i, k] = a[i, k] - b[i, k]; // Resta los elementos correspondientes de las matrices
                }
            }
            return resultado; // Devuelve la matriz resultante
        }

        // Función para multiplicar dos matrices
        static double[,] MultiplicarMatrices(double[,] a, double[,] b)
        {
            int filas = a.GetLength(0); // Obtiene el número de filas de la primera matriz
            int columnas = b.GetLength(1); // Obtiene el número de columnas de la segunda matriz
            int n = a.GetLength(1); // Obtiene el número de columnas de la primera matriz
            double[,] resultado = new double[filas, columnas]; // Crea una nueva matriz para almacenar el resultado

            for (int i = 0; i < filas; i++) // Itera sobre las filas de la primera matriz
            {
                for (int j = 0; j < columnas; j++) // Itera sobre las columnas de la segunda matriz
                {
                    resultado[i, j] = 0; // Inicializa el valor del elemento
                    for (int k = 0; k < n; k++) // Itera para realizar la multiplicación de las matrices
                    {
                        resultado[i, j] += a[i, k] * b[k, j]; // Realiza la multiplicación y suma de los productos
                    }
                }
            }
            return resultado; // Devuelve la matriz resultante
        }

        // Función para mostrar una matriz en la consola
        static void MostrarMatriz(double[,] matriz, string titulo)
        {
            Console.WriteLine($"\n{titulo}:"); // Muestra el título antes de la matriz
            int filas = matriz.GetLength(0); // Obtiene el número de filas de la matriz
            int columnas = matriz.GetLength(1); // Obtiene el número de columnas de la matriz
            for (int i = 0; i < filas; i++) // Itera sobre las filas
            {
                for (int j = 0; j < columnas; j++) // Itera sobre las columnas
                {
                    Console.Write($"{matriz[i, j]} \t"); // Muestra cada elemento seguido de un tabulador
                }
                Console.WriteLine(); // Salto de línea al final de cada fila
            }
        }

        // Función para guardar una matriz en un archivo de texto
        static void GuardarMatrizEnArchivoDeTexto(double[,] matriz, string nombreArchivo)
        {
            using (StreamWriter archivo = new StreamWriter(nombreArchivo)) // Abre el archivo para escritura
            {
                int filas = matriz.GetLength(0); // Obtiene el número de filas de la matriz
                int columnas = matriz.GetLength(1); // Obtiene el número de columnas de la matriz
                for (int i = 0; i < filas; i++) // Itera sobre las filas
                {
                    for (int j = 0; j < columnas; j++) // Itera sobre las columnas
                    {
                        archivo.Write($"{matriz[i, j]}\t"); // Escribe cada elemento seguido de un tabulador
                    }
                    archivo.WriteLine(); // Salto de línea al final de cada fila
                }
            }
        }
    }
}
