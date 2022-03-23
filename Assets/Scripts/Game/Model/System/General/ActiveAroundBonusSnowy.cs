//using Chessy.Game.Values.Cell;
//using Chessy.Game.Values.Cell.Environment;
//using Chessy.Game.Values.Cell.Unit;
//using Chessy.Game.Values.Cell.Unit.Effect;
//using Chessy.Game.Values.Cell.Unit.Stats;
//using Photon.Realtime;

//namespace Chessy.Game.System.Model.Master.Methods
//{
//    public struct ActiveAroundBonusSnowy
//    {
//        public ActiveAroundBonusSnowy(in byte idx_0, in Player sender, in Chessy.Game.Entity.Model.EntitiesModel e)
//        {
//            var whoseMove = e.WhoseMove.Player;
//            var ability = AbilityTypes.ActiveAroundBonusSnowy;

//            if (e.UnitWaterC(idx_0).Water >= WaterValues.BONUS_AROUND_SNOWY || e.RiverEs(idx_0).RiverTC.HaveRiverNear)
//            {
//                if (e.UnitStepC(idx_0).Steps >= StepValues.BONUS_AROUND_SNOWY)
//                {
//                    if (!e.RiverEs(idx_0).RiverTC.HaveRiverNear) e.UnitWaterC(idx_0).Water -= WaterValues.BONUS_AROUND_SNOWY;
//                    e.UnitStepC(idx_0).Steps -= StepValues.BONUS_AROUND_SNOWY;
//                    e.UnitEs(idx_0).CoolDownC(ability).Cooldown = AbilityCooldownValues.NeedAfterAbility(ability);

//                    e.FertilizeC(idx_0).Resources = EnvironmentValues.MAX_RESOURCES;

//                    foreach (var idx_1 in e.CellEs(idx_0).IdxsAround)
//                    {
//                        e.FertilizeC(idx_1).Resources = EnvironmentValues.MAX_RESOURCES;

//                        if (e.UnitTC(idx_1).HaveUnit)
//                        {
//                            if (e.UnitPlayerTC(idx_1).Is(whoseMove))
//                            {
//                                if (e.UnitMainTWTC(idx_1).Is(ToolWeaponTypes.BowCrossbow))
//                                {

//                                }
//                                else
//                                {
//                                    e.UnitWaterC(idx_1).Water = WaterValues.MAX;
//                                }
//                            }
//                        }

//                        e.EffectEs(idx_1).HaveFire = false;
//                    }
//                }
//            }

//            else
//            {
//                e.RpcPoolEs.SoundToGeneral(sender, ClipTypes.Mistake);
//            }
//        }
//    }
//}