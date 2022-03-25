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
            if (eMGame.CurPlayerITC.Is(eMGame.WhoseMove.Player))
            {
                var idx_sel = eMGame.CellsC.Selected;

                var abil = eMGame.UnitEs(idx_sel).Ability(uniqueButton);

                if (!eMGame.UnitEs(idx_sel).CoolDownC(abil.Ability).HaveCooldown)
                {
                    switch (abil.Ability)
                    {
                        case AbilityTypes.FirePawn:
                            eMGame.RpcPoolEs.FirePawnToMas(idx_sel);
                            break;

                        case AbilityTypes.PutOutFirePawn:
                            eMGame.RpcPoolEs.PutOutFirePawnToMas(idx_sel);
                            break;

                        case AbilityTypes.Seed:
                            eMGame.RpcPoolEs.SeedEnvToMaster(idx_sel, EnvironmentTypes.YoungForest);
                            break;

                        case AbilityTypes.FireArcher:
                            eMGame.SelectedE.AbilityTC.Set(AbilityTypes.FireArcher);
                            eMGame.CellClickTC.Click = CellClickTypes.UniqueAbility;
                            break;

                        case AbilityTypes.CircularAttack:
                            eMGame.RpcPoolEs.CircularAttackKingToMaster(idx_sel);
                            break;

                        case AbilityTypes.StunElfemale:
                            {
                                eMGame.SelectedE.AbilityTC.Ability = AbilityTypes.StunElfemale;
                                eMGame.CellClickTC.Click = CellClickTypes.UniqueAbility;
                            }
                            break;

                        case AbilityTypes.KingPassiveNearBonus:
                            //E.RpcPoolEs.BonusNearUnits(idx_sel);
                            //TryOnHint(VideoClipTypes.BonusKing);
                            break;


                        //Snowy

                        case AbilityTypes.IncreaseWindSnowy:
                            eMGame.RpcPoolEs.IncreaseWindSnowy_ToMaster(idx_sel);
                            break;

                        case AbilityTypes.DecreaseWindSnowy:
                            eMGame.RpcPoolEs.DecreaseWindSnowy_ToMaster(idx_sel);
                            break;

                        case AbilityTypes.ChangeCornerArcher:
                            eMGame.RpcPoolEs.ChangeCornerArchToMas(idx_sel);
                            break;

                        case AbilityTypes.GrowAdultForest:
                            eMGame.RpcPoolEs.GrowAdultForest(idx_sel);
                            break;

                        case AbilityTypes.ChangeDirectionWind:
                            {
                                eMGame.SelectedE.AbilityTC.Ability = AbilityTypes.ChangeDirectionWind;
                                eMGame.CellClickTC.Click = CellClickTypes.UniqueAbility;
                            }
                            break;

                        case AbilityTypes.SetFarm:
                            {
                                eMGame.RpcPoolEs.BuildFarmToMaster(idx_sel);
                            }
                            break;

                        case AbilityTypes.DestroyBuilding:
                            eMGame.RpcPoolEs.DestroyBuildingToMaster(idx_sel);
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
                            eMGame.SelectedE.AbilityTC.Ability = AbilityTypes.Resurrect;
                            eMGame.CellClickTC.Click = CellClickTypes.UniqueAbility;
                            break;

                        case AbilityTypes.SetTeleport:
                            eMGame.RpcPoolEs.SetTeleportToMaster(idx_sel);
                            break;

                        case AbilityTypes.Teleport:
                            eMGame.RpcPoolEs.TeleportToMaster(idx_sel);
                            break;

                        case AbilityTypes.InvokeSkeletons:
                            eMGame.RpcPoolEs.InvokeSkeletonsToMaster(idx_sel);
                            break;

                        default: throw new Exception();
                    }
                }

                else eMGame.Sound(ClipTypes.Mistake).Action.Invoke();
            }
            else eMGame.Sound(ClipTypes.Mistake).Action.Invoke();
        }
    }
}