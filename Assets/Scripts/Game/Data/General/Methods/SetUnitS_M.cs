using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Realtime;

namespace Chessy.Game.System.Model.Master
{
    public struct SetUnitS_M
    {
        public SetUnitS_M(in byte idx_0, in UnitTypes unitT, in Player sender, in EntitiesModel E)
        {
            var whoseMove = E.WhoseMove.Player;

            if (E.PlayerInfoE(whoseMove).ForSetUnitsC.Contains(idx_0))
            {
                E.UnitTC(idx_0).Unit = unitT;
                E.UnitPlayerTC(idx_0).Player = whoseMove;
                E.UnitLevelTC(idx_0).Level = LevelTypes.First;
                E.UnitConditionTC(idx_0).Condition = ConditionUnitTypes.None;
                E.UnitIsRightArcherC(idx_0).IsRight = false;
                E.UnitHpC(idx_0).Health = HpValues.MAX;
                E.UnitStepC(idx_0).Steps = StepValues.MAX;
                E.UnitWaterC(idx_0).Water = WaterValues.MAX;
                E.UnitExtraTWTC(idx_0).ToolWeapon = ToolWeaponTypes.None;
                E.UnitExtraLevelTC(idx_0).Level = LevelTypes.None;
                E.UnitExtraProtectionTC(idx_0).Protection = 0;
                E.UnitEffectStunC(idx_0).Stun = 0;
                E.UnitEffectShield(idx_0).Protection = 0;
                E.UnitEffectFrozenArrawC(idx_0).Shoots = 0;

                E.UnitInfo(E.UnitMainE(idx_0)).Add(E.UnitTC(idx_0).Unit, 1);


                if (unitT == UnitTypes.Pawn)
                {
                    E.PlayerInfoE(whoseMove).PeopleInCity--;

                    E.UnitMainTWTC(idx_0).ToolWeapon = ToolWeaponTypes.Axe;
                    E.UnitMainTWLevelTC(idx_0).Level = LevelTypes.First;
                }

                else
                {
                    if (E.IsHero(unitT))
                    {
                        E.PlayerInfoE(whoseMove).HaveHeroInInventor = false;
                    }
                    else if (unitT == UnitTypes.King)
                    {
                        E.PlayerInfoE(whoseMove).HaveKingInInventor = false;
                    }
                    //else
                    //{
                    //    E.UnitInfoE(whoseMove, LevelTypes.First).HaveInInventor = false;
                    //}


                    E.UnitMainTWTC(idx_0).ToolWeapon = ToolWeaponTypes.None;
                    E.UnitMainTWLevelTC(idx_0).Level = LevelTypes.None;
                }

                E.RpcPoolEs.SoundToGeneral(sender, ClipTypes.ClickToTable);
            }
        }
    }
}