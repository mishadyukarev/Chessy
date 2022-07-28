using Photon.Pun;
using System;
namespace Chessy.Model.System
{
    public sealed partial class ForButtonsSystemsModel
    {
        public void Click(in ButtonTypes uniqueButton)
        {

            var cellIdxSelected = IndexesCellsC.Selected;

            var abil = UnitButtonsC(cellIdxSelected).Ability(uniqueButton);

            if (!_effectsUnitCs[cellIdxSelected].IsStunned)
            {
                if (!_cooldownAbilityCs[cellIdxSelected].HaveCooldown(abil))
                {
                    switch (abil)
                    {
                        case AbilityTypes.FirePawn:
                            _rpcC.Action0(_rpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(_s.UnitSs.UnitAbilitiesSs.TryFireForestWithSimplePawnM), cellIdxSelected });
                            break;

                        case AbilityTypes.PutOutFirePawn:
                            _rpcC.Action0(_rpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(_s.UnitSs.UnitAbilitiesSs.TryPutOutFireForestWithSimplePawnM), cellIdxSelected });
                            break;

                        case AbilityTypes.Seed:
                            _rpcC.Action0(_rpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(_s.TrySeedYoungForestOnCellWithPawnM), cellIdxSelected });

                            break;

                        case AbilityTypes.FireArcher:
                            AboutGameC.AbilityT = AbilityTypes.FireArcher;
                            AboutGameC.CellClickT = CellClickTypes.UniqueAbility;
                            break;

                        case AbilityTypes.CircularAttack:
                            _rpcC.Action0(_rpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(_s.UnitSs.UnitAbilitiesSs.CircularAttackKingM), cellIdxSelected });
                            break;

                        case AbilityTypes.StunElfemale:
                            {
                                AboutGameC.AbilityT = AbilityTypes.StunElfemale;
                                AboutGameC.CellClickT = CellClickTypes.UniqueAbility;
                            }
                            break;

                        case AbilityTypes.KingPassiveNearBonus:
                            //E.RpcPoolEs.BonusNearUnits(idx_sel);
                            //TryOnHint(VideoClipTypes.BonusKing);
                            break;


                        //Snowy

                        case AbilityTypes.IncreaseWindSnowy:
                            _rpcC.Action0(_rpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(_s.UnitSs.UnitAbilitiesSs.IncreaseWindWithRainyM), cellIdxSelected });
                            break;

                        case AbilityTypes.DecreaseWindSnowy:
                            _rpcC.Action0(_rpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(_s.UnitSs.UnitAbilitiesSs.DecreaseWindWithRainyM), cellIdxSelected });
                            break;

                        case AbilityTypes.ChangeCornerArcher:
                            _rpcC.Action0(_rpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(_s.UnitSs.UnitAbilitiesSs.TryChangeCornerArcher), cellIdxSelected });
                            break;

                        case AbilityTypes.GrowAdultForest:
                            _rpcC.Action0(_rpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(_s.UnitSs.UnitAbilitiesSs.TryGrowAdultForestWithElfemaleM), cellIdxSelected });
                            break;

                        case AbilityTypes.ChangeDirectionWind:
                            {
                                AboutGameC.AbilityT = AbilityTypes.ChangeDirectionWind;
                                AboutGameC.CellClickT = CellClickTypes.UniqueAbility;
                            }
                            break;

                        case AbilityTypes.SetFarm:
                            _rpcC.Action0(_rpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(_s.TryBuildFarmOnCellWithSimplePawnM), cellIdxSelected });
                            break;

                        case AbilityTypes.DestroyBuilding:
                            _rpcC.Action0(_rpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(_s.UnitSs.TryDestroyBuildingWithSimplePawnM), cellIdxSelected });
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

                else _dataFromViewC.SoundAction(ClipTypes.Mistake).Invoke();
            }

            else _dataFromViewC.SoundAction(ClipTypes.Mistake).Invoke();
        }
    }
}