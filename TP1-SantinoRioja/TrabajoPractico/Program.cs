using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TrabajoPractico
{
    /*Realizar un programa que represente una simulación de copos de nieve cayendo en la consola,
     utilizando el símbolo "*" para cada copo.

El programa debe cumplir con las siguientes condiciones:
Definir una clase Configuracion que almacene parámetros de la simulación,
como la cantidad de filas, columnas y la velocidad de caída de los copos.

Definir una clase Copo que modele el comportamiento de un copo de nieve.
Cada copo debe tener una posición en la consola y un método para mostrarse y desplazarse hacia abajo.

Usar una lista para administrar todos los copos activos durante la simulación.

Implementar una lógica que controle la caída de los copos de nieve, evitando que se superpongan en la misma posición
    .
Al completarse una fila con copos en todas las columnas, esta debe eliminarse para permitir que continúe la simulación.

El programa debe ejecutarse en un ciclo continuo, simulando de manera animada la caída de los copos.*/

    class Configuracion
    {
        int cantFilas = 10, cantColumnas = 10;
        int velCaida=3000;/*Thread.Sleep(3000);*/

        public Configuracion()
        {
            string[,] consola = new string[cantFilas,cantColumnas];

        }
        public int CantFilas{
            get; set;
        }
        public int CantColumnas
        {
            get; set;
        }
        public int VelCaida
        {
            get { return velCaida; }
            set { velCaida = value; }
        }

        List<Copo> copos = new List<Copo>();    
    }
    class Copo : Configuracion
    {
        int posX, posY;
        public Copo()
        {
            Random random = new Random();
            while (true) { 
            this.posX = random.Next(0, 9); 

                mostrar(this.posX, this.posY);
            } 
        }
        public void mostrar(int posX, int posY)
        {
            Console.CursorVisible = false;
            this.posX = posX;
            this.posY = posY;
            Console.SetCursorPosition(this.posX, this.posY);
            Console.Write("*");
            Thread.Sleep(VelCaida);
        }   
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Copo copo1 = new Copo();
            Console.ReadKey();
        }
    }
}
