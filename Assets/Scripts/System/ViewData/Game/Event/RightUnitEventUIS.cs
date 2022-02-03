using System;
using static Game.Game.EntityVPool;

namespace Game.Game
{
    sealed class RightUnitEventUIS : SystemViewAbstract
    {
        public RightUnitEventUIS(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
            VEs.UIEs.RightEs.Unique(ButtonTypes.First).Button.AddListener(delegate { Unique(ButtonTypes.First); });
            VEs.UIEs.RightEs.Unique(ButtonTypes.Second).Button.AddListener(delegate { Unique(ButtonTypes.Second); });
            VEs.UIEs.RightEs.Unique(ButtonTypes.Third).Button.AddListener(delegate { Unique(ButtonTypes.Third); });
            VEs.UIEs.RightEs.Unique(ButtonTypes.Fourth).Button.AddListener(delegate { Unique(ButtonTypes.Fourth); });

            //RightUIEntities.Building(ButtonTypes.First).Button.AddListener(delegate { ExecuteBuild_Button(ButtonTypes.First); });
            //RightUIEntities.Building(ButtonTypes.Second).Button.AddListener(delegate { ExecuteBuild_Button(ButtonTypes.Second); });
            //RightUIEntities.Building(ButtonTypes.Third).Button.AddListener(delegate { ExecuteBuild_Button(ButtonTypes.Third); });

            RightProtectUIE.Button<ButtonUIC>().AddListener(delegate { ConditionAbilityButton(ConditionUnitTypes.Protected); });
            RightRelaxUIE.Button<ButtonUIC>().AddListener(delegate { ConditionAbilityButton(ConditionUnitTypes.Relaxed); });
        }

        void ConditionAbilityButton(ConditionUnitTypes condUnitType)
        {
            if (Es.WhoseMove.IsMyMove)
            {
                TryOnHint(VideoClipTypes.ProtRelax);

                if (UnitEs(Es.SelectedIdxE.IdxC.Idx).MainE.ConditionTC.Is(condUnitType))
                {
                    Es.Rpc.ConditionUnitToMaster(ConditionUnitTypes.None, Es.SelectedIdxE.IdxC.Idx);
                }
                else
                {
                    Es.Rpc.ConditionUnitToMaster(condUnitType, Es.SelectedIdxE.IdxC.Idx);
                }
            }
            else SoundV(ClipTypes.Mistake).Play();
        }

