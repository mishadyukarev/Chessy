using Chessy.Model.Entity;
using Chessy.Model.Enum;
using Chessy.Model.System;
using Photon.Pun;
using System;
using UnityEngine;
namespace Chessy.Model
{
    sealed class SelectorS : SystemModelAbstract, IUpdate
    {
        readonly CellSimpleClickS _cellSimpleClickS;

        public SelectorS(in SystemsModel sMG, in EntitiesModel eMG) : base(sMG, eMG)
        {
            _cellSimpleClickS = new CellSimpleClickS(sMG, eMG);
        }

        public void Update()
        {
            var idx_cur = _e.CurrentCellIdx;


            if (Input.GetKeyDown(KeyCode.F1))
            {
                _e.IsActivatedIdxAndXyInfoCells = !_e.IsActivatedIdxAndXyInfoCells;
                _e.NeedUpdateView = true;
            }

            if (Input.GetKeyDown(KeyCode.F2))
            {
                _s.ClearAllEnvironment(idx_cur);
                _e.NeedUpdateView = true;
            }


            if (_e.IsClicked)
            {
                _e.NeedUpdateView = true;
                _e.MistakeT = MistakeTypes.None;




                if (_e.LessonT == LessonTypes.UniqueAttackInfo)
                {
                    _e.SunSideT = SunSideTypes.Dawn;
                     _s.SetNextLesson();
                }
                else if (_e.LessonT.Is(LessonTypes.YouNeedDestroyKing, LessonTypes.ThatIsYourSpawn, LessonTypes.ClickBuyMarketInTown, LessonTypes.LookInfoAboutSun,
                    LessonTypes.ThatsYourEffects, LessonTypes.ThatsYourDamage, LessonTypes.MenuInfo))
                {
                     _s.SetNextLesson();
                }

                switch (_e.RaycastT)
                {
                    case RaycastTypes.Cell:
                        {
                            switch (_e.CellClickT)
                            {
                                case CellClickTypes.SimpleClick:
                                    _cellSimpleClickS.Execute();
                                    break;

                                case CellClickTypes.SetUnit:
                                    {
                                        _e.RpcC.Action0(_e.RpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(_s.TrySetUnitOnCellM), _e.CurrentCellIdx, _e.SelectedUnitC.UnitT });
                                        _e.CellClickT = CellClickTypes.SimpleClick;
                                    }
                                    break;

                                case CellClickTypes.GiveTakeTW:
                                    {
                                        _e.SelectedCellIdx = _e.CurrentCellIdx;

                                        if (_e.UnitT(idx_cur).Is(UnitTypes.Pawn) && _e.UnitPlayerT(idx_cur).Is(_e.CurrentPlayerIT))
                                        {
                                            _e.RpcC.Action0(_e.RpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(_s.TryGiveTakeToolOrWeaponToUnitOnCellM), _e.CurrentCellIdx, _e.SelectedE.ToolWeaponC.ToolWeaponT, _e.SelectedE.ToolWeaponC.LevelT });
                                        }
                                        else
                                        {
                                            _e.CellClickT = CellClickTypes.SimpleClick;
                                            _e.CellsC.PreviousSelected = _e.SelectedCellIdx;
                                            _e.SelectedCellIdx = _e.CurrentCellIdx;
                                        }
                                    }
                                    break;

                                case CellClickTypes.UniqueAbility:
                                    {
                                        switch (_e.SelectedE.AbilityT)
                                        {
                                            case AbilityTypes.FireArcher:
                                                _e.RpcC.Action0(_e.RpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(_s.UnitSs.UnitAbilitiesSs.TryFireForestWithArcherM), _e.SelectedCellIdx, _e.CurrentCellIdx });
                                                break;

                                            case AbilityTypes.StunElfemale:
                                                _e.RpcC.Action0(_e.RpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(_s.UnitSs.UnitAbilitiesSs.TryStunEnemyWithElfemaleM), _e.SelectedCellIdx, _e.CurrentCellIdx });
                                                break;

                                            case AbilityTypes.ChangeDirectionWind:
                                                {
                                                    if (_e.HaveCloud(_e.CurrentCellIdx) && !_e.IsCenterCloud(_e.CurrentCellIdx))
                                                    {
                                                        _e.RpcC.Action0(_e.RpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(_s.UnitSs.UnitAbilitiesSs.TryChangeDirectWindWithSnowyM), _e.SelectedCellIdx, _e.CurrentCellIdx });
                                                    }
                                                }
                                                break;

                                            default: throw new Exception();
                                        }

                                        _e.CellClickT = CellClickTypes.SimpleClick;
                                    }
                                    break;

                                default: throw new Exception();
                            }
                        }
                        break;

                    case RaycastTypes.UI:
                        break;

                    case RaycastTypes.Background:
                        {
                            if (!_e.LessonT.HaveLesson()/* Is(LessonTypes.RelaxExtractPawn, LessonTypes.SeedingPawn)*/)
                            {
                                _e.CellClickT = CellClickTypes.SimpleClick;

                                _e.CellsC.PreviousSelected = _e.SelectedCellIdx;
                                _e.SelectedCellIdx = 0;

                                _e.IsSelectedCity = false;

                                //eMG.LessonTC.SetPreviousLesson();
                            }
                        }
                        break;

                    default: throw new Exception();
                }
            }

            else
            {
                if (_e.RaycastT == RaycastTypes.Cell)
                {

                }
            }
        }
    }
}