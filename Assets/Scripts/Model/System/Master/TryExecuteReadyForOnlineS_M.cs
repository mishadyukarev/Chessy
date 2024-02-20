using Photon.Realtime;

namespace Chessy.Model.System
{
    public partial class SystemsModel
    {
        internal void TryExecuteReadyForOnlineM(in Player sender)
        {
            var playerTSender_byte = (byte)sender.GetPlayer();

            playerInfoCs[playerTSender_byte].IsReadyForStartOnlineGame = !playerInfoCs[playerTSender_byte].IsReadyForStartOnlineGame;

            if (playerInfoCs[(byte)PlayerTypes.First].IsReadyForStartOnlineGame
                && playerInfoCs[(byte)PlayerTypes.Second].IsReadyForStartOnlineGame)
            {
                aboutGameC.IsStartedGame = true;
            }

            else
            {
                aboutGameC.IsStartedGame = false;
            }
        }
    }
}