namespace PlayersAndMonsters.Core
{
    using System;
    using PlayersAndMonsters.IO;
    using PlayersAndMonsters.IO.Contracts;
    using PlayersAndMonsters.Core.Contracts;
    public class Engine : IEngine
    {
        private IReader reader;
        private IWriter writer;

        private IManagerController controller;

        public Engine()
        {
            this.reader = new Reader();
            this.writer = new Writer();

            this.controller = new ManagerController();
        }
        public void Run()
        {


            while (true)
            {

                try
                {
                    string[] data = this.reader.ReadLine().Split();

                    if (data[0] == "Exit")
                    {
                        Environment.Exit(0);
                    }

                    string command = data[0];

                    var result = string.Empty;
                    if (command == "AddPlayer")
                    {
                        string playerType = data[1];
                        string playerUsername = data[2];

                        result = controller.AddPlayer(playerType, playerUsername);

                    }
                    else if (command == "AddCard")
                    {
                        string cardType = data[1];
                        string cardName = data[2];

                        result = controller.AddCard(cardType, cardName);

                    }

                    else if (command == "AddPlayerCard")
                    {
                        string username = data[1];
                        string cardName = data[2];

                        result = controller.AddPlayerCard(username, cardName);

                    }
                    else if (command == "Fight")
                    {
                        string attackUser = data[1];
                        string enemyUser = data[2];

                        result = controller.Fight(attackUser, enemyUser);

                    }

                    else if (command == "Report")
                    {
                        result = controller.Report();
                    }

                    this.writer.WriteLine(result);
                }
                catch (Exception e)
                {
                    this.writer.WriteLine(e.Message);
                }
            }
        }
    }
}
