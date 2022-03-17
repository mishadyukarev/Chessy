using Chessy.Common;
using System;

namespace Chessy.Game
{
    public sealed class RightUnitEventsUI : SystemUIAbstract
    {
        internal RightUnitEventsUI(in EntitiesViewUI entsUI, in EntitiesModel ents) : base(entsUI, ents)
        {
            UIE.RightEs.Unique(ButtonTypes.First).ButtonC.AddListener(delegate { Unique(ButtonTypes.First); });
            UIE.RightEs.Unique(ButtonTypes.Second).ButtonC.AddListener(delegate { Unique(ButtonTypes.Second); });
            UIE.RightEs.Unique(ButtonTypes.Third).ButtonC.AddListener(delegate { Unique(ButtonTypes.Third); });
            UIE.RightEs.Unique(ButtonTypes.Fourth).ButtonC.AddListener(delegate { Unique(ButtonTypes.Fourth); });
            UIE.RightEs.Unique(ButtonTypes.Fifth).ButtonC.AddListener(delegate { Unique(ButtonTypes.Fifth); });

            UIE.RightEs.ProtectE.ButtonC.AddListener(delegate { ConditionAbilityButton(ConditionUnitTypes.Protected); });
            UIE.RightEs.RelaxE.ButtonC.AddListener(delegate { ConditionAbilityButton(ConditionUnitTypes.Relaxed); });
        }

        void ConditionAbilityButton(ConditionUnitTypes condUnitType)
        {
            if (E.CurPlayerITC.Is(E.WhoseMove.Player))
            {
                TryOnHint(VideoClipTypes.ProtRelax);

                if (E.UnitConditionTC(E.CellsC.Selected).Is(condUnitType))
                {
                    E.RpcPoolEs.ConditionUnitToMaster(E.CellsC.Selected, ConditionUnitTypes.None);
                }
                else
                {
                    E.RpcPoolEs.ConditionUnitToMaster(E.CellsC.Selected, condUnitType);
                }
            }
            else E.Sound(ClipTypes.Mistake).Action.Invoke();
        }

        void Unique(in ButtonTypes uniqueButton)
        {
            if (E.CurPlayerITC.Is(E.WhoseMove.Player))
            {
                var idx_sel = E.CellsC.Selected;

                var abil = E.UnitEs(idx_sel).Ability(uniqueButton);

                if (!E.UnitEs(idx_sel).CoolDownC(abil.Ability).HaveCooldown)
                {
                    switch (abil.Ability)
                    {
                        case AbilityTypes.FirePawn:
                            E.RpcPoolEs.FirePawnToMas(idx_sel);
                            TryOnHint(VideoClipTypes.SeedFire);
                            break;

                        case AbilityTypes.PutOutFirePawn:
                            E.RpcPoolEs.PutOutFirePawnToMas(idx_sel);
                            TryOnHint(VideoClipTypes.SeedFire);
                            break;

                        case AbilityTypes.Seed:
                            E.RpcPoolEs.SeedEnvToMaster(idx_sel, EnvironmentTypes.YoungForest);
                            TryOnHint(VideoClipTypes.SeedFire);
                            break;

                        case AbilityTypes.FireArcher:
                            E.SelectedE.AbilityTC.Set(AbilityTypes.FireArcher);
                            E.CellClickTC.Click = CellClickTypes.UniqueAbility;
                            TryOnHint(VideoClipTypes.SeedFire);
                            break;

                        case AbilityTypes.CircularAttack:
                            E.RpcPoolEs.CircularAttackKingToMaster(idx_sel);
                            TryOnHint(VideoClipTypes.CircularAttack);
                            break;

                        case AbilityTypes.StunElfemale:
                            {
                                E.SelectedE.AbilityTC.Ability = AbilityTypes.StunElfemale;
                                E.CellClickTC.Click = CellClickTypes.UniqueAbility;
                                TryOnHint(VideoClipTypes.StunElfemale);
                            }
                            break;

                        case AbilityTypes.KingPassiveNearBonus:
                            //E.RpcPoolEs.BonusNearUnits(idx_sel);
                            //TryOnHint(VideoClipTypes.BonusKing);
                            break;


                        //Snowy

                        case AbilityTypes.IncreaseWindSnowy:
                            E.RpcPoolEs.IncreaseWindSnowy_ToMaster(idx_sel);
                            break;

                        case AbilityTypes.DecreaseWindSnowy:
                            E.RpcPoolEs.DecreaseWindSnowy_ToMaster(idx_sel);
                            break;

                        case AbilityTypes.ChangeCornerArcher:
                            E.RpcPoolEs.ChangeCornerArchToMas(idx_sel);
                            break;

                        case AbilityTypes.GrowAdultForest:
                            E.RpcPoolEs.GrowAdultForest(idx_sel);
                            TryOnHint(VideoClipTypes.GrowingAdForesElfemale);
                            break;

                        case AbilityTypes.ChangeDirectionWind:
                            {
                                TryOnHint(VideoClipTypes.PutOutElfemale);
                                E.SelectedE.AbilityTC.Ability = AbilityTypes.ChangeDirectionWind;
                                E.CellClickTC.Click = CellClickTypes.UniqueAbility;
                            }
                            break;

                        case AbilityTypes.SetFarm:
                            {
                                E.RpcPoolEs.BuildFarmToMaster(idx_sel);
                                TryOnHint(VideoClipTypes.BuldFarms);
                            }
                            break;

                        case AbilityTypes.DestroyBuilding:
                            E.RpcPoolEs.DestroyBuildingToMaster(idx_sel);
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
                            E.SelectedE.AbilityTC.Ability = AbilityTypes.Resurrect;
                            E.CellClickTC.Click = CellClickTypes.UniqueAbility;
                            break;

                        case AbilityTypes.SetTeleport:
                            E.RpcPoolEs.SetTeleportToMaster(idx_sel);
                            break;

                        case AbilityTypes.Teleport:
                            E.RpcPoolEs.TeleportToMaster(idx_sel);
                            break;

                        case AbilityTypes.InvokeSkeletons:
                            E.RpcPoolEs.InvokeSkeletonsToMaster(idx_sel);
                            break;

                        default: throw new Exception();
                    }
                }

                else E.Sound(ClipTypes.Mistake).Action.Invoke();
            }
            else E.Sound(ClipTypes.Mistake).Action.Invoke();
        }

        void TryOnHint(VideoClipTypes videoClip)
        {
            if (Common.HintC.IsOnHint)
            {
                //if (!HintC.WasActived(videoClip))
                //{
                //    //EntityCenterHintUIPool.SetActiveHintZone(true);
                //    //EntityCenterHintUIPool.SetVideoClip(videoClip);
                //    HintC.SetWasActived(videoClip, true);
                //}
            }
        }
    }
}

