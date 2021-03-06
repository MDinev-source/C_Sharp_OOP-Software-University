namespace OnlineShop.IO
{
    using System;
    public class ConsoleWriter : IWriter
    {
        public void CustomWriteLine(string text)
        {
            using (System.IO.StreamWriter file =
          new System.IO.StreamWriter(@"./test.txt", true))
            {
                file.WriteLine(text);
            }
            Console.WriteLine(text);
        }

        public void CustomWrite(string text)
        {
            using (System.IO.StreamWriter file =
          new System.IO.StreamWriter(@"./test.txt", true))
            {
                file.WriteLine(text);
            }
            Console.Write(text);
        }
    }
}