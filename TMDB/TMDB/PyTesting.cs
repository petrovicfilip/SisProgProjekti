using Python.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMDB
{
    internal class PyTesting
    {
        public static void Test()
        {
            Runtime.PythonDLL = @"C:\Users\vukas\AppData\Local\Programs\Python\Python312\python312.dll";
            //Runtime.PythonDLL = @"C:\Users\BOBAN\AppData\Local\Programs\Python\Python311\python311.dll";

            PythonEngine.Initialize();
            using (Py.GIL())
            {
                dynamic sys = Py.Import("sys");
                sys.path.append("C:\\Users\\vukas\\source\\repos\\Sistemsko programiranje\\PROJEKATI\\TMDB\\SisProgProjekti\\TMDB\\TMDB");
                //sys.path.append("C:\\SistemskoProgramiranjeGitHub\\SisProgProjekti\\TMDB\\TMDB");
                var pythonScript = Py.Import("klijenti.py");
                pythonScript.Invoke();
            }
        }
    }
}
