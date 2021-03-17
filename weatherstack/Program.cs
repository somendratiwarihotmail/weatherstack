using System;

namespace weatherstack
{
    class Program
    {
        static TaskRunnerHelper taskRunnerHelper;
        static Program()
        {
            taskRunnerHelper = new TaskRunnerHelper();
        }
        
        static void Main()
        {
            taskRunnerHelper.RunAsync().GetAwaiter().GetResult();
            Console.ReadKey();
        }
    }
}
