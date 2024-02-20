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
            var idx_cur = indexesCellsC.Current;


            if (Input.GetKeyDown(KeyCode.F1))
            {
                aboutGameC.IsActivatedIdxAndXyInfoCells = !aboutGameC.IsActivatedIdxAndXyInfoCells;
                updateAllViewC.NeedUpdateView = true;
            }

            if (Input.GetKeyDown(KeyCode.F2))
            {
                environmentCs[idx_cur].Dispose();
                updateAllViewC.NeedUpdateView = true;
            }


            if (inputC.IsClicked)
            {
                updateAllViewC.NeedUpdateView = true;
                mistakeC.MistakeT = MistakeTypes.None;




                if (aboutGameC.LessonT == LessonTypes.UniqueAttackInfo)
                {
                    sunC.SunSideT = SunSideTypes.Dawn;
                    s.SetNextLesson();
                }
                else if (aboutGameC.LessonT.Is(LessonTypes.YouNeedDestroyKing, LessonTypes.ThatIsYourSpawn, LessonTypes.ClickBuyMarketInTown, LessonTypes.LookInfoAboutSun,
                     LessonTypes.MenuInfo))
                {
                    s.SetNextLesson();
                }

                switch (aboutGameC.RaycastT)
                {
                    case RaycastTypes.Cell:
                        {
                            switch (aboutGameC.CellClickT)
                            {
                                case CellClickTypes.SimpleClick:
                                    _cellSimpleClickS.Execute();
                                    break;

                                case CellClickTypes.SetUnit:
                                    {
                                        rpcC.Action0(rpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(s.TrySetUnitOnCellM), indexesCellsC.Current, selectedUnitC.UnitT });
                                        aboutGameC.CellClickT = CellClickTypes.SimpleClick;
                                    }
                                    break;

                                case CellClickTypes.GiveTakeTW:
                                    {
                                        indexesCellsC.Selected = indexesCellsC.Current;

                                        if (unitCs[idx_cur].UnitT == UnitTypes.Pawn && unitCs[idx_cur].PlayerT == aboutGameC.CurrentPlayerIT)
                                        {
                                            rpcC.Action0(rpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(s.TryGiveTakeToolOrWeaponToUnitOnCellM), indexesCellsC.Current, selectedToolWeaponC.ToolWeaponT, selectedToolWeaponC.LevelT });
                                        }
                                        else
                                        {
                                            aboutGameC.CellClickT = CellClickTypes.SimpleClick;
                                            indexesCellsC.PreviousSelected = indexesCellsC.Selected;
                                            indexesCellsC.Selected = indexesCellsC.Current;
                                        }
                                    }
                                    break;

                                case CellClickTypes.UniqueAbility:
                                    {
                                        switch (aboutGameC.AbilityT)
                                        {
                                            case AbilityTypes.FireArcher:
                                                rpcC.Action0(rpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(s.unitSs.unitAbilitiesSs.TryFireForestWithArcherM), indexesCellsC.Selected, indexesCellsC.Current });
                                                break;

                                            case AbilityTypes.StunElfemale:
                                                rpcC.Action0(rpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(s.unitSs.unitAbilitiesSs.stunElfemaleS_M.TryStunEnemyWithElfemaleM), indexesCellsC.Selected, indexesCellsC.Current });
                                                break;

                                            case AbilityTypes.ChangeDirectionWind:
                                                {
                                                    if (!cloudCs[indexesCellsC.Current].IsCenter)
                                                    {
                                                        rpcC.Action0(rpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(s.unitSs.unitAbilitiesSs.TryChangeDirectWindWithSnowyM), indexesCellsC.Selected, indexesCellsC.Current });
                                                    }
                                                }
                                                break;

                                            default: throw new Exception();
                                        }

                                        aboutGameC.CellClickT = CellClickTypes.SimpleClick;
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
                            if (!aboutGameC.LessonT.HaveLesson()/* Is(LessonTypes.RelaxExtractPawn, LessonTypes.SeedingPawn)*/)
                            {
                                aboutGameC.CellClickT = CellClickTypes.SimpleClick;

                                indexesCellsC.PreviousSelected = indexesCellsC.Selected;
                                indexesCellsC.Selected = 0;

                                aboutGameC.IsSelectedCity = false;

                                //eMG.LessonTC.SetPreviousLesson();
                            }
                        }
                        break;

                    default: throw new Exception();
                }
            }

            else
            {
                if (aboutGameC.RaycastT == RaycastTypes.Cell)
                {

                }
            }
        }
    }
}