using Chessy.Model;
using Chessy.Model.Entity;
using Chessy.Model.Enum;
using Chessy.View.UI.Entity;

namespace Chessy.View.UI.System
{
    sealed class SyncFirstAbilityButtonVS : SystemUIAbstract
    {
        bool _needActive;
        bool _wasActivated;

        readonly UniqueButtonUIE _buttonE;

        internal SyncFirstAbilityButtonVS(in UniqueButtonUIE uniqueButtonUIE, in EntitiesModel eM) : base(eM)
        {
            _buttonE = uniqueButtonUIE;
        }

        internal override void Sync()
        {
            _needActive = false;


            if (indexesCellsC.IsSelectedCell)
            {
                var selectedIdxCell = indexesCellsC.Selected;

                var currentAbility = UnitButtonsC(selectedIdxCell).Ability(ButtonTypes.First);

                if (unitCs[selectedIdxCell].PlayerType == aboutGameC.CurrentPlayerIType && currentAbility != AbilityTypes.None)
                {
                    if (!aboutGameC.LessonType.HaveLesson() || aboutGameC.LessonType >= LessonTypes.SeedingPawn)
                    {
                        _needActive = true;
                    }
                }
            }

            _buttonE.ParenGOC.TrySetActive2(_needActive, ref _wasActivated);

            //if (_needActive)
            //{
            //    _buttonE.CooldonwTextC.SetActiveParent(_cooldownAbilityCs[_indexesCellsC.Selected].HaveCooldown(currentAbility));
            //    _buttonE.CooldonwTextC.TextUI.text = _cooldownAbilityCs[_indexesCellsC.Selected].Cooldown(currentAbility).ToString();

            //    _buttonE.AbilityImageC.Image.sprite = _fromResourcesC.Sprite(currentAbility);

            //    for (var abilityT = AbilityTypes.None + 1; abilityT < AbilityTypes.End; abilityT++)
            //    {
            //        _buttonE.Zone(abilityT).TrySetActive(false);
            //    }

            //    _buttonE.Zone(currentAbility).TrySetActive(true);


            //    _buttonE.WaterTextC.ParentG.SetActive(false);

            //    _buttonE.WoodTextC.ParentG.SetActive(false);
            //    switch (currentAbility)
            //    {
            //        case AbilityTypes.SetFarm:
            //            _buttonE.WoodTextC.ParentG.SetActive(true);
            //            _buttonE.WoodTextC.TextUI.text = ((int)(100 * EconomyValues.WOOD_FOR_BUILDING_FARM)).ToString();
            //            break;
            //    }
            //}
        }
    }
}