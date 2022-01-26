using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WinKeg.Hardware
{
    public static class HardwareFactory
    {
        public static object GetHardwareClass(string className, string initializationData)
        {
            Type t = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.Name == className)
                .First();

            return Activator.CreateInstance(t, initializationData);
        }
    }
}