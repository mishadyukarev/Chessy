using Photon.Pun;
using System;
namespace Chessy.Model.System
{
    public sealed partial class ForButtonsSystemsModel
    {
        public void Click(in ButtonTypes uniqueButton)
        {

            var cellIdxSelected = indexesCellsC.Selected;

            var abil = UnitButtonsC(cellIdxSelected).Ability(uniqueButton);

            if (!_effectsUnitCs[cellIdxSelected].IsStunned)
            {
                if (!_cooldownAbilityCs[cellIdxSelected].HaveCooldown(abil))
                {
                    switch (abil)
                    {
                        case AbilityTypes.FirePawn:
                            rpcC.Action0(rpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(s.unitSs.unitAbilitiesSs.TryFireForestWithSimplePawnM), cellIdxSelected });
                            break;

                        case AbilityTypes.PutOutFirePawn:
                            rpcC.Action0(rpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(s.unitSs.unitAbilitiesSs.TryPutOutFireForestWithSimplePawnM), cellIdxSelected });
                            break;

                        case AbilityTypes.Seed:
                            rpcC.Action0(rpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(s.TrySeedYoungForestOnCellWithPawnM), cellIdxSelected });

                            break;

                        case AbilityTypes.FireArcher:
                            aboutGameC.AbilityT = AbilityTypes.FireArcher;
                            aboutGameC.CellClickT = CellClickTypes.UniqueAbility;
                            break;

                        case AbilityTypes.CircularAttack:
                            rpcC.Action0(rpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(s.unitSs.unitAbilitiesSs.CircularAttackKingM), cellIdxSelected });
                            break;

                        case AbilityTypes.StunElfemale:
                            {
                                aboutGameC.AbilityT = AbilityTypes.StunElfemale;
                                aboutGameC.CellClickT = CellClickTypes.UniqueAbility;
                            }
                            break;

                        case AbilityTypes.KingPassiveNearBonus:
                            //E.RpcPoolEs.BonusNearUnits(idx_sel);
                            //TryOnHint(VideoClipTypes.BonusKing);
                            break;


                        //Snowy

                        case AbilityTypes.IncreaseWindSnowy:
                            rpcC.Action0(rpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(s.unitSs.unitAbilitiesSs.IncreaseWindWithRainyM), cellIdxSelected });
                            break;

                        case AbilityTypes.DecreaseWindSnowy:
                            rpcC.Action0(rpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(s.unitSs.unitAbilitiesSs.DecreaseWindWithRainyM), cellIdxSelected });
                            break;

                        case AbilityTypes.ChangeCornerArcher:
                            rpcC.Action0(rpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(s.unitSs.unitAbilitiesSs.changeCornerArcherS.TryChangeCornerArcher), cellIdxSelected });
                            break;

                        case AbilityTypes.GrowAdultForest:
                            rpcC.Action0(rpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(s.unitSs.unitAbilitiesSs.TryGrowAdultForestWithElfemaleM), cellIdxSelected });
                            break;

                        case AbilityTypes.ChangeDirectionWind:
                            {
                                aboutGameC.AbilityT = AbilityTypes.ChangeDirectionWind;
                                aboutGameC.CellClickT = CellClickTypes.UniqueAbility;
                            }
                            break;

                        case AbilityTypes.SetFarm:
                            rpcC.Action0(rpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(s.TryBuildFarmOnCellWithSimplePawnM), cellIdxSelected });
                            break;

                        case AbilityTypes.DestroyBuilding:
                            rpcC.Action0(rpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(s.unitSs.TryDestroyBuildingWithSimplePawnM), cellIdxSelected });
                            break;


                        case AbilityTypes.Resurrect:
                            break;

                        case AbilityTypes.SetTeleport:
                            break;

                        case AbilityTypes.Teleport:
                            break;

                        case AbilityTypes.InvokeSkeletons:

                            break;

                        default: throw new Exception();
                    }
                }

                else dataFromViewC.SoundAction(ClipTypes.Mistake).Invoke();
            }

            else dataFromViewC.SoundAction(ClipTypes.Mistake).Invoke();
        }
    }
}