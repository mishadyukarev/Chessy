using Photon.Realtime;
namespace Chessy.Model.System
{
    public partial class SystemsModel
    {
        internal void TryExecuteReadyForOnlineM(in Player sender)
        {
            var playerSend = sender.GetPlayer();

            _e.PlayerInfoE(playerSend).PlayerInfoC.IsReadyForStartOnlineGame = !_e.PlayerInfoE(playerSend).PlayerInfoC.IsReadyForStartOnlineGame;

            if (_e.PlayerInfoE(PlayerTypes.First).PlayerInfoC.IsReadyForStartOnlineGame
                && _e.PlayerInfoE(PlayerTypes.Second).PlayerInfoC.IsReadyForStartOnlineGame)
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