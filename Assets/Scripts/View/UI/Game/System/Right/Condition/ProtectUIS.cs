using Chessy.Game.Model.Entity;
using UnityEngine;

namespace Chessy.Game
{
    sealed class ProtectUIS : SystemUIAbstract
    {
        readonly RightProtectUIE _protectUIE;

        internal ProtectUIS(in RightProtectUIE protectUIE, in EntitiesModelGame eMG) : base(eMG)
        {
            _protectUIE = protectUIE;
        }

        internal override void Sync()
        {
            var isEnableButt = false;

            if (!e.LessonTC.HaveLesson || e.LessonT >= Enum.LessonTypes.ClickDefend)
            {
                if (e.UnitTC(e.SelectedCell).HaveUnit)
                {
                    if (e.UnitPlayerTC(e.SelectedCell).Is(e.CurPlayerIT))
                    {
                        isEnableButt = true;

                        _protectUIE.Button(UnitTypes.King).SetActive(false);
                        _protectUIE.Button(UnitTypes.Pawn).SetActive(false);
                        _protectUIE.Button(UnitTypes.Elfemale).SetActive(false);

                        _protectUIE.Button(e.UnitT(e.SelectedCell)).SetActive(true);

                        if (e.UnitConditionTC(e.SelectedCell).Is(ConditionUnitTypes.Protected))
                        {
                            _protectUIE.ImageUIC.Image.color = Color.yellow;
                        }

                        else
                        {
                            _protectUIE.ImageUIC.Image.color = Color.white;
                        }
                    }
                }
            }

            _protectUIE.ImageUIC.SetActiveParent(isEnableButt);
        }
    }
}
