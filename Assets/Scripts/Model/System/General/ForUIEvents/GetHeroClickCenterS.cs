using Photon.Pun;
namespace Chessy.Model.System
{
    public sealed partial class ForButtonsSystemsModel
    {
        public void GetHeroClickCenter(in UnitTypes unitT)
        {
            if (unitT == UnitTypes.Elfemale && _aboutGameC.LessonT.HaveLesson()) return;


            _e.SoundAction(ClipTypes.Click).Invoke();

            _rpcC.Action0(_rpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(_s.GetHeroInCenterM), unitT });

            _updateAllViewC.NeedUpdateView = true;
        }
    }
}