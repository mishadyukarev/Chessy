using Photon.Realtime;

namespace Chessy.Model.Model.System
{
    public sealed partial class SystemsModel
    {
        internal void TryExecuteReadyForOnlineM(in Player sender)
        {
            var playerSend = sender.GetPlayer();

            _e.PlayerInfoE(playerSend).IsReadyForStartOnlineGame = !_e.PlayerInfoE(playerSend).IsReadyForStartOnlineGame;

            if (_e.PlayerInfoE(PlayerTypes.First).IsReadyForStartOnlineGame
                && _e.PlayerInfoE(PlayerTypes.Second).IsReadyForStartOnlineGame)
            {
                _e.IsStartedGame = true;
            }

            else
            {
                _e.IsStartedGame = false;
            }
        }
    }
}