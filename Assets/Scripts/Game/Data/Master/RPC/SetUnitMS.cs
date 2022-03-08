using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Unit.Stats;

namespace Chessy.Game
{
    sealed class SetUnitMS : SystemAbstract, IEcsRunSystem
    {
        internal SetUnitMS(in EntitiesModel ents) : base(ents)
        {

        }

        public void Run()
        {
            var whoseMove = E.WhoseMove.Player;

            var idx_0 = E.RpcPoolEs.SetUnitME.IdxC.Idx;
            var unitT = E.RpcPoolEs.SetUnitME.UnitTC.Unit;
            var sender = E.RpcPoolEs.SenderC.Player;


            if (unitT != UnitTypes.None)
            {
                if (E.UnitEs(idx_0).ForPlayer(whoseMove).CanSetUnitHere)
                {
                    E.UnitTC(idx_0).Unit = unitT;
                    E.UnitPlayerTC(idx_0).Player = whoseMove;
                    E.UnitLevelTC(idx_0).Level = LevelTypes.First;
                    E.UnitConditionTC(idx_0).Condition = ConditionUnitTypes.None;
                    E.UnitIsRightArcherC(idx_0).IsRight = false;
                    E.UnitHpC(idx_0).Health = Hp_VALUES.HP;
                    E.UnitStepC(idx_0).Steps = 1/*E.UnitStatsE(idx_0).MaxStepsC.Steps*/;
                    E.UnitWaterC(idx_0).Water = E.UnitInfo(E.UnitMainE(idx_0)).WaterKingPawnMax;
                    E.UnitExtraTWTC(idx_0).ToolWeapon = ToolWeaponTypes.None;
                    E.UnitExtraLevelTC(idx_0).Level = LevelTypes.None;
                    E.UnitExtraProtectionTC(idx_0).Protection = 0;
                    E.UnitEffectStunC(idx_0).Stun = 0;
                    E.UnitEffectShield(idx_0).Protection = 0;
                    E.UnitEffectFrozenArrawC(idx_0).Shoots = 0;

                    E.UnitInfo(E.UnitMainE(idx_0)).Add(E.UnitTC(idx_0).Unit, 1);


                    if (unitT == UnitTypes.Pawn)
                    {
                        E.PlayerE(whoseMove).PeopleInCity--;

                        E.UnitMainTWTC(idx_0).ToolWeapon = ToolWeaponTypes.Axe;
                        E.UnitMainTWLevelTC(idx_0).Level = LevelTypes.First;
                    }

                    else
                    {
                        if (E.IsHero(unitT))
                        {
                            E.PlayerE(whoseMove).HaveHeroInInventor = false;
                        }
                        else if (unitT == UnitTypes.King)
                        {
                            E.PlayerE(whoseMove).HaveKingInInventor = false;
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


                E.RpcPoolEs.SetUnitME.UnitTC.Unit = UnitTypes.None;
            }
        }
    }
}