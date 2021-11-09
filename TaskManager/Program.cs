using System;
using System.Threading.Tasks;

namespace TaskManager
{
    class Program
    {
        static async Task Main(string[] args)
        {
            TaskManager tm = new TaskManager();
            await tm.SimulateExecution();
        }
    }
}