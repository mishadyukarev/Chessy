using Chessy.Model.Entity;
using Chessy.View.UI.Entity;
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

            if (!AboutGameC.LessonType.HaveLesson() || AboutGameC.LessonType >= Enum.LessonTypes.UniqueAttackInfo)
            {
                if (_unitCs[IndexesCellsC.Selected].HaveUnit)
                {
                    if (_unitCs[IndexesCellsC.Selected].PlayerType == AboutGameC.CurrentPlayerIType)
                    {
                        isEnableButt = true;

                        _protectUIE.Button(UnitTypes.King).TrySetActive(false);
                        _protectUIE.Button(UnitTypes.Pawn).TrySetActive(false);
                        _protectUIE.Button(UnitTypes.Elfemale).TrySetActive(false);

                        _protectUIE.Button(_unitCs[IndexesCellsC.Selected].UnitType).TrySetActive(true);

                        if (_unitCs[IndexesCellsC.Selected].ConditionType == ConditionUnitTypes.Protected)
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
