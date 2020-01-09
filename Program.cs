using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CLox
{
    public class Program
    {
        static bool hadError = false;

        static async Task Main(string[] args)
        {
            if (args.Length > 1)
            {
                Console.WriteLine("Usage: clox [script]");
                Environment.Exit(64);
            }
            else if (args.Length == 1)
            {
                await runFiles(args[0]);
            }
            else
            {
                runPrompt();
            }
        }

        private static void runPrompt()
        {
            while (true)
            {
                Console.WriteLine("> ");
                run(Console.ReadLine());
                hadError = false;
            }
        }

        private static void run(string source)
        {
            Scanner scanner = new Scanner(source);
            List<Token> tokens = scanner.scanTokens();
            foreach (var token in tokens)
            {
                Console.WriteLine(token);
            }
        }

        public static void error(int line, string message)
        {
            report(line, "", message);
        }

        private static void report(int line, string where, string message)
        {
            Console.Error.WriteLine($"[line {line}] Error {where}: {message}");
            hadError = true;
        }

        private static async Task runFiles(string path)
        {
            if (File.Exists(path))
            {
                byte[] fileBytes = await File.ReadAllBytesAsync(path, new System.Threading.CancellationToken());
                run(Encoding.ASCII.GetString(fileBytes));
                if (hadError)
                {
                    Environment.Exit(65);
                }
            }
            else
            {
                Console.WriteLine($"{path} is not a valid file location");
            }
        }
    }
}
