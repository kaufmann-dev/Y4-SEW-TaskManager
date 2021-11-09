using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace TaskManager
{
    public class TaskManager
    {
        public async Task SimulateExecution()
        {
            SerialExecution();
            ConcurrentTaskExecution();
            await TaskExecutionAsync();
        }

        private void SerialExecution()
        {
            var stopWatch = Stopwatch.StartNew();
            var mSeconds = 0;

            mSeconds += OperationA();
            mSeconds += OperationB();
            mSeconds += OperationC();
            
            stopWatch.Stop();
            var elapsedTime = stopWatch.ElapsedMilliseconds;
            Console.WriteLine($"SerialExecution: serialExecution: {elapsedTime} taskExecution: {mSeconds}");
        }

        private void ConcurrentTaskExecution()
        {
            var stopWatch = Stopwatch.StartNew();

            Task<int> t1 = Task.Run(() => OperationA());
            Task<int> t2 = Task.Run(OperationB);
            Task<int> t3 = Task.Run(OperationC);

            var mSeconds = 0;

            var guard = Task.WhenAll(t1, t2, t3);
            
            foreach (var i in guard.Result)
            {
                mSeconds += i;
            }

            stopWatch.Stop();
            var elapsedTime = stopWatch.ElapsedMilliseconds;
            Console.WriteLine($"ConcurrentTaskExecution: serialExecution: {elapsedTime} taskExecution: {mSeconds}");
        }
        
        private async Task TaskExecutionAsync()
        {
            var stopWatch = Stopwatch.StartNew();

            Task<int> t1 = Task.Run(() => OperationA());
            Task<int> t2 = Task.Run(OperationB);
            Task<int> t3 = Task.Run(OperationC);

            var mSeconds = 0;

            var guard = await Task.WhenAll(t1, t2, t3);
            
            foreach (var i in guard)
            {
                mSeconds += i;
            }

            stopWatch.Stop();
            var elapsedTime = stopWatch.ElapsedMilliseconds;
            Console.WriteLine($"TaskExecutionAsync: serialExecution: {elapsedTime} taskExecution: {mSeconds}");
        }
        
        private int OperationA()
        {
            Thread.Sleep(700);
            return 700;
        }
        
        public int OperationB()
        {
            Thread.Sleep(500);
            return 500;
        }
        
        public int OperationC()
        {
            Thread.Sleep(680);
            return 680;
        }
    }
}