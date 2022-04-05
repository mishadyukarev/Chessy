using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;
using Chessy.Game.Enum;
using System;

namespace Chessy.Game.Model.System
{
    sealed class SelectorS : SystemModelGameAbs, IEcsRunSystem
    {
        readonly CellSimpleClickS _cellSimpleClickS;

        public SelectorS(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG)
        {
            _cellSimpleClickS = new CellSimpleClickS(sMC, eMC, sMG, eMG);
        }

        public void Run()
        {
            var idx_cur = eMG.CellsC.Current;


            if (eMG.IsClicked)
            {
                eMG.NeedUpdateView = true;
                eMG.MistakeTC.MistakeT = MistakeTypes.None;


                if (eMG.LessonTC.Is(LessonTypes.LookInfoAboutSun))
                {
                    eMG.LessonTC.SetNextLesson();
                }

                switch (eMG.RaycastTC.RaycastT)
                {
                    case RaycastTypes.Cell:
                        {
                            if (eMG.CurPlayerITC.Is(eMG.WhoseMovePlayerTC.PlayerT))
                            {
                                switch (eMG.CellClickTC.CellClickT)
                                {
                                    case CellClickTypes.SimpleClick:
                                        _cellSimpleClickS.Execute();
                                        break;

                                    case CellClickTypes.SetUnit:
                                        {
                                            eMG.RpcPoolEs.TrySetUnit_ToMaster(eMG.CellsC.Current, eMG.SelectedUnitE.UnitTC.UnitT);
                                            eMG.CellClickTC.CellClickT = CellClickTypes.SimpleClick;
                                        }
                                        break;

                                    case CellClickTypes.GiveTakeTW:
                                        {
                                            eMG.CellsC.Selected = eMG.CellsC.Current;

                                            if (eMG.UnitTC(idx_cur).Is(UnitTypes.Pawn) && eMG.UnitPlayerTC(idx_cur).Is(eMG.CurPlayerITC.PlayerT))
                                            {
                                                eMG.RpcPoolEs.GiveTakeToolWeaponToMaster(eMG.CellsC.Current, eMG.SelectedE.ToolWeaponC.ToolWeaponT, eMG.SelectedE.ToolWeaponC.LevelT);
                                            }
                                            else
                                            {
                                                eMG.CellClickTC.CellClickT = CellClickTypes.SimpleClick;
                                                eMG.CellsC.PreviousSelected = eMG.CellsC.Selected;
                                                eMG.CellsC.Selected = eMG.CellsC.Current;
                                            }
                                        }
                                        break;

                                    case CellClickTypes.UniqueAbility:
                                        {
                                            switch (eMG.SelectedE.AbilityTC.Ability)
                                            {
                                                case AbilityTypes.FireArcher:
                                                    eMG.RpcPoolEs.FireArcherToMas(eMG.CellsC.Selected, eMG.CellsC.Current);
                                                    break;

                                                case AbilityTypes.StunElfemale:
                                                    eMG.RpcPoolEs.StunElfemaleToMas(eMG.CellsC.Selected, eMG.CellsC.Current);
                                                    break;

                                                case AbilityTypes.ChangeDirectionWind:
                                                    {
                                                        foreach (var cellE in eMG.AroundCellsE(eMG.WeatherE.CloudC.Center).CellsAround)
                                                        {
                                                            if (cellE == eMG.CellsC.Current)
                                                            {
                                                                eMG.RpcPoolEs.PutOutFireElffToMas(eMG.CellsC.Selected, eMG.CellsC.Current);
                                                            }
                                                        }
                                                    }
                                                    break;

                                                //case AbilityTypes.DirectWave:
                                                //    e.RpcPoolEs.DirectWaveToMaster(e.CellsC.SelectedIdxC, e.CellsC.CurrentIdxC);
                                                //    break;

                                                case AbilityTypes.Resurrect:
                                                    eMG.RpcPoolEs.ResurrectToMaster(eMG.CellsC.Selected, eMG.CellsC.Current);
                                                    break;

                                                default: throw new Exception();
                                            }

                                            eMG.CellClickTC.CellClickT = CellClickTypes.SimpleClick;
                                        }
                                        break;

                                    default: throw new Exception();
                                }
                            }

                            else
                            {
                                eMG.CellsC.Selected = eMG.CellsC.Current;
                            }
                        }
                        break;

                    case RaycastTypes.UI:
                        break;

                    case RaycastTypes.Background:
                        {
                            if (eMG.LessonTC.Is(LessonTypes.RelaxExtractPawn)) eMG.LessonTC.SetPreviousLesson();


                            eMG.CellClickTC.CellClickT = CellClickTypes.SimpleClick;

                            eMG.CellsC.PreviousSelected = eMG.CellsC.Selected;
                            eMG.CellsC.Selected = 0;
                        }
                        break;

                    default: throw new Exception();
                }
            }

            else
            {
                if (eMG.RaycastTC.Is(RaycastTypes.Cell))
                {

                }
            }
        }
    }
}