using Chessy.Model;
using UnityEngine;

namespace Chessy.Model
{
    sealed class ProtectUIS : SystemUIAbstract
    {
        readonly RightProtectUIE _protectUIE;

        internal ProtectUIS(in RightProtectUIE protectUIE, in EntitiesModel eMG) : base(eMG)
        {
            _protectUIE = protectUIE;
        }

        internal override void Sync()
        {
            var isEnableButt = false;

            if (!_e.LessonT.HaveLesson() || _e.LessonT >= Enum.LessonTypes.ClickDefend)
            {
                if (_e.UnitT(_e.SelectedCellIdx).HaveUnit())
                {
                    if (_e.UnitPlayerT(_e.SelectedCellIdx).Is(_e.CurPlayerIT))
                    {
                        isEnableButt = true;

                        _protectUIE.Button(UnitTypes.King).SetActive(false);
                        _protectUIE.Button(UnitTypes.Pawn).SetActive(false);
                        _protectUIE.Button(UnitTypes.Elfemale).SetActive(false);

                        _protectUIE.Button(_e.UnitT(_e.SelectedCellIdx)).SetActive(true);

                        if (_e.UnitConditionT(_e.SelectedCellIdx).Is(ConditionUnitTypes.Protected))
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
