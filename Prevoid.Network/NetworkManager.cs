using Prevoid.Model;

namespace Prevoid.Network
{
    public class NetworkManager
    {
        public readonly Player CurrentMachinePlayer;
        private readonly Connection _Connection;

        public NetworkManager(Player currentMachinePlayer, string remoteIP = null)
        {
            CurrentMachinePlayer = currentMachinePlayer;
            _Connection = remoteIP is null ? new Connection() : new Connection(remoteIP);
            GM.TurnEnded += SendCommandsIfNeeded;

            Task.Run(() => RecieveCommands());
        }

        private async void SendCommandsIfNeeded(IEnumerable<Command> commands)
        {
            if (GM.CurrentPlayer != CurrentMachinePlayer)
            {
                await SendCommands(commands);
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
            throw new NotImplementedException();
        }
    }
}