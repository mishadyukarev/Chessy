using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Realtime;

namespace Chessy.Game.System.Model.Master
{
    public struct SetUnitS_M
    {
        public SetUnitS_M(in byte idx_0, in UnitTypes unitT, in Player sender, in EntitiesModel e)
        {
            var whoseMove = e.WhoseMove.Player;

            if (e.CellEs(idx_0).CellE.IsStartedCell(whoseMove) && !e.UnitTC(idx_0).HaveUnit)
            {
                e.UnitTC(idx_0).Unit = unitT;
                e.UnitPlayerTC(idx_0).Player = whoseMove;
                e.UnitLevelTC(idx_0).Level = LevelTypes.First;
                e.UnitConditionTC(idx_0).Condition = ConditionUnitTypes.None;
                e.UnitIsRightArcherC(idx_0).IsRight = false;
                e.UnitHpC(idx_0).Health = HpValues.MAX;
                e.UnitStepC(idx_0).Steps = StepValues.MAX;
                e.UnitWaterC(idx_0).Water = WaterValues.MAX;
                e.UnitExtraTWTC(idx_0).ToolWeapon = ToolWeaponTypes.None;
                e.UnitExtraLevelTC(idx_0).Level = LevelTypes.None;
                e.UnitExtraProtectionTC(idx_0).Protection = 0;
                e.UnitEffectStunC(idx_0).Stun = 0;
                e.UnitEffectShield(idx_0).Protection = 0;
                e.UnitEffectFrozenArrawC(idx_0).Shoots = 0;

                e.UnitInfo(e.UnitMainE(idx_0)).Add(e.UnitTC(idx_0).Unit, 1);


                if (unitT == UnitTypes.Pawn)
                {
                    e.PlayerInfoE(whoseMove).PeopleInCity--;

                    e.UnitMainTWTC(idx_0).ToolWeapon = ToolWeaponTypes.Axe;
                    e.UnitMainTWLevelTC(idx_0).Level = LevelTypes.First;
                }

                else
                {
                    if (e.IsHero(unitT))
                    {
                        e.PlayerInfoE(whoseMove).HaveHeroInInventor = false;
                    }
                    else if (unitT == UnitTypes.King)
                    {
                        e.PlayerInfoE(whoseMove).HaveKingInInventor = false;
                    }
                    //else
                    //{
                    //    E.UnitInfoE(whoseMove, LevelTypes.First).HaveInInventor = false;
                    //}


                    e.UnitMainTWTC(idx_0).ToolWeapon = ToolWeaponTypes.None;
                    e.UnitMainTWLevelTC(idx_0).Level = LevelTypes.None;
                }

                e.RpcPoolEs.SoundToGeneral(sender, ClipTypes.ClickToTable);


                new GetDataUnitOnCellS(idx_0, e);
            }
        }
    }
}