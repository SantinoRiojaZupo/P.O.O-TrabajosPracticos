using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

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
        int cantFilas = 20, cantColumnas = 10;
        int velCaida=500;/*Thread.Sleep(3000);*/

        public int CantFilas{
            get { return cantFilas; } set { cantFilas = value; }
        }
        public int CantColumnas
        {
            get { return cantColumnas; } set { cantColumnas = value; }
        }
        public int VelCaida
        {
            get { return velCaida; }
            set { velCaida = value; }
        }
   
    }
    class Copo
    {
        int posX, posY;
        public Copo(int x)
        {
            posX = x;
            posY = 0;
        }
        public void mover()
        {
            posY++;
        }
        public void mostrar()
        {
            
            Console.SetCursorPosition(posX,posY);
            Console.Write("*");
        }   
        public int PosY
        {
            get { return posY; }
            set { posY = value; }
        } public int PosX { 
            get { return posX; }
            set { posX = value; }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Configuracion config = new Configuracion();
            string[,] consola = new string[config.CantFilas, config.CantColumnas];
         
            List<Copo> copos = new List<Copo>();
               
            Random random = new Random();
            Console.CursorVisible = false;
            while (true) { 
           int posX = random.Next(0, config.CantColumnas); 

               copos.Add(new Copo(posX));

                foreach (Copo copo in copos)
                {
                    if (copo.PosY == config.CantFilas-1)
                    {

                    }
                       else if (consola[copo.PosY + 1,copo.PosX] == "*")
                        {

                        }
                        else
                        {
                    consola[copo.PosY, copo.PosX] = "";
                            copo.mover();
                        }
                    consola[copo.PosY, copo.PosX] = "*";
                 
                    copo.mostrar();
                }
                Thread.Sleep(config.VelCaida);

                //aca cuenta los copos de las filas y los borra
                //si queres que se borre la ultima de todas quita el primer for y cambia i por config.CantidadFilas-1
                int contador = 0;
                for(int i = 0; i < config.CantFilas; i++)
                {
                    contador = 0;
                    for (int j=0; j<config.CantColumnas;j++)
                    { 
                if (consola[i, j]=="*")
                    {
                        contador++;
                    }

                    }
                if(contador == config.CantColumnas)
                {
                            int filaBorrar = i;
                   for(int k=0;k<config.CantColumnas;k++)
                            {
                        consola[i, k] = "";

                            }
                for (int l = copos.Count - 1; l >= 0; l--)
                {
                    if (copos[l].PosY == filaBorrar)
                                    { 
                        copos.RemoveAt(l);
                                }
                              
                }
                            foreach (Copo copo in copos)
                            {
                                if (copo.PosY<filaBorrar)
                                { 
                                copo.PosY++;
                                }
                            }
                            for (int p=filaBorrar-1; p >= 0; p--)
                            {
                                for (int k =0;k< config.CantColumnas; k++)
                                {
                                    consola[p + 1, k] = consola[p, k];
                                }
                            }
                            for (int k = 0; k < config.CantColumnas; k++)
                            {
                                consola[0, k] = "";
                            }

                            contador = 0;
                            
                        }
                }
                Console.Clear();
               
            } 
        
            Console.ReadKey();
        }   
    }
}
