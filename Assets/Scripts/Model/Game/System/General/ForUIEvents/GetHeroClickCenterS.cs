namespace Chessy.Game.Model.System
{
    public sealed partial class SystemsModelGameForUI
    {
        public void GetHeroClickCenter(in UnitTypes unitT)
        {
            if (unitT == UnitTypes.Elfemale && _eMG.LessonTC.HaveLesson) return;

            if (_eMG.CurPlayerIT == _eMG.WhoseMovePlayerT)
            {
                _eMG.Common.SoundActionC(Common.Enum.ClipCommonTypes.Click).Invoke();

                _eMG.RpcPoolEs.GetHeroToMaster(unitT);
            }
            else _eMG.SoundAction(ClipTypes.Mistake).Invoke();

            _eMG.NeedUpdateView = true;
        }
    }
}