using Chessy.Common;
using Photon.Pun;
using System;

namespace Chessy.Model
{
    public sealed partial class SystemsModelGameForUI
    {
        public void Click(in ButtonTypes uniqueButton)
        {
            if (_e.CurPlayerIT == _e.WhoseMovePlayerT)
            {
                var cellIdxSelected = _e.SelectedCellIdx;

                var abil = _e.UnitButtonAbilitiesC(cellIdxSelected).Ability(uniqueButton);

                if (!_e.UnitEffectsC(cellIdxSelected).IsStunned)
                {
                    if (!_e.UnitCooldownAbilitiesC(cellIdxSelected).HaveCooldown(abil))
                    {
                        switch (abil)
                        {
                            case AbilityTypes.FirePawn:
                                _e.RpcC.Action0(_e.RpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(_s.UnitSs.UnitAbilitiesSs.TryFireForestWithSimplePawnM), cellIdxSelected });
                                break;

                            case AbilityTypes.PutOutFirePawn:
                                _e.RpcC.Action0(_e.RpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(_s.UnitSs.UnitAbilitiesSs.TryPutOutFireForestWithSimplePawnM), cellIdxSelected });
                                break;

                            case AbilityTypes.Seed:
                                _e.RpcC.Action0(_e.RpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(_s.TrySeedYoungForestOnCellWithPawnM), cellIdxSelected });

                                break;

                            case AbilityTypes.FireArcher:
                                _e.SelectedE.AbilityT = AbilityTypes.FireArcher;
                                _e.CellClickT = CellClickTypes.UniqueAbility;
                                break;

                            case AbilityTypes.CircularAttack:
                                _e.RpcC.Action0(_e.RpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(_s.UnitSs.UnitAbilitiesSs.CurcularAttackKingM), cellIdxSelected });
                                break;

                            case AbilityTypes.StunElfemale:
                                {
                                    _e.SelectedE.AbilityT = AbilityTypes.StunElfemale;
                                    _e.CellClickT = CellClickTypes.UniqueAbility;
                                }
                                break;

                            case AbilityTypes.KingPassiveNearBonus:
                                //E.RpcPoolEs.BonusNearUnits(idx_sel);
                                //TryOnHint(VideoClipTypes.BonusKing);
                                break;


                            //Snowy

                            case AbilityTypes.IncreaseWindSnowy:
                                _e.RpcC.Action0(_e.RpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(_s.UnitSs.UnitAbilitiesSs.IncreaseWindWithRainyM), cellIdxSelected });
                                break;

                            case AbilityTypes.DecreaseWindSnowy:
                                _e.RpcC.Action0(_e.RpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(_s.UnitSs.UnitAbilitiesSs.DecreaseWindWithRainyM), cellIdxSelected });
                                break;

                            case AbilityTypes.ChangeCornerArcher:
                                _e.RpcC.Action0(_e.RpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(_s.UnitSs.UnitAbilitiesSs.TryChangeCornerArcher), cellIdxSelected });
                                break;

                            case AbilityTypes.GrowAdultForest:
                                _e.RpcC.Action0(_e.RpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(_s.UnitSs.UnitAbilitiesSs.TryGrowAdultForestWithElfemaleM), cellIdxSelected });
                                break;

                            case AbilityTypes.ChangeDirectionWind:
                                {
                                    _e.SelectedE.AbilityT = AbilityTypes.ChangeDirectionWind;
                                    _e.CellClickT = CellClickTypes.UniqueAbility;
                                }
                                break;

                            case AbilityTypes.SetFarm:
                                _e.RpcC.Action0(_e.RpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(_s.TryBuildFarmOnCellWithSimplePawnM), cellIdxSelected });
                                break;

                            case AbilityTypes.DestroyBuilding:
                                _e.RpcC.Action0(_e.RpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(_s.UnitSs.TryDestroyBuildingWithSimplePawnM), cellIdxSelected });
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

                    else _e.SoundAction(ClipTypes.Mistake).Invoke();
                }

                else _e.SoundAction(ClipTypes.Mistake).Invoke();
            }

            else _s.Mistake(MistakeTypes.NeedWaitQueue);


            _e.NeedUpdateView = true;
        }
    }
}