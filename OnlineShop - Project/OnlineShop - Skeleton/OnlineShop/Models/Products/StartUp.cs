namespace OnlineShop
{
using System.IO;

using OnlineShop.IO;

using OnlineShop.Core;
    public class StartUp
    {
        public static void Main()
        {
         
            string pathFile = Path.Combine("..", "..", "..", "output.txt");
            File.Create(pathFile).Close();

            IReader reader = new ConsoleReader();
            IWriter writer = new ConsoleWriter();
            ICommandInterpreter commandInterpreter = new CommandInterpreter();
            IController controller =new Controller();
            FileWriter fileWriter = new FileWriter(pathFile);

            IEngine engine = new Engine(reader, writer, commandInterpreter, controller, fileWriter);

            engine.Run();

        }
    }
}
