using Chessy.Game.Model.Entity;

namespace Chessy.Game.Model.System
{
    public sealed class GetHeroClickCenterS : SystemModel
    {
        internal GetHeroClickCenterS(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG)
        {
        }

        public void Get(in UnitTypes unitT)
        {
            if (unitT == UnitTypes.Elfemale && eMG.LessonTC.HaveLesson) return;

            if (eMG.CurPlayerIT == eMG.WhoseMovePlayerT)
            {
                eMG.Common.SoundActionC(Common.Enum.ClipCommonTypes.Click).Invoke();

                eMG.RpcPoolEs.GetHeroToMaster(unitT);
            }
            else eMG.SoundAction(ClipTypes.Mistake).Invoke();

            eMG.NeedUpdateView = true;
        }
    }
}