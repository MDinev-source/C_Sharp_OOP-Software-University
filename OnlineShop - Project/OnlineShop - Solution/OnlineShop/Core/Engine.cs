namespace OnlineShop.Core
{
    using System;

    using OnlineShop.IO;
    public class Engine : IEngine
    {
        private const string Separator = " ";

        private readonly IReader reader;
        private readonly IWriter writer;
        private readonly ICommandInterpreter commandInterpreter;
        private readonly IController controller;
        private readonly FileWriter fileWriter;

        public Engine(IReader reader, IWriter writer, ICommandInterpreter commandInterpreter, IController controller,FileWriter fileWriter)
        {
            this.reader = reader;
            this.writer = writer;
            this.commandInterpreter = commandInterpreter;
            this.controller = controller;
            this.fileWriter = fileWriter;
        }

        public void Run()
        {
            while (true)
            {
                string[] data = this.reader.CustomReadLine().Split(Separator);
                string msg;

                try
                {
                    msg = this.commandInterpreter.ExecuteCommand(data, this.controller);
                }
                catch (ArgumentException e)
                {
                    msg = e.Message;
                }
                catch (InvalidOperationException e)
                {
                    msg = e.Message;
                }

                this.writer.CustomWriteLine(msg);

                this.fileWriter.CustomWriteLine(msg);
            }
        }
    }
}