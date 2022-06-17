using Chessy.Game.Model.Entity;
using Photon.Realtime;

namespace Chessy.Game.Model.System
{
    public sealed partial class SystemsModelGame
    {
        internal void TryExecuteReadyForOnlineM(in Player sender)
        {
            var playerSend = sender.GetPlayer();

            _eMG.PlayerInfoE(playerSend).IsReadyForStartOnlineGame = !_eMG.PlayerInfoE(playerSend).IsReadyForStartOnlineGame;

            if (_eMG.PlayerInfoE(PlayerTypes.First).IsReadyForStartOnlineGame
                && _eMG.PlayerInfoE(PlayerTypes.Second).IsReadyForStartOnlineGame)
            {
                _eMG.IsStartedGame = true;
            }

            else
            {
                _eMG.IsStartedGame = false;
            }
        }
    }
}