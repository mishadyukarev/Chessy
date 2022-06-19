using Chessy.Game.Enum;
using Chessy.Game.Model.Entity;
using Chessy.Game.Values;
using Photon.Pun;
using System;
using UnityEngine;

namespace Chessy.Game.Model.System
{
    sealed class SelectorS : SystemModel, IUpdate
    {
        readonly CellSimpleClickS _cellSimpleClickS;

        public SelectorS(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG)
        {
            _cellSimpleClickS = new CellSimpleClickS(sMG, eMG);
        }

        public void Update()
        {
            var idx_cur = _eMG.CellsC.Current;


            if (Input.GetKeyDown(KeyCode.F1))
            {
                _eMG.IsActivatedIdxAndXyInfoCells = !_eMG.IsActivatedIdxAndXyInfoCells;
                _eMG.NeedUpdateView = true;
            }

            if (Input.GetKeyDown(KeyCode.F2))
            {
                _sMG.ClearAllEnvironment(idx_cur);
                _eMG.NeedUpdateView = true;
            }


            if (_eMG.IsClicked)
            {
                _eMG.NeedUpdateView = true;
                _eMG.MistakeTC.MistakeT = MistakeTypes.None;




                if (_eMG.LessonT == LessonTypes.UniqueAttackInfo)
                {
                    _eMG.WeatherE.SunSideTC.SunSideT = SunSideTypes.Dawn;
                    _eMG.LessonTC.SetNextLesson();
                }
                else if (_eMG.LessonTC.Is(LessonTypes.YouNeedDestroyKing, LessonTypes.ThatIsYourSpawn, LessonTypes.ClickBuyMarketInTown, LessonTypes.LookInfoAboutSun, 
                    LessonTypes.ThatsYourEffects, LessonTypes.ThatsYourDamage, LessonTypes.MenuInfo))
                {
                    _eMG.LessonTC.SetNextLesson();
                }

                switch (_eMG.RaycastTC.RaycastT)
                {
                    case RaycastTypes.Cell:
                        {
                            if (_eMG.CurPlayerITC.Is(_eMG.WhoseMovePlayerTC.PlayerT))
                            {
                                switch (_eMG.CellClickTC.CellClickT)
                                {
                                    case CellClickTypes.SimpleClick:
                                        _cellSimpleClickS.Execute();
                                        break;

                                    case CellClickTypes.SetUnit:
                                        {
                                            _eMG.RpcPoolEs.Action0(_eMG.RpcPoolEs.MasterRPCName, RpcTarget.MasterClient, new object[] { nameof(_sMG.TrySetUnitOnCellM), _eMG.CellsC.Current, _eMG.SelectedUnitE.UnitTC.UnitT });
                                            _eMG.CellClickTC.CellClickT = CellClickTypes.SimpleClick;
                                        }
                                        break;

                                    case CellClickTypes.GiveTakeTW:
                                        {
                                            _eMG.CellsC.Selected = _eMG.CellsC.Current;

                                            if (_eMG.UnitTC(idx_cur).Is(UnitTypes.Pawn) && _eMG.UnitPlayerTC(idx_cur).Is(_eMG.CurPlayerITC.PlayerT))
                                            {
                                                _eMG.RpcPoolEs.Action0(_eMG.RpcPoolEs.MasterRPCName, RpcTarget.MasterClient, new object[] { nameof(_sMG.TryGiveTakeToolOrWeaponToUnitOnCellM), _eMG.CurrentCellIdx, _eMG.SelectedE.ToolWeaponC.ToolWeaponT, _eMG.SelectedE.ToolWeaponC.LevelT });
                                            }
                                            else
                                            {
                                                _eMG.CellClickTC.CellClickT = CellClickTypes.SimpleClick;
                                                _eMG.CellsC.PreviousSelected = _eMG.CellsC.Selected;
                                                _eMG.CellsC.Selected = _eMG.CellsC.Current;
                                            }
                                        }
                                        break;

                                    case CellClickTypes.UniqueAbility:
                                        {
                                            switch (_eMG.SelectedE.AbilityTC.Ability)
                                            {
                                                case AbilityTypes.FireArcher:
                                                    _eMG.RpcPoolEs.Action0(_eMG.RpcPoolEs.MasterRPCName, RpcTarget.MasterClient, new object[] { nameof(_sMG.UnitSs.UnitAbilitiesSs.TryFireForestWithArcherM), _eMG.CellsC.Selected, _eMG.CellsC.Current });
                                                    break;

                                                case AbilityTypes.StunElfemale:
                                                    _eMG.RpcPoolEs.Action0(_eMG.RpcPoolEs.MasterRPCName, RpcTarget.MasterClient, new object[] { nameof(_sMG.UnitSs.UnitAbilitiesSs.TryStunEnemyWithElfemaleM), _eMG.CellsC.Selected, _eMG.CellsC.Current });
                                                    break;

                                                case AbilityTypes.ChangeDirectionWind:
                                                    {
                                                        foreach (var cellE in _eMG.AroundCellsE(_eMG.WeatherE.CloudC.Center).CellsAround)
                                                        {
                                                            if (cellE == _eMG.CellsC.Current)
                                                            {
                                                                _eMG.RpcPoolEs.Action0(_eMG.RpcPoolEs.MasterRPCName, RpcTarget.MasterClient, new object[] { nameof(_sMG.UnitSs.UnitAbilitiesSs.TryPutOutFireWithSimplePawnM), _eMG.CellsC.Selected, _eMG.CellsC.Current });
                                                            }
                                                        }
                                                    }
                                                    break;

                                                default: throw new Exception();
                                            }

                                            _eMG.CellClickTC.CellClickT = CellClickTypes.SimpleClick;
                                        }
                                        break;

                                    default: throw new Exception();
                                }
                            }

                            else
                            {
                                _eMG.CellsC.Selected = _eMG.CellsC.Current;
                            }
                        }
                        break;

                    case RaycastTypes.UI:
                        break;

                    case RaycastTypes.Background:
                        {
                            if (!_eMG.LessonTC.HaveLesson/* Is(LessonTypes.RelaxExtractPawn, LessonTypes.SeedingPawn)*/)
                            {
                                _eMG.CellClickTC.CellClickT = CellClickTypes.SimpleClick;

                                _eMG.CellsC.PreviousSelected = _eMG.CellsC.Selected;
                                _eMG.SelectedCell = 0;

                                _eMG.IsSelectedCity = false;

                                //eMG.LessonTC.SetPreviousLesson();
                            }
                        }
                        break;

                    default: throw new Exception();
                }
            }

            else
            {
                if (_eMG.RaycastTC.Is(RaycastTypes.Cell))
                {

                }
            }
        }
    }
}