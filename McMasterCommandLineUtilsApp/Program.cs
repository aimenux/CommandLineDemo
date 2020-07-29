using System.Linq;
using System.Threading.Tasks;
using CoreLib;
using McMaster.Extensions.CommandLineUtils;

namespace McMasterCommandLineUtilsApp
{
    public static class Program
    {
        public static Task Main(string[] args)
        {
            var app = new CommandLineApplication
            {
                Name = Constants.Name, 
                Description = Constants.Description
            };

            app.HelpOption();

            app.Command(Constants.HelloCommandName, (command) =>
            {
                command.Description = Constants.HelloCommandDescription;
                command.OnExecute(() =>
                {
                    Constants.Color.WriteLine("Hello World");
                    return 0;
                });
            });

            app.Command(Constants.WelcomeCommandName, (command) =>
            {
                command.Description = Constants.WelcomeCommandDescription;
                var nameOption = command.Option<string>("--name|-n", Constants.WelcomeOptionDescription, CommandOptionType.SingleValue);
                command.OnExecute(() =>
                {
                    if (nameOption.HasValue())
                    {
                        Constants.Color.WriteLine($"Welcome '{nameOption.ParsedValue}'");
                    }
                    return 0;
                });
            });

            app.Command(Constants.SumCommandName, (command) =>
            {
                command.Description = Constants.SumCommandDescription;
                var numbersOption = command.Option<int>("--numbers|-n", Constants.SumOptionDescription, CommandOptionType.MultipleValue);
                command.OnExecute(() =>
                {
                    if (numbersOption.HasValue())
                    {
                        Constants.Color.WriteLine($"Sum = '{numbersOption.ParsedValues.Sum()}'");
                    }
                    return 0;
                });
            });

            app.Command(Constants.LengthCommandName, (command) =>
            {
                command.Description = Constants.LengthCommandDescription;
                var inputOption = command.Option<string>("--input|-i", Constants.LengthOptionDescription, CommandOptionType.SingleValue);
                command.OnExecute(() =>
                {
                    if (inputOption.HasValue())
                    {
                        Constants.Color.WriteLine($"Length = '{inputOption.ParsedValue.Length}'");
                    }
                    return 0;
                });
            });

            app.Command(Constants.ReverseCommandName, (command) =>
            {
                command.Description = Constants.ReverseCommandDescription;
                var inputOption = command.Option<string>("--input|-i", Constants.ReverseOptionDescription, CommandOptionType.SingleValue);
                command.OnExecute(() =>
                {
                    if (inputOption.HasValue())
                    {
                        Constants.Color.WriteLine($"Reverse = '{string.Concat(inputOption.ParsedValue.Reverse())}'");
                    }
                    return 0;
                });
            });

            return app.ExecuteAsync(args);
        }
    }
}
