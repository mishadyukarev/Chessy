using Photon.Pun;
namespace Chessy.Model.System
{
    public sealed partial class ForButtonsSystemsModel
    {
        public void GetHeroClickCenter(in UnitTypes unitT)
        {
            if (unitT == UnitTypes.Elfemale && _e.LessonT.HaveLesson()) return;

            if (_e.CurrentPlayerIT == _e.WhoseMovePlayerT)
            {
                _e.SoundAction(ClipTypes.Click).Invoke();

                _e.RpcC.Action0(_e.RpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(_s.GetHeroInCenterM), unitT });
            }
            else _e.SoundAction(ClipTypes.Mistake).Invoke();

            _e.NeedUpdateView = true;
        }
    }
}