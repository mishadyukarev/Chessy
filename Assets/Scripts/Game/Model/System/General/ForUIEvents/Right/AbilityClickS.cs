using Chessy.Common;
using Chessy.Game.Entity.Model;
using System;

namespace Chessy.Game.System.Model
{
    public sealed class AbilityClickS : SystemModelGameAbs
    {
        public AbilityClickS(in EntitiesModelGame eMGame) : base(eMGame)
        {

        }

        public void Click(in ButtonTypes uniqueButton)
        {
            if (e.CurPlayerITC.Is(e.WhoseMove.Player))
            {
                var idx_sel = e.CellsC.Selected;

                var abil = e.UnitEs(idx_sel).Ability(uniqueButton);

                if (!e.UnitEs(idx_sel).CoolDownC(abil.Ability).HaveCooldown)
                {
                    switch (abil.Ability)
                    {
                        case AbilityTypes.FirePawn:
                            e.RpcPoolEs.FirePawnToMas(idx_sel);
                            break;

                        case AbilityTypes.PutOutFirePawn:
                            e.RpcPoolEs.PutOutFirePawnToMas(idx_sel);
                            break;

                        case AbilityTypes.Seed:
                            e.RpcPoolEs.SeedEnvToMaster(idx_sel, EnvironmentTypes.YoungForest);
                            break;

                        case AbilityTypes.FireArcher:
                            e.SelectedE.AbilityTC.Set(AbilityTypes.FireArcher);
                            e.CellClickTC.Click = CellClickTypes.UniqueAbility;
                            break;

                        case AbilityTypes.CircularAttack:
                            e.RpcPoolEs.CircularAttackKingToMaster(idx_sel);
                            break;

                        case AbilityTypes.StunElfemale:
                            {
                                e.SelectedE.AbilityTC.Ability = AbilityTypes.StunElfemale;
                                e.CellClickTC.Click = CellClickTypes.UniqueAbility;
                            }
                            break;

                        case AbilityTypes.KingPassiveNearBonus:
                            //E.RpcPoolEs.BonusNearUnits(idx_sel);
                            //TryOnHint(VideoClipTypes.BonusKing);
                            break;


                        //Snowy

                        case AbilityTypes.IncreaseWindSnowy:
                            e.RpcPoolEs.IncreaseWindSnowy_ToMaster(idx_sel);
                            break;

                        case AbilityTypes.DecreaseWindSnowy:
                            e.RpcPoolEs.DecreaseWindSnowy_ToMaster(idx_sel);
                            break;

                        case AbilityTypes.ChangeCornerArcher:
                            e.RpcPoolEs.ChangeCornerArchToMas(idx_sel);
                            break;

                        case AbilityTypes.GrowAdultForest:
                            e.RpcPoolEs.GrowAdultForest(idx_sel);
                            break;

                        case AbilityTypes.ChangeDirectionWind:
                            {
                                e.SelectedE.AbilityTC.Ability = AbilityTypes.ChangeDirectionWind;
                                e.CellClickTC.Click = CellClickTypes.UniqueAbility;
                            }
                            break;

                        case AbilityTypes.SetFarm:
                            {
                                e.RpcPoolEs.BuildFarmToMaster(idx_sel);
                            }
                            break;

                        case AbilityTypes.DestroyBuilding:
                            e.RpcPoolEs.DestroyBuildingToMaster(idx_sel);
                            break;


                        //case AbilityTypes.IceWall:
                        //    E.RpcPoolEs.IceWallToMaster(idx_sel);
                        //    break;

                        //case AbilityTypes.ActiveAroundBonusSnowy:
                        //    E.RpcPoolEs.ActiveSnowyAroundToMaster(idx_sel);
                        //    break;

                        //case AbilityTypes.DirectWave:
                        //    E.SelectedAbilityTC.Ability = AbilityTypes.DirectWave;
                        //    E.CellClickTC.Click = CellClickTypes.UniqueAbility;
                        //    break;


                        case AbilityTypes.Resurrect:
                            e.SelectedE.AbilityTC.Ability = AbilityTypes.Resurrect;
                            e.CellClickTC.Click = CellClickTypes.UniqueAbility;
                            break;

                        case AbilityTypes.SetTeleport:
                            e.RpcPoolEs.SetTeleportToMaster(idx_sel);
                            break;

                        case AbilityTypes.Teleport:
                            e.RpcPoolEs.TeleportToMaster(idx_sel);
                            break;

                        case AbilityTypes.InvokeSkeletons:
                            e.RpcPoolEs.InvokeSkeletonsToMaster(idx_sel);
                            break;

                        default: throw new Exception();
                    }
                }

                else e.Sound(ClipTypes.Mistake).Action.Invoke();
            }
            else e.Sound(ClipTypes.Mistake).Action.Invoke();


            e.NeedUpdateView = true;
        }
    }
}