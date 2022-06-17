using Chessy.Common;
using Chessy.Game.Model.Entity;
using System;

namespace Chessy.Game.Model.System
{
    public sealed partial class SystemsModelGameForUI
    {
        public void Click(in ButtonTypes uniqueButton)
        {
            if (_eMG.CurPlayerIT == _eMG.WhoseMovePlayerT)
            {
                var cell_sel = _eMG.SelectedCell;

                var abil = _eMG.UnitButtonAbilitiesC(cell_sel).Ability(uniqueButton);

                if (!_eMG.StunUnitC(cell_sel).IsStunned)
                {
                    if (!_eMG.UnitCooldownAbilitiesC(cell_sel).HaveCooldown(abil))
                    {
                        switch (abil)
                        {
                            case AbilityTypes.FirePawn:
                                _eMG.RpcPoolEs.FirePawnToMas(cell_sel);
                                break;

                            case AbilityTypes.PutOutFirePawn:
                                _eMG.RpcPoolEs.PutOutFirePawnToMas(cell_sel);
                                break;

                            case AbilityTypes.Seed:
                                _eMG.RpcPoolEs.SeedEnvToMaster(cell_sel, EnvironmentTypes.YoungForest);
                                break;

                            case AbilityTypes.FireArcher:
                                _eMG.SelectedE.AbilityTC.Ability = AbilityTypes.FireArcher;
                                _eMG.CellClickTC.CellClickT = CellClickTypes.UniqueAbility;
                                break;

                            case AbilityTypes.CircularAttack:
                                _eMG.RpcPoolEs.CircularAttackKingToMaster(cell_sel);
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
                                _eMG.RpcPoolEs.IncreaseWindSnowy_ToMaster(cell_sel);
                                break;

                            case AbilityTypes.DecreaseWindSnowy:
                                _eMG.RpcPoolEs.DecreaseWindSnowy_ToMaster(cell_sel);
                                break;

                            case AbilityTypes.ChangeCornerArcher:
                                _eMG.RpcPoolEs.ChangeCornerArchToMas(cell_sel);
                                break;

                            case AbilityTypes.GrowAdultForest:
                                _eMG.RpcPoolEs.GrowAdultForest(cell_sel);
                                break;

                            case AbilityTypes.ChangeDirectionWind:
                                {
                                    _eMG.SelectedE.AbilityTC.Ability = AbilityTypes.ChangeDirectionWind;
                                    _eMG.CellClickTC.CellClickT = CellClickTypes.UniqueAbility;
                                }
                                break;

                            case AbilityTypes.SetFarm:
                                {
                                    _eMG.RpcPoolEs.BuildFarmToMaster(cell_sel);
                                }
                                break;

                            case AbilityTypes.DestroyBuilding:
                                _eMG.RpcPoolEs.DestroyBuildingToMaster(cell_sel);
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
                                _eMG.SelectedE.AbilityTC.Ability = AbilityTypes.Resurrect;
                                _eMG.CellClickTC.CellClickT = CellClickTypes.UniqueAbility;
                                break;

                            case AbilityTypes.SetTeleport:
                                _eMG.RpcPoolEs.SetTeleportToMaster(cell_sel);
                                break;

                            case AbilityTypes.Teleport:
                                _eMG.RpcPoolEs.TeleportToMaster(cell_sel);
                                break;

                            case AbilityTypes.InvokeSkeletons:
                                _eMG.RpcPoolEs.InvokeSkeletonsToMaster(cell_sel);
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