using Chessy.Common;
using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;
using System;

namespace Chessy.Game.Model.System
{
    public sealed class AbilityClickS : SystemModel
    {
        internal AbilityClickS(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        public void Click(in ButtonTypes uniqueButton)
        {
            if (eMG.CurPlayerITC.Is(eMG.WhoseMovePlayerTC.PlayerT))
            {
                var idx_sel = eMG.CellsC.Selected;

                var abil = eMG.UnitButtonAbilitiesC(idx_sel).Ability(uniqueButton);

                if (!eMG.UnitCooldownAbilitiesC(idx_sel).HaveCooldown(abil))
                {
                    switch (abil)
                    {
                        case AbilityTypes.FirePawn:
                            eMG.RpcPoolEs.FirePawnToMas(idx_sel);
                            break;

                        case AbilityTypes.PutOutFirePawn:
                            eMG.RpcPoolEs.PutOutFirePawnToMas(idx_sel);
                            break;

                        case AbilityTypes.Seed:
                            eMG.RpcPoolEs.SeedEnvToMaster(idx_sel, EnvironmentTypes.YoungForest);
                            break;

                        case AbilityTypes.FireArcher:
                            eMG.SelectedE.AbilityTC.Ability = AbilityTypes.FireArcher;
                            eMG.CellClickTC.CellClickT = CellClickTypes.UniqueAbility;
                            break;

                        case AbilityTypes.CircularAttack:
                            eMG.RpcPoolEs.CircularAttackKingToMaster(idx_sel);
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
                            eMG.RpcPoolEs.IncreaseWindSnowy_ToMaster(idx_sel);
                            break;

                        case AbilityTypes.DecreaseWindSnowy:
                            eMG.RpcPoolEs.DecreaseWindSnowy_ToMaster(idx_sel);
                            break;

                        case AbilityTypes.ChangeCornerArcher:
                            eMG.RpcPoolEs.ChangeCornerArchToMas(idx_sel);
                            break;

                        case AbilityTypes.GrowAdultForest:
                            eMG.RpcPoolEs.GrowAdultForest(idx_sel);
                            break;

                        case AbilityTypes.ChangeDirectionWind:
                            {
                                eMG.SelectedE.AbilityTC.Ability = AbilityTypes.ChangeDirectionWind;
                                eMG.CellClickTC.CellClickT = CellClickTypes.UniqueAbility;
                            }
                            break;

                        case AbilityTypes.SetFarm:
                            {
                                eMG.RpcPoolEs.BuildFarmToMaster(idx_sel);
                            }
                            break;

                        case AbilityTypes.DestroyBuilding:
                            eMG.RpcPoolEs.DestroyBuildingToMaster(idx_sel);
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
                            eMG.RpcPoolEs.SetTeleportToMaster(idx_sel);
                            break;

                        case AbilityTypes.Teleport:
                            eMG.RpcPoolEs.TeleportToMaster(idx_sel);
                            break;

                        case AbilityTypes.InvokeSkeletons:
                            eMG.RpcPoolEs.InvokeSkeletonsToMaster(idx_sel);
                            break;

                        default: throw new Exception();
                    }
                }

                else eMG.SoundActionC(ClipTypes.Mistake).Action.Invoke();
            }

            else sMG.MistakeS.Mistake(MistakeTypes.NeedWaitQueue);


            eMG.NeedUpdateView = true;
        }
    }
}