        void Unique(ButtonTypes uniqueButton)
        {
            if (Es.WhoseMove.IsMyMove)
            {
                var idx_sel = Es.SelectedIdxE.IdxC.Idx;

                var abil = UnitEs(idx_sel).AbilityButton(uniqueButton).AbilityC;

                if (!UnitEs(idx_sel).CooldownAbility(abil.Ability).HaveCooldown)
                {
                    switch (abil.Ability)
                    {
                        case AbilityTypes.FirePawn:
                            Es.Rpc.FirePawnToMas(idx_sel);
                            TryOnHint(VideoClipTypes.SeedFire);
                            break;

                        case AbilityTypes.PutOutFirePawn:
                            Es.Rpc.PutOutFirePawnToMas(idx_sel);
                            TryOnHint(VideoClipTypes.SeedFire);
                            break;

                        case AbilityTypes.Seed:
                            Es.Rpc.SeedEnvToMaster(idx_sel, EnvironmentTypes.YoungForest);
                            TryOnHint(VideoClipTypes.SeedFire);
                            break;

                        case AbilityTypes.FireArcher:
                            Es.ClickerObject.CellClickC.Click = CellClickTypes.UniqueAbility;
                            Es.SelectedUniqueAbilityE.AbilityC.Ability = AbilityTypes.FireArcher;
                            TryOnHint(VideoClipTypes.SeedFire);
                            break;

                        case AbilityTypes.CircularAttack:
                            Es.Rpc.CircularAttackKingToMaster(idx_sel);
                            TryOnHint(VideoClipTypes.CircularAttack);
                            break;

                        case AbilityTypes.StunElfemale:
                            {
                                Es.ClickerObject.CellClickC.Click = CellClickTypes.UniqueAbility;
                                Es.SelectedUniqueAbilityE.AbilityC.Ability = AbilityTypes.StunElfemale;
                                TryOnHint(VideoClipTypes.StunElfemale);
                            }
                            break;

                        case AbilityTypes.BonusNear:
                            Es.Rpc.BonusNearUnits(idx_sel);
                            TryOnHint(VideoClipTypes.BonusKing);
                            break;

                        case AbilityTypes.ChangeCornerArcher:
                            {
                                Es.Rpc.ChangeCornerArchToMas(idx_sel);
                            }
                            break;

                        case AbilityTypes.GrowAdultForest:
                            Es.Rpc.GrowAdultForest(idx_sel);
                            TryOnHint(VideoClipTypes.GrowingAdForesElfemale);
                            break;

                        case AbilityTypes.ChangeDirectionWind:
                            {
                                TryOnHint(VideoClipTypes.PutOutElfemale);
                                Es.ClickerObject.CellClickC.Click = CellClickTypes.UniqueAbility;
                                Es.SelectedUniqueAbilityE.AbilityC.Ability = AbilityTypes.ChangeDirectionWind;
                            }
                            break;

                        case AbilityTypes.Farm:
                            {
                                Es.Rpc.BuildFarmToMaster(idx_sel);
                                TryOnHint(VideoClipTypes.BuldFarms);
                            }
                            break;

                        case AbilityTypes.Mine:
                            {
                                Es.Rpc.BuildMineToMaster(idx_sel);
                                TryOnHint(VideoClipTypes.BuildMine);
                            }
                            break;

                        case AbilityTypes.City:
                            {
                                Es.Rpc.BuildCityToMaster(idx_sel);
                            }
                            break;

                        case AbilityTypes.DestroyBuilding:
                            {
                                Es.Rpc.DestroyBuildingToMaster(idx_sel);
                            }
                            break;

                        case AbilityTypes.IceWall:
                                Es.Rpc.IceWallToMaster(idx_sel);
                            break;

                        case AbilityTypes.ActiveAroundBonusSnowy:
                            Es.Rpc.ActiveSnowyAroundToMaster(idx_sel);
                            break;

                        case AbilityTypes.DirectWave:
                            Es.ClickerObject.CellClickC.Click = CellClickTypes.UniqueAbility;
                            Es.SelectedUniqueAbilityE.AbilityC.Ability = AbilityTypes.DirectWave;
                            break;

                        default: throw new Exception();
                    }
                }

                else SoundV(ClipTypes.Mistake).Play();
            }
            else SoundV(ClipTypes.Mistake).Play();
        }

        void ExecuteBuild_Button(ButtonTypes buildBut)
        {
            var idx_sel = Es.SelectedIdxE.IdxC.Idx;

            if (Es.WhoseMove.IsMyMove)
            {
                switch (buildBut)
                {
                    case ButtonTypes.None:
                        throw new Exception();

                    case ButtonTypes.First:

                        break;

                    case ButtonTypes.Second:

                        break;

                    case ButtonTypes.Third:

                        //switch (BuildAbilC.AbilityType(buildBut))
                        //{
                        //    case BuildAbilityTypes.None: throw new Exception();
                        //    case BuildAbilityTypes.FarmBuild: throw new Exception();
                        //    case BuildAbilityTypes.MineBuild: throw new Exception();
                        //    case BuildAbilityTypes.CityBuild:
                        //        Ents.Rpc.BuildToMaster(EntitiesPool.SelectedIdxE.SelIdx<IdxC>().Idx, BuildTypes.City);
                        //        break;

                        //    case BuildAbilityTypes.Destroy:
                        //        EntityPool
                        //        break;

                        //    default: throw new Exception();
                        //}
                        break;

                    default: throw new Exception();
                }
            }
            else SoundV(ClipTypes.Mistake).Play();
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

