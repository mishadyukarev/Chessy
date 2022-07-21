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

            if (!_aboutGameC.LessonType.HaveLesson() || _aboutGameC.LessonType >= Enum.LessonTypes.UniqueAttackInfo)
            {
                if (_e.UnitT(_cellsC.Selected).HaveUnit())
                {
                    if (_unitCs[_cellsC.Selected].PlayerType == _aboutGameC.CurrentPlayerIType)
                    {
                        isEnableButt = true;

                        _protectUIE.Button(UnitTypes.King).TrySetActive(false);
                        _protectUIE.Button(UnitTypes.Pawn).TrySetActive(false);
                        _protectUIE.Button(UnitTypes.Elfemale).TrySetActive(false);

                        _protectUIE.Button(_e.UnitT(_cellsC.Selected)).TrySetActive(true);

                        if (_unitCs[_cellsC.Selected].ConditionType == ConditionUnitTypes.Protected)
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
