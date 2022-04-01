using Microsoft.Extensions.DependencyInjection;
using Ncfe.CodeTest;
using System;

namespace CodeTestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddSingleton<ILearnerService, LearnerService>()
                .AddSingleton<IFailoverLearnerDataAccess, FailoverLearnerDataAccess>()
                .AddSingleton<IArchivedDataService, ArchivedDataService>()
                .AddSingleton<IFailoverRepository, FailoverRepository>()
                .AddSingleton<ILearnerDataAccess, LearnerDataAccess>()
                .BuildServiceProvider();

            var learningService = serviceProvider.GetService<ILearnerService>();
            var data1= learningService.GetLearner(1, true);
            var data2 = learningService.GetLearner(1, false);
            Console.ReadKey();
           
        }
    }
}
