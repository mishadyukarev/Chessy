using Chessy.Model.Entity;
using UnityEngine;
using Chessy.View.UI.Entity; namespace Chessy.Model
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

            if (!_e.LessonT.HaveLesson() || _e.LessonT >= Enum.LessonTypes.UniqueAttackInfo)
            {
                if (_e.UnitT(_e.SelectedCellIdx).HaveUnit())
                {
                    if (_e.UnitPlayerT(_e.SelectedCellIdx).Is(_aboutGameC.CurrentPlayerIType))
                    {
                        isEnableButt = true;

                        _protectUIE.Button(UnitTypes.King).TrySetActive(false);
                        _protectUIE.Button(UnitTypes.Pawn).TrySetActive(false);
                        _protectUIE.Button(UnitTypes.Elfemale).TrySetActive(false);

                        _protectUIE.Button(_e.UnitT(_e.SelectedCellIdx)).TrySetActive(true);

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
