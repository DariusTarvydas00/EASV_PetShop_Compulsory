using CrashCourseVideoMenu.Core.ApplicationService;
using CrashCourseVideoMenu.Core.ApplicationService.Services;
using CrashCourseVideoMenu.Core.DomainService;
using CrashCourseVideoMenu.Infrastructure.Static.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CrashCourseVideoMenu
{
    static class Program
    {
        private static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<ICustomerRepository, CustomerRepository>();
            serviceCollection.AddScoped<ICustomerService, CustomerService>();
            serviceCollection.AddScoped<IPrinter, Printer>();
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var printer = serviceProvider.GetRequiredService<IPrinter>();
            printer.StartUI();
        }
    }
}