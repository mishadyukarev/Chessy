using Photon.Realtime;
namespace Chessy.Model.System
{
    public partial class SystemsModel
    {
        internal void TryExecuteReadyForOnlineM(in Player sender)
        {
            var playerSend = sender.GetPlayer();

            PlayerInfoE(playerSend).PlayerInfoC.IsReadyForStartOnlineGame = !PlayerInfoE(playerSend).PlayerInfoC.IsReadyForStartOnlineGame;

            if (PlayerInfoE(PlayerTypes.First).PlayerInfoC.IsReadyForStartOnlineGame
                && PlayerInfoE(PlayerTypes.Second).PlayerInfoC.IsReadyForStartOnlineGame)
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