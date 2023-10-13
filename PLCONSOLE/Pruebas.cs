using ML;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    public class Pruebas

    {
        static void Main(string[] args)
        {
            Console.WriteLine("Bienvenido, que operacion deseas hacer? \n " +
                "1:Altas \n " +
                "2:Actualizaciones \n " +
                "3:Bajas \n " +
                "4:Visualizar todos los datos \n " +
                "5:Visualizar por Id \n " +
                "6:Salir");
            int x = int.Parse(Console.ReadLine());
            if (x > 0)
            {
                switch (x)
                {
                    //case 1:
                    //    PL.Pruebas.Add();
                    //    break;
                    //case 2:
                    //    PL.Usuario.Update();
                    //    break;
                    //case 3:
                    //    PL.Usuario.Delete();
                    //    break;
                    case 4:
                        PL.Pruebas.GetAll();
                        break;
                    case 5:
                        PL.Pruebas.GetById(1);
                        break;
                    case 6:
                        Environment.Exit(0);
                        break;

                }

            }
            else
            {
                Console.WriteLine("Escribe un numeroque sea mayor a 0");
            }


            //PL.Usuario.Add();
            // PL.Usuario.Update();
            //PL.Usuario.Delete();
            //PL.Usuario.GetAll();
            // PL.Usuario.GetById();
            Console.ReadKey();
        }

        public static void GetAll() //Obtener todos los registros
        {

            ML.Result result = new ML.Result();

            ML.Result result1 = BL.Empleado.GetAll();

            if (result.Correct)
            {
                foreach (ML.Empleado empleado in result.Objects)
                {
                    Console.WriteLine("El id del empleado es: " + empleado.Id);
                    Console.WriteLine("El numero de nomina de empleado es: " + empleado.NumeroNomina);
                    Console.WriteLine("El nombre del empleado es: " + empleado.Nombre);
                    Console.WriteLine("El Apellido Paterno del empleado es: " + empleado.ApellidoPaterno);
                    Console.WriteLine("El Apellido Materno del empleado es: " + empleado.ApellidoMaterno);
                    Console.WriteLine("El estado del empleado es: " + empleado.Estado.Nombre);
                    Console.WriteLine("El id estado del empleado es: " + empleado.Estado.IdEstado);
                    Console.WriteLine("*-*-*-**-");


                }
            }
            else
            {
                Console.WriteLine("Error " + result.ErrorMessage);
            }
        }
        //Obtener solo un registro por medio del Id
        public static void GetById(int Id)
        {
            ML.Empleado empleado = new ML.Empleado();

            Console.WriteLine("Ingresa el ID del usuario para buscarlo");
            empleado.Id = int.Parse(Console.ReadLine());


            ML.Result result = BL.Empleado.GetById(empleado);



            if (result.Correct)
            {

                Console.WriteLine("El id del empleado es: " + empleado.Id);
                Console.WriteLine("El numero de nomina de empleado es: " + empleado.NumeroNomina);
                Console.WriteLine("El nombre del empleado es: " + empleado.Nombre);
                Console.WriteLine("El Apellido Paterno del empleado es: " + empleado.ApellidoPaterno);
                Console.WriteLine("El Apellido Materno del empleado es: " + empleado.ApellidoMaterno);
                Console.WriteLine("El estado del empleado es: " + empleado.Estado.Nombre);
                Console.WriteLine("El id estado del empleado es: " + empleado.Estado.IdEstado);
                Console.WriteLine("*-*-*-**-");


            }
            else
            {
                Console.WriteLine("Error al mostrar" + result.ErrorMessage);
            }

        }


    }
}