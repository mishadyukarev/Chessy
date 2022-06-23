using Photon.Pun;

namespace Chessy.Model.Model.System
{
    public sealed partial class SystemsModelGameForUI
    {
        public void GetHeroClickCenter(in UnitTypes unitT)
        {
            if (unitT == UnitTypes.Elfemale && _e.LessonT.HaveLesson()) return;

            if (_e.CurPlayerIT == _e.WhoseMovePlayerT)
            {
                _e.SoundAction(ClipTypes.Click).Invoke();

                _e.RpcC.Action0(_e.RpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(_s.GetHeroInCenterM), unitT });
            }
            else _e.SoundAction(ClipTypes.Mistake).Invoke();

            _e.NeedUpdateView = true;
        }
    }
}