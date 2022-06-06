using Chessy.Common.Enum;
using Chessy.Common.Interface;
using Chessy.Game.Model.Entity;

namespace Chessy.Game.Model.System
{
    public sealed class GetKingClickS : SystemModel, IClickUI
    {
        internal GetKingClickS(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG) { }

        public void Click()
        {
            eMG.CellsC.Selected = 0;

            if (eMG.CurPlayerITC.Is(eMG.WhoseMovePlayerTC.PlayerT))
            {
                eMG.Common.SoundActionC(ClipCommonTypes.Click).Invoke();

                if (eMG.PlayerInfoE(eMG.CurPlayerITC.PlayerT).KingInfoE.HaveInInventor)
                {
                    eMG.SelectedUnitE.UnitTC.UnitT = UnitTypes.King;
                    eMG.SelectedUnitE.LevelTC.LevelT = LevelTypes.First;

                    eMG.CellClickTC.CellClickT = CellClickTypes.SetUnit;
                }
            }
            else sMG.MistakeSs.MistakeS.Mistake(MistakeTypes.NeedWaitQueue);

            eMG.NeedUpdateView = true;
        }
    }
}