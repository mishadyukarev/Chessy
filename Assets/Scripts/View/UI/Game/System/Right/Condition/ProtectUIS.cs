using Chessy.Game.Model.Entity;
using Chessy.Game.Model.Entity.Cell.Unit;
using UnityEngine;

namespace Chessy.Game
{
    sealed class ProtectUIS
    {
        readonly RightProtectUIE _protectUIE;
        readonly EntitiesModelGame _eMG;

        internal ProtectUIS(in RightProtectUIE protectUIE, in EntitiesModelGame eMG)
        {
            _protectUIE = protectUIE;
            _eMG = eMG;
        }

        public void Run()
        {
            var isEnableButt = false;

            if (_eMG.UnitTC(_eMG.SelectedCell).HaveUnit)
            {
                if (_eMG.UnitPlayerTC(_eMG.SelectedCell).Is(_eMG.CurPlayerIT))
                {
                    isEnableButt = true;

                    _protectUIE.Button(UnitTypes.King).SetActive(false);
                    _protectUIE.Button(UnitTypes.Pawn).SetActive(false);
                    _protectUIE.Button(UnitTypes.Elfemale).SetActive(false);

                    _protectUIE.Button(_eMG.UnitT(_eMG.SelectedCell)).SetActive(true);

                    if (_eMG.UnitConditionTC(_eMG.SelectedCell).Is(ConditionUnitTypes.Protected))
                    {
                        _protectUIE.ImageUIC.Image.color = Color.yellow;
                    }

                    else
                    {
                        _protectUIE.ImageUIC.Image.color = Color.white;
                    }
                }
            }

            _protectUIE.ImageUIC.SetActiveParent(isEnableButt);
        }
    }
}
