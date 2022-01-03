using Prevoid.Model;
using Prevoid.Model.Commands;
using Prevoid.Model.EventArgs;
using Prevoid.Network.Connections;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prevoid.Network
{
    public class NetworkManager
    {
        public readonly Player LocalPlayer;
        private readonly Connection _Connection;
        private readonly Task _RecieveCommandsTask;

        public NetworkManager(Player localPlayer, string remoteIP = null)
        {
            LocalPlayer = localPlayer;
            _Connection = string.IsNullOrEmpty(remoteIP) ? new ServerConnection() : new Connection(remoteIP);
            GM.TurnEnded += SendCommandsIfNeeded;

            _RecieveCommandsTask = Task.Run(() => RecieveCommands());
        }

        public static void StartOnline()
        {
            Console.WriteLine("Your IP is listed below; Press Enter\nto create a game as a host,\nor enter host's IP to join.");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(Connection.GetLocalIP());
            string input = Console.ReadLine();

            var localPlayer = string.IsNullOrEmpty(input) ? GM.Player1 : GM.Player2;
            GM.GameMode = GameMode.PvPOnline;
            GM.LocalPlayer = localPlayer;
            GM.SetCurrentPlayer(localPlayer);

            _ = new NetworkManager(localPlayer, input);

            if (localPlayer == GM.Player1) // server
            {
                GM.CommandHandler.HandleCommand(new GenMapCommand(GM.Random.Next()));
            }
        }

        private async void SendCommandsIfNeeded(TurnEndedEventArgs e)
        {
            if (_RecieveCommandsTask.Exception != null) throw _RecieveCommandsTask.Exception;

            if (GM.CurrentPlayer != LocalPlayer && e.SendToOtherOnlinePlayer)
            {
                await SendCommands(e.Commands);
            }
        }

        private async Task SendCommands(IEnumerable<Command> commands)
        {
            await _Connection.SendText(CommandConverter.Serialize(commands));
        }

        private void RecieveCommands()
        {
            while (true)
            {
                HandleIncomingCommands(CommandConverter.Deserialize(_Connection.RecieveText()));
            }
        }

        private void HandleIncomingCommands(IEnumerable<Command> commands)
        {
            foreach (var command in commands)
            {
                GM.CommandHandler.HandleCommand(command);
            }

            GM.NextTurn(true);
        }
    }
}