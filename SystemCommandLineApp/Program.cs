using System.CommandLine;
using System.CommandLine.Invocation;
using System.Linq;
using System.Threading.Tasks;
using CoreLib;

namespace SystemCommandLineApp
{
    public static class Program
    {
        public static Task Main(string[] args)
        {
            var rootCommand = new RootCommand
            {
                Name = Constants.Name, 
                Description = Constants.Description
            };
            rootCommand.AddCommand(BuildSayHelloCommand());
            rootCommand.AddCommand(BuildSayWelcomeCommand());
            rootCommand.AddCommand(BuildNumbersSumCommand());
            rootCommand.AddCommand(BuildStringLengthCommand());
            rootCommand.AddCommand(BuildStringReverseCommand());
            return rootCommand.InvokeAsync(args);
        }

        private static Command BuildSayHelloCommand()
        {
            var cmd = new Command("hello", Constants.HelloCommandDescription)
            {
                Handler = CommandHandler.Create(() =>
                {
                    Constants.Color.WriteLine("Hello World");
                })
            };
            return cmd;
        }

        private static Command BuildSayWelcomeCommand()
        {
            var cmd = new Command("welcome", Constants.WelcomeCommandDescription);
            cmd.AddOption(new Option(new[] {"--name", "-n"}, Constants.WelcomeOptionDescription)
            {
                Argument = new Argument<string>
                {
                    Arity = ArgumentArity.ExactlyOne
                }
            });
            cmd.Handler = CommandHandler.Create<string>(name =>
            {
                Constants.Color.WriteLine($"Welcome '{name}'");
            });
            return cmd;
        }

        private static Command BuildNumbersSumCommand()
        {
            var cmd = new Command("sum", Constants.SumCommandDescription);
            cmd.AddOption(new Option(new[] {"--numbers", "-n"}, Constants.SumOptionDescription)
            {
                Argument = new Argument<int[]>()
            });
            cmd.Handler = CommandHandler.Create<int[]>(numbers =>
            {
                Constants.Color.WriteLine($"Sum = '{numbers?.Sum() ?? 0}'");
            });
            return cmd;
        }

        private static Command BuildStringLengthCommand()
        {
            var cmd = new Command("length", Constants.LengthCommandDescription);
            cmd.AddOption(new Option(new[] {"--input", "-i"}, Constants.LengthOptionDescription)
            {
                Argument = new Argument<string>
                {
                    Arity = ArgumentArity.ExactlyOne
                }
            });
            cmd.Handler = CommandHandler.Create<string>(input =>
            {
                Constants.Color.WriteLine($"Length = '{input?.Length ?? 0}'");
            });
            return cmd;
        }

        private static Command BuildStringReverseCommand()
        {
            var cmd = new Command("reverse", Constants.ReverseCommandDescription);
            cmd.AddOption(new Option(new[] {"--input", "-i"}, Constants.ReverseOptionDescription)
            {
                Argument = new Argument<string>
                {
                    Arity = ArgumentArity.ExactlyOne
                }
            });
            cmd.Handler = CommandHandler.Create<string>(input =>
            {
                if (!string.IsNullOrWhiteSpace(input))
                {
                    Constants.Color.WriteLine($"Reverse = '{string.Concat(input.Reverse())}'");
                }
            });
            return cmd;
        }
    }
}
