using Chessy.Common;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Unit.Stats;
using Chessy.Game.View.UI.Entity.Right;

namespace Chessy.Game.View.UI.System
{
    sealed class UniqueButtonUIS : SystemAbstract, IEcsRunSystem
    {
        readonly ButtonTypes _buttonT;
        readonly UniqueButtonUIE _buttonE;
        readonly ResourcesE _resources;

        internal UniqueButtonUIS(in ButtonTypes buttonT, in UniqueButtonUIE uniqueButtonUIE, in ResourcesE res, in EntitiesModel ents) : base(ents)
        {
            _buttonT = buttonT;
            _buttonE = uniqueButtonUIE;
            _resources = res;
        }

        public void Run()
        {
            var ability_cur = E.UnitEs(E.CellsC.Selected).Ability(_buttonT).Ability;

            if (ability_cur == default)
            {
                _buttonE.ParenC.SetActive(false);
            }
            else
            {
                _buttonE.ParenC.SetActive(true);

                _buttonE.CooldonwTextC.SetActiveParent(E.UnitEs(E.CellsC.Selected).CoolDownC(ability_cur).HaveCooldown);
                _buttonE.CooldonwTextC.TextUI.text = E.UnitEs(E.CellsC.Selected).CoolDownC(ability_cur).Cooldown.ToString();

                

                _buttonE.AbilityImageC.Image.sprite = _resources.Sprite(ability_cur).Sprite;

                for (var abilityT = AbilityTypes.None + 1; abilityT < AbilityTypes.End; abilityT++)
                {
                    _buttonE.Zone(abilityT).SetActive(false);
                }
                _buttonE.Zone(ability_cur).SetActive(true);


                _buttonE.WaterTextC.ParentG.SetActive(false);
                switch (ability_cur)
                {
                    //case AbilityTypes.IceWall:
                    //    //_buttonE.WaterTextC.ParentG.SetActive(true);
                    //    //_buttonE.WaterTextC.TextUI.text = WaterValues..ToString();
                    //    break;

                    //case AbilityTypes.ActiveAroundBonusSnowy:
                    //    _buttonE.WaterTextC.ParentG.SetActive(true);
                    //    _buttonE.WaterTextC.TextUI.text = WaterValues.BONUS_AROUND_SNOWY.ToString();
                    //    break;

                    //case AbilityTypes.DirectWave:
                    //    _buttonE.WaterTextC.ParentG.SetActive(true);
                    //    _buttonE.WaterTextC.TextUI.text = WaterValues.DIRECT_WAVE.ToString();
                    //    break;

                    //case AbilityTypes.ChangeDirectionWind:
                    //    _buttonE.WaterTextC.ParentG.SetActive(true);
                    //    _buttonE.WaterTextC.TextUI.text = WaterValues.Need(ability_cur).ToString();
                    //    break;
                }

                _buttonE.WoodTextC.ParentG.SetActive(false);
                switch (ability_cur)
                {
                    case AbilityTypes.SetFarm:
                        _buttonE.WoodTextC.ParentG.SetActive(true);
                        _buttonE.WoodTextC.TextUI.text = EconomyValues.WOOD_FOR_BUILDING_FARM.ToString();
                        break;
                }

                if (ability_cur == AbilityTypes.KingPassiveNearBonus)
                {
                    _buttonE.StepsTextC.ParentG.SetActive(false);
                }
                else
                {
                    _buttonE.StepsTextC.ParentG.SetActive(true);
                }

                _buttonE.StepsTextC.TextUI.text = StepValues.Need(ability_cur).ToString();
            }
        }
    }
}