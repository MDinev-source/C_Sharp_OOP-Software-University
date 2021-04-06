namespace RobotService.IO
{
    using System;
    using System.IO;
    using RobotService.IO.Contracts;

    public class Writer : IWriter
    {
        public void Write(string message)
        {
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"./test.txt", true))
            {
                file.WriteLine(message);
            }

            Console.Write(message);
        }

        public void WriteLine(string message)
        {
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"./test.txt", true))
            {
                file.WriteLine(message);
            }

            Console.WriteLine(message);
        }
    }
}
