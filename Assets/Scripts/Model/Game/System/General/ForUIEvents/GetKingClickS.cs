using Chessy.Common.Enum;

namespace Chessy.Game.Model.System
{
    public sealed partial class SystemsModelGameForUI
    {
        public void GetClickEffect()
        {
            _eMG.CellsC.Selected = 0;

            if (_eMG.CurPlayerITC.Is(_eMG.WhoseMovePlayerTC.PlayerT))
            {
                _eMG.Common.SoundActionC(ClipCommonTypes.Click).Invoke();

                if (_eMG.PlayerInfoE(_eMG.CurPlayerITC.PlayerT).KingInfoE.HaveInInventor)
                {
                    _eMG.SelectedUnitE.UnitTC.UnitT = UnitTypes.King;
                    _eMG.SelectedUnitE.LevelTC.LevelT = LevelTypes.First;

                    _eMG.CellClickTC.CellClickT = CellClickTypes.SetUnit;
                }
            }
            else _sMG.MistakeSs.MistakeS.Mistake(MistakeTypes.NeedWaitQueue);

            _eMG.NeedUpdateView = true;
        }
    }
}