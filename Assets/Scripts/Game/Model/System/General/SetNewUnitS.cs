using Chessy.Game.Entity.Model;
using Chessy.Game.Entity.Model.Cell.Unit;
using Chessy.Game.Values.Cell.Unit.Stats;

namespace Chessy.Game.System.Model
{
    public static class SetNewUnitS
    {
        public static void SetNewUnitHere(this ref UnitEs units, in UnitTypes unitT, in PlayerTypes playerT, in PlayerInfoEs playerInfo, in Chessy.Game.Entity.Model.EntitiesModelGame e)
        {
            units.MainE.UnitTC.Unit = unitT;
            units.MainE.PlayerTC.Player = playerT;
            units.MainE.LevelTC.Level = LevelTypes.First;
            units.MainE.ConditionTC.Condition = ConditionUnitTypes.None;
            units.MainE.IsRightArcherC.IsRight = false;

            units.StatsE.HealthC.Health = HpValues.MAX;
            units.StatsE.StepC.Steps = StepValues.MAX;
            units.StatsE.WaterC.Water = WaterValues.MAX;

            units.ExtraToolWeaponE.ToolWeaponTC.ToolWeapon = ToolWeaponTypes.None;
            units.ExtraToolWeaponE.LevelTC.Level = LevelTypes.None;
            units.ExtraToolWeaponE.ProtectionC.Protection = 0;

            units.EffectsE.StunC.Stun = 0;
            units.EffectsE.ShieldEffectC.Protection = 0;
            units.EffectsE.FrozenArrawC.Shoots = 0;

            playerInfo.LevelE(units.MainE.LevelTC.Level).Add(units.MainE.UnitTC.Unit, 1);


            if (unitT == UnitTypes.Pawn)
            {
                playerInfo.PeopleInCity--;

                units.MainToolWeaponE.ToolWeaponTC.ToolWeapon = ToolWeaponTypes.Axe;
                units.MainToolWeaponE.LevelTC.Level = LevelTypes.First;
            }

            else
            {
                if (units.MainE.UnitTC.Is(UnitTypes.Tree)) e.HaveTreeUnit = true;


                if (units.MainE.UnitTC.IsHero)
                {
                    playerInfo.HaveHeroInInventor = false;
                }
                else if (unitT == UnitTypes.King)
                {
                    playerInfo.HaveKingInInventor = false;
                }
                //else
                //{
                //    E.UnitInfoE(whoseMove, LevelTypes.First).HaveInInventor = false;
                //}


                units.MainToolWeaponE.ToolWeaponTC.ToolWeapon = ToolWeaponTypes.None;
                units.MainToolWeaponE.LevelTC.Level = LevelTypes.None;
            }

        }
    }
}