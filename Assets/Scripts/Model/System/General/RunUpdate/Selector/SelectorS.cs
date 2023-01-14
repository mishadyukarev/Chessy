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
            var idx_cur = IndexesCellsC.Current;


            if (Input.GetKeyDown(KeyCode.F1))
            {
                AboutGameC.IsActivatedIdxAndXyInfoCells = !AboutGameC.IsActivatedIdxAndXyInfoCells;
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




                if (AboutGameC.LessonT == LessonTypes.UniqueAttackInfo)
                {
                    SunC.SunSideT = SunSideTypes.Dawn;
                    _s.SetNextLesson();
                }
                else if (AboutGameC.LessonT.Is(LessonTypes.YouNeedDestroyKing, LessonTypes.ThatIsYourSpawn, LessonTypes.ClickBuyMarketInTown, LessonTypes.LookInfoAboutSun,
                     LessonTypes.MenuInfo))
                {
                    if (!_settingsC.IsOpenedBarWithSettings)
                    {
                        _s.SetNextLesson();
                    }   
                }

                switch (AboutGameC.RaycastT)
                {
                    case RaycastTypes.Cell:
                        {
                            switch (AboutGameC.CellClickT)
                            {
                                case CellClickTypes.SimpleClick:
                                    _cellSimpleClickS.Execute();
                                    break;

                                case CellClickTypes.SetUnit:
                                    {
                                        _rpcC.Action0(_rpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(_s.TrySetUnitOnCellM), IndexesCellsC.Current, _selectedUnitC.UnitT });
                                        AboutGameC.CellClickT = CellClickTypes.SimpleClick;
                                    }
                                    break;

                                case CellClickTypes.GiveTakeTW:
                                    {
                                        IndexesCellsC.Selected = IndexesCellsC.Current;

                                        if (_unitCs[idx_cur].UnitT == UnitTypes.Pawn && _unitCs[idx_cur].PlayerT == AboutGameC.CurrentPlayerIT)
                                        {
                                            _rpcC.Action0(_rpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(_s.TryGiveTakeToolOrWeaponToUnitOnCellM), IndexesCellsC.Current, _selectedToolWeaponC.ToolWeaponT, _selectedToolWeaponC.LevelT });
                                        }
                                        else
                                        {
                                            AboutGameC.CellClickT = CellClickTypes.SimpleClick;
                                            IndexesCellsC.PreviousSelected = IndexesCellsC.Selected;
                                            IndexesCellsC.Selected = IndexesCellsC.Current;
                                        }
                                    }
                                    break;

                                case CellClickTypes.UniqueAbility:
                                    {
                                        switch (AboutGameC.AbilityT)
                                        {
                                            case AbilityTypes.FireArcher:
                                                _rpcC.Action0(_rpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(_s.UnitSs.UnitAbilitiesSs.TryFireForestWithArcherM), IndexesCellsC.Selected, IndexesCellsC.Current });
                                                break;

                                            case AbilityTypes.StunElfemale:
                                                _rpcC.Action0(_rpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(_s.UnitSs.UnitAbilitiesSs.TryStunEnemyWithElfemaleM), IndexesCellsC.Selected, IndexesCellsC.Current });
                                                break;

                                            case AbilityTypes.ChangeDirectionWind:
                                                {
                                                    if (!CloudC(IndexesCellsC.Current).IsCenter)
                                                    {
                                                        _rpcC.Action0(_rpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(_s.UnitSs.UnitAbilitiesSs.TryChangeDirectWindWithSnowyM), IndexesCellsC.Selected, IndexesCellsC.Current });
                                                    }
                                                }
                                                break;

                                            default: throw new Exception();
                                        }

                                        AboutGameC.CellClickT = CellClickTypes.SimpleClick;
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
                            if (!AboutGameC.LessonT.HaveLesson()/* Is(LessonTypes.RelaxExtractPawn, LessonTypes.SeedingPawn)*/)
                            {
                                AboutGameC.CellClickT = CellClickTypes.SimpleClick;

                                IndexesCellsC.PreviousSelected = IndexesCellsC.Selected;
                                IndexesCellsC.Selected = 0;

                                AboutGameC.IsSelectedCity = false;

                                //eMG.LessonTC.SetPreviousLesson();
                            }
                        }
                        break;

                    default: throw new Exception();
                }
            }

            else
            {
                if (AboutGameC.RaycastT == RaycastTypes.Cell)
                {

                }
            }
        }
    }
}