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

            if (!aboutGameC.LessonType.HaveLesson() || aboutGameC.LessonType >= Enum.LessonTypes.UniqueAttackInfo)
            {
                if (unitCs[indexesCellsC.Selected].HaveUnit)
                {
                    if (unitCs[indexesCellsC.Selected].PlayerType == aboutGameC.CurrentPlayerIType)
                    {
                        isEnableButt = true;

                        _protectUIE.Button(UnitTypes.King).TrySetActive(false);
                        _protectUIE.Button(UnitTypes.Pawn).TrySetActive(false);
                        _protectUIE.Button(UnitTypes.Elfemale).TrySetActive(false);

                        _protectUIE.Button(unitCs[indexesCellsC.Selected].UnitType).TrySetActive(true);

                        if (unitCs[indexesCellsC.Selected].ConditionType == ConditionUnitTypes.Protected)
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
