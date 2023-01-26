using ConsoleApp.Dialogs;
using ConsoleApp.Extensions;
using ConsoleApp.Infrastructure;
using DataAccess;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using Repository.Repos;
using Spectre.Console;
using System.Text.RegularExpressions;
using static System.Console;

namespace ConsoleApp
{

    internal class Program
    {
        static void Main(string[] args)
        {
            ApplicationMain();



            //string sentence = "29810229-1411";
            ////^((19|20)\d{2})((0[1-9])|10|11|12)([0-3][1-9])-(\d{4})$
            //Regex reg = new Regex(@"^(\d{4})(\d{2})(\d{2})-(\d{4})$");
            //var matched = reg.Match(sentence);
            //if (matched.Success)
            //{
            //    WriteLine($"it is a match - \"{matched.Value}\"");
            //    var g = matched.Groups;
            //    if (DateTime.TryParse($"{g[1].Value}-{g[2].Value}-{g[3].Value}", out DateTime date))
            //    {
            //        WriteLine(date.ToString());
            //        if (date > DateTime.Today)
            //            WriteLine("in the future");
            //        else if (date < DateTime.Today)
            //            WriteLine("in the past");
            //    }
            //    else
            //        WriteLine("Unparsable date");
            //}
            //else
            //    WriteLine("it is not a match!");
            //AnsiConsole.Write(new Markup("[invert]Hello, world![/]").Centered());
        }

        static void ApplicationMain()
        {

            IServiceCollection services = new ServiceCollection();

            services.AddDialogs();
            services.AddTransient<DialogRouter>();
            services.AddDbContext<ApplicationDbContext>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IPersonnelRepository, PersonnelRepository>();
            services.AddScoped<IOccupationRepository, OccupationRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IClassRepository, ClassRepository>();
            services.AddScoped<ISqlServerClient, SqlServerClient>();

            IServiceProvider provider = services.BuildServiceProvider();
            IServiceScope scope = provider.CreateScope();

            var router = scope.ServiceProvider.GetRequiredService<DialogRouter>();
            router.StartRouterAt<HomeDialog>(scope.ServiceProvider);

            scope.Dispose();
        }
    }
}