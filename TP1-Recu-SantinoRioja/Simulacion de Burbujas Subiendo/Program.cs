using Microsoft.Win32;
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
    /*Realizar un programa que represente una simulación de burbujas ascendiendo en la consola,
     utilizando el símbolo "o" para cada burbuja.

El programa debe cumplir con las siguientes condiciones:

Definir una clase Configuracion que almacene los parámetros de la simulación,
    como la cantidad de filas, columnas, velocidad base del ascenso y cantidad máxima de burbujas permitidas.

Definir una clase Burbuja que modele el comportamiento de una burbuja.
    Cada burbuja debe tener una posición dentro de la consola,
    una velocidad propia y métodos para mostrarse, borrarse y desplazarse hacia arriba de manera irregular.

Usar una lista para administrar todas las burbujas activas durante la simulación.

Implementar una lógica que controle el ascenso de las burbujas,
    evitando que dos burbujas ocupen la misma posición tanto vertical como horizontalmente. 

Las burbujas deben mover­se de forma más natural:
 pueden ascender derecho, desviarse levemente hacia la izquierda o derecha, y deben hacerlo con velocidades diferentes entre sí.

Cuando una burbuja llegue a la fila superior, deberá eliminarse de la simulación para permitir la aparición de nuevas burbujas.
 Las burbujas deben aparecer de forma aleatoria, no constante, simulando un comportamiento más realista.

El programa debe ejecutarse en un ciclo continuo,
 generando una animación que simule burbujas ascendiendo dentro de un “acuario” en la consola.*/

    class Configuracion
    {
        int cantFilas = 20, cantColumnas = 10;
        int velCaida = 300;/*Thread.Sleep(3000);*/
        int intensidad = 5;

        public int CantFilas
        {
            get { return cantFilas; }
            set { cantFilas = value; }
        }
        public int CantColumnas
        {
            get { return cantColumnas; }
            set { cantColumnas = value; }
        }
        public int VelCaida
        {
            get { return velCaida; }
            set { velCaida = value; }
        }
        public int Intensidad
        {
            get { return intensidad; }
            set { intensidad = value; }
        }

    }
    class Copo
    {
        int posX, posY;
        public Copo(int x, int cantidadFilas)
        {
            posX = x;
            posY = cantidadFilas - 1;
        }
        public void mover()
        {
            posY--;
        }
        public void mostrar()
        {

            Console.SetCursorPosition(posX, posY);
            Console.Write("o");
        }
        public int PosY
        {
            get { return posY; }
            set { posY = value; }
        }
        public int PosX
        {
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
            Random viento = new Random();
            Console.CursorVisible = false;
            while (true)
            {
                int posX = random.Next(0, config.CantColumnas);
                int posicionviento = viento.Next(0, 2);
                int cantidadCopos = random.Next(1, config.Intensidad);

                for (int i = 0; i < cantidadCopos; i++)
                {
                    copos.Add(new Copo(posX, config.CantFilas));

                }

                foreach (Copo copo in copos)
                {
                    if (copo.PosY == 0)
                    {

                    }
                    else if ((copo.PosY > 0) && (consola[copo.PosY - 1, copo.PosX] == "o"))
                    {

                    }
                    else if (copo.PosY != config.CantFilas)
                    {
                        consola[copo.PosY, copo.PosX] = "";
                        copo.mover();
                    }
                    consola[copo.PosY, copo.PosX] = "o";

                    copo.mostrar();
                }
                Thread.Sleep(config.VelCaida);

                //aca cuenta los copos de las filas y los borra
                //si queres que se borre la ultima de todas quita el primer for y cambia i por config.CantidadFilas-1
                int contador = 0;
                for (int i = 0; i < config.CantFilas; i++)
                {
                    contador = 0;
                    for (int j = 0; j < config.CantColumnas; j++)
                    {
                        if (consola[i, j] == "o")
                        {
                            contador++;
                        }

                    }
                    if (contador == config.CantColumnas)
                    {
                        int filaBorrar = i;
                        for (int k = 0; k < config.CantColumnas; k++)
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


                        for (int p = filaBorrar - 1; p >= 0; p--)
                        {
                            for (int k = 0; k < config.CantColumnas; k++)
                            {
                                consola[p + 1, k] = consola[p, k];
                                consola[p, k] = "";
                            }
                        }


                        foreach (Copo copo in copos)
                        {
                            if (copo.PosY > filaBorrar)
                            {

                                copo.PosY--;
                            }
                        }
                        contador = 0;

                    }
                }
                foreach (Copo copo in copos)
                {


                    if (posicionviento == 0)
                    {
                        if (copo.PosX != 0 && consola[copo.PosY, copo.PosX - 1] != "o")
                        {
                            copo.PosX--;
                        }
                    }
                    else
                    {
                        if (copo.PosX != config.CantColumnas - 1 && consola[copo.PosY, copo.PosX + 1] != "o")
                        {
                            copo.PosX++;
                        }
                    }


                }
                for (int l = copos.Count - 1; l >= 0; l--)
                {
                    if (copos[l].PosY == 0)
                    {
                        copos.RemoveAt(l);
                    }

                }

                //quitar burbujas que sobran
                for (int i = 0; i < config.CantFilas; i++)
                {
                    for (int j = 0; j < config.CantColumnas; j++)
                    {
                        consola[i, j] = "";
                    }
                }

                // Volver a burbujas los copos
                foreach (Copo copo in copos)
                {
                    consola[copo.PosY, copo.PosX] = "o";
                }
                Console.Clear();
                if (Console.KeyAvailable)
                {
                    break;
                }

            }

        }
    }
}
