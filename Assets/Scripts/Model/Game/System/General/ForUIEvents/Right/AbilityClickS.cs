using Chessy.Common;
using Chessy.Game.Model.Entity;
using Photon.Pun;
using System;

namespace Chessy.Game.Model.System
{
    public sealed partial class SystemsModelGameForUI
    {
        public void Click(in ButtonTypes uniqueButton)
        {
            if (_eMG.CurPlayerIT == _eMG.WhoseMovePlayerT)
            {
                var cellIdxSelected = _eMG.SelectedCell;

                var abil = _eMG.UnitButtonAbilitiesC(cellIdxSelected).Ability(uniqueButton);

                if (!_eMG.StunUnitC(cellIdxSelected).IsStunned)
                {
                    if (!_eMG.UnitCooldownAbilitiesC(cellIdxSelected).HaveCooldown(abil))
                    {
                        switch (abil)
                        {
                            case AbilityTypes.FirePawn:
                                _eMG.RpcPoolEs.Action0(_eMG.RpcPoolEs.MasterRPCName, RpcTarget.MasterClient, new object[] { nameof(_sMG.UnitSs.UnitAbilitiesSs.TryFireWithSimplePawnM), cellIdxSelected });
                                break;

                            case AbilityTypes.PutOutFirePawn:
                                _eMG.RpcPoolEs.Action0(_eMG.RpcPoolEs.MasterRPCName, RpcTarget.MasterClient, new object[] { nameof(_sMG.UnitSs.UnitAbilitiesSs.TryPutOutFireWithSimplePawnM), cellIdxSelected });
                                break;

                            case AbilityTypes.Seed:
                                _eMG.RpcPoolEs.Action0(_eMG.RpcPoolEs.MasterRPCName, RpcTarget.MasterClient, new object[] { nameof(_sMG.TrySeedYoungForestOnCellWithPawnM), cellIdxSelected });
  
                                break;

                            case AbilityTypes.FireArcher:
                                _eMG.SelectedE.AbilityTC.Ability = AbilityTypes.FireArcher;
                                _eMG.CellClickTC.CellClickT = CellClickTypes.UniqueAbility;
                                break;

                            case AbilityTypes.CircularAttack:
                                _eMG.RpcPoolEs.Action0(_eMG.RpcPoolEs.MasterRPCName, RpcTarget.MasterClient, new object[] { nameof(_sMG.UnitSs.UnitAbilitiesSs.CurcularAttackKingM), cellIdxSelected });
                                break;

                            case AbilityTypes.StunElfemale:
                                {
                                    _eMG.SelectedE.AbilityTC.Ability = AbilityTypes.StunElfemale;
                                    _eMG.CellClickTC.CellClickT = CellClickTypes.UniqueAbility;
                                }
                                break;

                            case AbilityTypes.KingPassiveNearBonus:
                                //E.RpcPoolEs.BonusNearUnits(idx_sel);
                                //TryOnHint(VideoClipTypes.BonusKing);
                                break;


                            //Snowy

                            case AbilityTypes.IncreaseWindSnowy:
                                _eMG.RpcPoolEs.Action0(_eMG.RpcPoolEs.MasterRPCName, RpcTarget.MasterClient, new object[] { nameof(_sMG.UnitSs.UnitAbilitiesSs.IncreaseWindWithRainyM), cellIdxSelected });
                                break;

                            case AbilityTypes.DecreaseWindSnowy:
                                _eMG.RpcPoolEs.Action0(_eMG.RpcPoolEs.MasterRPCName, RpcTarget.MasterClient, new object[] { nameof(_sMG.UnitSs.UnitAbilitiesSs.DecreaseWindWithRainyM), cellIdxSelected });
                                break;

                            case AbilityTypes.ChangeCornerArcher:
                                _eMG.RpcPoolEs.Action0(_eMG.RpcPoolEs.MasterRPCName, RpcTarget.MasterClient, new object[] { nameof(_sMG.UnitSs.UnitAbilitiesSs.TryChangeCornerArcher), cellIdxSelected });
                                break;

                            case AbilityTypes.GrowAdultForest:
                                _eMG.RpcPoolEs.Action0(_eMG.RpcPoolEs.MasterRPCName, RpcTarget.MasterClient, new object[] { nameof(_sMG.UnitSs.UnitAbilitiesSs.TryGrowAdultForestWithElfemaleM), cellIdxSelected });
                                break;

                            case AbilityTypes.ChangeDirectionWind:
                                {
                                    _eMG.SelectedE.AbilityTC.Ability = AbilityTypes.ChangeDirectionWind;
                                    _eMG.CellClickTC.CellClickT = CellClickTypes.UniqueAbility;
                                }
                                break;

                            case AbilityTypes.SetFarm:
                                _eMG.RpcPoolEs.Action0(_eMG.RpcPoolEs.MasterRPCName, RpcTarget.MasterClient, new object[] { nameof(_sMG.TryBuildFarmOnCellWithUnitM), cellIdxSelected });
                                break;

                            case AbilityTypes.DestroyBuilding:
                                _eMG.RpcPoolEs.Action0(_eMG.RpcPoolEs.MasterRPCName, RpcTarget.MasterClient, new object[] { nameof(_sMG.BuildingSs.TryDestroyBuildingWithSimplePawnM), cellIdxSelected });
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

                    else _eMG.SoundAction(ClipTypes.Mistake).Invoke();
                }

                else _eMG.SoundAction(ClipTypes.Mistake).Invoke();
            }

            else _sMG.Mistake(MistakeTypes.NeedWaitQueue);


            _eMG.NeedUpdateView = true;
        }
    }
}