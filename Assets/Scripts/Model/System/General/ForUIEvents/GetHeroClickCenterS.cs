using Photon.Pun;
namespace Chessy.Model.System
{
    public sealed partial class ForButtonsSystemsModel
    {
        public void GetHeroClickCenter(in UnitTypes unitT)
        {
            if (unitT == UnitTypes.Elfemale && aboutGameC.LessonT.HaveLesson()) return;


            dataFromViewC.SoundAction(ClipTypes.Click).Invoke();

            rpcC.Action0(rpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(s.GetHeroInCenterM), unitT });

            updateAllViewC.NeedUpdateView = true;
        }
    }
}