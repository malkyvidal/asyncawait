using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using System.Net;
namespace AsyncAwait
{
    class Program
    {
        static void Main(string[] args)
        {
           
            Console.WriteLine("hilo principal {0}",Thread.CurrentThread.ManagedThreadId);
            
           
            //initialize mytask and assign
            //a unit of work in form of 'myMethod()'
            //Task myTask = MyTarea();
            //myTask.Start();// Start the execution of mytask
            //Console.WriteLine("tarea otro hilo");
            //myTask.Wait(); //Wait until mytask finish its job
            //It's the part of Main Method

            //MyAsync().Wait();
            //Console.WriteLine(MyNumeroAsync().Result);
            Console.WriteLine(MetodoEsperaHtml().Result);
        }


        public static async Task<int> SumTwoOperationsAsync()
        {
            Console.WriteLine("Tarea pri {0}", Thread.CurrentThread.ManagedThreadId);
            var firstTask = GetOperationOneAsync();
            var secondTask = GetOperationTwoAsync();
            return await firstTask + await secondTask;
        }


        private static async Task<int> GetOperationOneAsync()
        {
            await Task.Delay(500); // Just to simulate an operation taking time
            Console.WriteLine("Tarea uno {0}", Thread.CurrentThread.ManagedThreadId);
            return 10;
        }

        private  static async Task<int> GetOperationTwoAsync()
        {
            await Task.Delay(100); // Just to simulate an operation taking time
            Console.WriteLine("Tarea do {0}", Thread.CurrentThread.ManagedThreadId);
            return 5;
        }

       
        private static int GetSum()
        {
            var fx = (10369 * 10369) - 1;
            for (Decimal i = 0; i < 10000000000000000000; i++)
            {
                Decimal d = i % fx;
                if (d==0)
                {
                    break;
                }
            }
            return 500;
        }
        private static void myMethod()
        {
            Console.WriteLine("Hello From My Task");
            for (int i = 0; i < 10; i++)
            {
                Console.Write("{0} ", i);
            }
            
            
            Console.WriteLine("my method prin {0}", Thread.CurrentThread.ManagedThreadId);
            
            Console.WriteLine("Bye From My Task");
        }

        private static async Task MyAsync()
        {
            Task tw = new Task(myMethod);
            tw.Start();
            Console.WriteLine("myasync {0}", Thread.CurrentThread.ManagedThreadId);
            
            // sin await hariamos tw.wait();
            await tw;
        }

        public static int MyNumero()
        {
            int sum = 0;
            for (int i = 0; i < 100000; i++)
            {
                sum++;
            }
            Console.WriteLine("Thread de Task  {0}", Thread.CurrentThread.ManagedThreadId);
            return sum;
        }
        private static Task MyTarea()
        {
            return new Task(myMethod);
        }

        private static async Task<int> MyNumeroAsync()
        {
            
            Task<int> tarea = new Task<int>(MyNumero);
            tarea.Start();// las tareas tiene que estar iniciada 

            Task<int> t2 = new Task<int>(MyNumero);
            t2.Start();

            // sin await hariamos esto:
            // int res = tarea.result;
            //int res2 = t2.result
            int res = await tarea;
            int res2 = await t2;
            return res + res2; 

        }
        private static Task<int> MetodoDevuelveTarea()
        {
            Task<int> tarea = new Task<int>(MyNumero);
            tarea.Start();//la tarea tiene que estar iniciada.
            return tarea;
        }

        private static async Task<int> MetodoEsperaTarea()
        {
            //int res = await MetodoDevuelveTarea(); // aca directamente espera sin que el hilo principal pueda hacer nada
            var de =  MetodoDevuelveTarea();
            Console.WriteLine("haciendo otra cosa mientras el otro hilo se ocupa de la tarea");
            int sw = await de; //en este punto si o si me paro por que el principal no
                              // tiene mas para hacer o porque requiere el resultado del otro hilo
            return sw;
        }
        //
        private static string ObtenerHtml()
        {
            WebClient cliente = new WebClient();
            Console.WriteLine("Thread de tarea que trae html  {0}", Thread.CurrentThread.ManagedThreadId);
            string respuesta = cliente.DownloadString("https://www.google.com.ar/");
            return respuesta;

            
        }

        private static async Task<string> MetodoEsperaHtml()
        {
            var tarea = GetHtml();
            Console.WriteLine("aprovechando mientras llega html");
            string resultado = await tarea;
            return resultado.Substring(0,100);
        }
        private static Task<string> GetHtml()
        {
            Task<string> tarea = new Task<string>(ObtenerHtml);
            tarea.Start();
            return tarea;
        }

    }
}
