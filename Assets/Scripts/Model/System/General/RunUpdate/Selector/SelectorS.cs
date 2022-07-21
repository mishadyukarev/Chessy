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
            var idx_cur = _cellsC.Current;


            if (Input.GetKeyDown(KeyCode.F1))
            {
                _aboutGameC.IsActivatedIdxAndXyInfoCells = !_aboutGameC.IsActivatedIdxAndXyInfoCells;
                _updateAllViewC.NeedUpdateView = true;
            }

            if (Input.GetKeyDown(KeyCode.F2))
            {
                _environmentCs[idx_cur].Dispose();
                _updateAllViewC.NeedUpdateView = true;
            }


            if (_inputC.IsClicked)
            {
                _updateAllViewC.NeedUpdateView = true;
                _mistakeC.MistakeT = MistakeTypes.None;




                if (_aboutGameC.LessonT == LessonTypes.UniqueAttackInfo)
                {
                    _sunC.SunSideT = SunSideTypes.Dawn;
                    _s.SetNextLesson();
                }
                else if (_aboutGameC.LessonT.Is(LessonTypes.YouNeedDestroyKing, LessonTypes.ThatIsYourSpawn, LessonTypes.ClickBuyMarketInTown, LessonTypes.LookInfoAboutSun,
                     LessonTypes.MenuInfo))
                {
                    _s.SetNextLesson();
                }

                switch (_aboutGameC.RaycastT)
                {
                    case RaycastTypes.Cell:
                        {
                            switch (_aboutGameC.CellClickT)
                            {
                                case CellClickTypes.SimpleClick:
                                    _cellSimpleClickS.Execute();
                                    break;

                                case CellClickTypes.SetUnit:
                                    {
                                        _rpcC.Action0(_rpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(_s.TrySetUnitOnCellM), _cellsC.Current, _selectedUnitC.UnitT });
                                        _aboutGameC.CellClickT = CellClickTypes.SimpleClick;
                                    }
                                    break;

                                case CellClickTypes.GiveTakeTW:
                                    {
                                        _cellsC.Selected = _cellsC.Current;

                                        if (_unitCs[idx_cur].UnitT == UnitTypes.Pawn && _unitCs[idx_cur].PlayerT == _aboutGameC.CurrentPlayerIT)
                                        {
                                            _rpcC.Action0(_rpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(_s.TryGiveTakeToolOrWeaponToUnitOnCellM), _cellsC.Current, _selectedToolWeaponC.ToolWeaponT, _selectedToolWeaponC.LevelT });
                                        }
                                        else
                                        {
                                            _aboutGameC.CellClickT = CellClickTypes.SimpleClick;
                                            _cellsC.PreviousSelected = _cellsC.Selected;
                                            _cellsC.Selected = _cellsC.Current;
                                        }
                                    }
                                    break;

                                case CellClickTypes.UniqueAbility:
                                    {
                                        switch (_aboutGameC.AbilityT)
                                        {
                                            case AbilityTypes.FireArcher:
                                                _rpcC.Action0(_rpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(_s.UnitSs.UnitAbilitiesSs.TryFireForestWithArcherM), _cellsC.Selected, _cellsC.Current });
                                                break;

                                            case AbilityTypes.StunElfemale:
                                                _rpcC.Action0(_rpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(_s.UnitSs.UnitAbilitiesSs.TryStunEnemyWithElfemaleM), _cellsC.Selected, _cellsC.Current });
                                                break;

                                            case AbilityTypes.ChangeDirectionWind:
                                                {
                                                    if (!_cloudCs[_cellsC.Current].IsCenter)
                                                    {
                                                        _rpcC.Action0(_rpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(_s.UnitSs.UnitAbilitiesSs.TryChangeDirectWindWithSnowyM), _cellsC.Selected, _cellsC.Current });
                                                    }
                                                }
                                                break;

                                            default: throw new Exception();
                                        }

                                        _aboutGameC.CellClickT = CellClickTypes.SimpleClick;
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
                            if (!_aboutGameC.LessonT.HaveLesson()/* Is(LessonTypes.RelaxExtractPawn, LessonTypes.SeedingPawn)*/)
                            {
                                _aboutGameC.CellClickT = CellClickTypes.SimpleClick;

                                _cellsC.PreviousSelected = _cellsC.Selected;
                                _cellsC.Selected = 0;

                                _aboutGameC.IsSelectedCity = false;

                                //eMG.LessonTC.SetPreviousLesson();
                            }
                        }
                        break;

                    default: throw new Exception();
                }
            }

            else
            {
                if (_aboutGameC.RaycastT == RaycastTypes.Cell)
                {

                }
            }
        }
    }
}