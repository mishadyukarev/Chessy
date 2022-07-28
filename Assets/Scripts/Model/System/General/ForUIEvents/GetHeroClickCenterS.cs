using Photon.Pun;
namespace Chessy.Model.System
{
    public sealed partial class ForButtonsSystemsModel
    {
        public void GetHeroClickCenter(in UnitTypes unitT)
        {
            if (unitT == UnitTypes.Elfemale && AboutGameC.LessonT.HaveLesson()) return;


            _dataFromViewC.SoundAction(ClipTypes.Click).Invoke();

            _rpcC.Action0(_rpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(_s.GetHeroInCenterM), unitT });

            _updateAllViewC.NeedUpdateView = true;
        }
    }
}