using Chessy.Common;
using Chessy.Game.Model.Entity;
using System;

namespace Chessy.Game.Model.System
{
    public sealed class AbilityClickS : SystemModel
    {
        internal AbilityClickS(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG) { }

        public void Click(in ButtonTypes uniqueButton)
        {
            if (eMG.CurPlayerIT == eMG.WhoseMovePlayerT)
            {
                var cell_sel = eMG.SelectedCell;

                var abil = eMG.UnitButtonAbilitiesC(cell_sel).Ability(uniqueButton);

                if (!eMG.StunUnitC(cell_sel).IsStunned)
                {
                    if (!eMG.UnitCooldownAbilitiesC(cell_sel).HaveCooldown(abil))
                    {
                        switch (abil)
                        {
                            case AbilityTypes.FirePawn:
                                eMG.RpcPoolEs.FirePawnToMas(cell_sel);
                                break;

                            case AbilityTypes.PutOutFirePawn:
                                eMG.RpcPoolEs.PutOutFirePawnToMas(cell_sel);
                                break;

                            case AbilityTypes.Seed:
                                eMG.RpcPoolEs.SeedEnvToMaster(cell_sel, EnvironmentTypes.YoungForest);
                                break;

                            case AbilityTypes.FireArcher:
                                eMG.SelectedE.AbilityTC.Ability = AbilityTypes.FireArcher;
                                eMG.CellClickTC.CellClickT = CellClickTypes.UniqueAbility;
                                break;

                            case AbilityTypes.CircularAttack:
                                eMG.RpcPoolEs.CircularAttackKingToMaster(cell_sel);
                                break;

                            case AbilityTypes.StunElfemale:
                                {
                                    eMG.SelectedE.AbilityTC.Ability = AbilityTypes.StunElfemale;
                                    eMG.CellClickTC.CellClickT = CellClickTypes.UniqueAbility;
                                }
                                break;

                            case AbilityTypes.KingPassiveNearBonus:
                                //E.RpcPoolEs.BonusNearUnits(idx_sel);
                                //TryOnHint(VideoClipTypes.BonusKing);
                                break;


                            //Snowy

                            case AbilityTypes.IncreaseWindSnowy:
                                eMG.RpcPoolEs.IncreaseWindSnowy_ToMaster(cell_sel);
                                break;

                            case AbilityTypes.DecreaseWindSnowy:
                                eMG.RpcPoolEs.DecreaseWindSnowy_ToMaster(cell_sel);
                                break;

                            case AbilityTypes.ChangeCornerArcher:
                                eMG.RpcPoolEs.ChangeCornerArchToMas(cell_sel);
                                break;

                            case AbilityTypes.GrowAdultForest:
                                eMG.RpcPoolEs.GrowAdultForest(cell_sel);
                                break;

                            case AbilityTypes.ChangeDirectionWind:
                                {
                                    eMG.SelectedE.AbilityTC.Ability = AbilityTypes.ChangeDirectionWind;
                                    eMG.CellClickTC.CellClickT = CellClickTypes.UniqueAbility;
                                }
                                break;

                            case AbilityTypes.SetFarm:
                                {
                                    eMG.RpcPoolEs.BuildFarmToMaster(cell_sel);
                                }
                                break;

                            case AbilityTypes.DestroyBuilding:
                                eMG.RpcPoolEs.DestroyBuildingToMaster(cell_sel);
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
                                eMG.SelectedE.AbilityTC.Ability = AbilityTypes.Resurrect;
                                eMG.CellClickTC.CellClickT = CellClickTypes.UniqueAbility;
                                break;

                            case AbilityTypes.SetTeleport:
                                eMG.RpcPoolEs.SetTeleportToMaster(cell_sel);
                                break;

                            case AbilityTypes.Teleport:
                                eMG.RpcPoolEs.TeleportToMaster(cell_sel);
                                break;

                            case AbilityTypes.InvokeSkeletons:
                                eMG.RpcPoolEs.InvokeSkeletonsToMaster(cell_sel);
                                break;

                            default: throw new Exception();
                        }
                    }

                    else eMG.SoundActionC(ClipTypes.Mistake).Action.Invoke();
                }

                else eMG.SoundActionC(ClipTypes.Mistake).Action.Invoke();
            }

            else sMG.MistakeSs.MistakeS.Mistake(MistakeTypes.NeedWaitQueue);


            eMG.NeedUpdateView = true;
        }
    }
}