using CosmosOperatingSystem.Graphics;
using System;
using System.Collections.Generic;
using Sys = Cosmos.System;

namespace CosmosOperatingSystem
{
    public class Kernel : Sys.Kernel
    {
        const string OS_NAME = "zak";
        // First argument is command, second is nickname
        List<string[]> nicknames = new();

        protected override void BeforeRun()
        {
            Console.WriteLine("=======   Welcome to " + OS_NAME + "   =======");

            DisplayHandler.init();
            MouseHandler.init();
        }

        protected override void Run()
        {
            DisplayHandler.Update();

            /*Console.Write("> ");

            var input = Console.ReadLine();
            HandleCommand(input);*/
        }

        void HandleCommand(string cmd)
        {
            if (cmd == null) return;

            string[] args = cmd.Split(" ");

            switch (args[0])
            {
                case "hi":
                    Console.WriteLine("Hello.");
                    break;

                case "help":
                    if(args.Length == 1)
                    {
                        Console.WriteLine("help [command] Lists all commands or help for a speific command");
                        Console.WriteLine("info View info about OS");
                        Console.WriteLine("version View version number");
                        Console.WriteLine("shutdown Turn off computer");
                        Console.WriteLine("reboot Turn computer off and back on");
                        Console.WriteLine("wait <ms> Pauses operating system in milliseconds");
                        Console.WriteLine("nickname <command> <name> Allows command to be run by typing userdefined string");
                        Console.WriteLine("start_interface Starts GUI");
                        Console.WriteLine("clear Clear console");
                    }
                    break;

                case "info":
                    Console.WriteLine("Created by Jahan Rashidi. Last update: 2023.01.07");
                    break;

                case "build":
                case "version":
                case "v":
                    Console.WriteLine(OS_NAME + " v0.0.0");
                    break;

                case "shutdown":
                    Sys.Power.Shutdown();
                    break;

                case "restart":
                case "reboot":
                    Sys.Power.Reboot();
                    break;

                case "wait":
                case "delay":
                case "sleep":
                    if (args.Length == 2)
                    {
                        Console.WriteLine("Waiting for " + args[1] + "ms");
                        System.Threading.Thread.Sleep(short.Parse(args[1]));
                        Console.WriteLine("Finished");

                        break;
                    }

                    //BadArgCount();
                    break;

                case "nickname":
                    if(args.Length == 3)
                    {
                        nicknames.Add(new string[2] { args[1], args[2] });

                        Console.WriteLine("Nicknamed " + args[1] + " " + args[2]);

                        break;
                    }

                    //BadArgCount();
                    break;

                case "clear":
                    Console.Clear();
                    break;

                case "bless_code":
                    Console.WriteLine("佛祖保佑         永无BUG");
                    break;

                case "start_gui":
                    break;

                default:
                    for(int k = 0; k < nicknames.Count; k++)
                    {
                        if (nicknames[k][1] == args[0])
                        {
                            HandleCommand(nicknames[k][0]);
                            break;
                        }
                    }

                    Console.WriteLine("What is \"" + cmd + "\"?");
                    break;
            }

            /*void BadArgCount()
            {
                Console.WriteLine("Incorrect number of arguments");
                Console.WriteLine("Use help \"" + args[0] + "\" to learn the correct usage of the command");
            }*/
        }
    }
}
