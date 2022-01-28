using System;
using static Game.Game.EntityVPool;

namespace Game.Game
{
    sealed class RightUnitEventUIS
    {
        internal RightUnitEventUIS()
        {
            RightUIEntities.Unique(ButtonTypes.First).Button.AddListener(delegate { Unique(ButtonTypes.First); });
            RightUIEntities.Unique(ButtonTypes.Second).Button.AddListener(delegate { Unique(ButtonTypes.Second); });
            RightUIEntities.Unique(ButtonTypes.Third).Button.AddListener(delegate { Unique(ButtonTypes.Third); });
            RightUIEntities.Unique(ButtonTypes.Fourth).Button.AddListener(delegate { Unique(ButtonTypes.Fourth); });

            //RightUIEntities.Building(ButtonTypes.First).Button.AddListener(delegate { ExecuteBuild_Button(ButtonTypes.First); });
            //RightUIEntities.Building(ButtonTypes.Second).Button.AddListener(delegate { ExecuteBuild_Button(ButtonTypes.Second); });
            //RightUIEntities.Building(ButtonTypes.Third).Button.AddListener(delegate { ExecuteBuild_Button(ButtonTypes.Third); });

            RightProtectUIE.Button<ButtonUIC>().AddListener(delegate { ConditionAbilityButton(ConditionUnitTypes.Protected); });
            RightRelaxUIE.Button<ButtonUIC>().AddListener(delegate { ConditionAbilityButton(ConditionUnitTypes.Relaxed); });
        }

        void ConditionAbilityButton(ConditionUnitTypes condUnitType)
        {
            if (Entities.WhoseMove.IsMyMove)
            {
                TryOnHint(VideoClipTypes.ProtRelax);

                if (CellUnitEs.Else(Entities.SelectedIdxE.IdxC.Idx).ConditionC.Is(condUnitType))
                {
                    Entities.Rpc.ConditionUnitToMaster(ConditionUnitTypes.None, Entities.SelectedIdxE.IdxC.Idx);
                }
                else
                {
                    Entities.Rpc.ConditionUnitToMaster(condUnitType, Entities.SelectedIdxE.IdxC.Idx);
                }
            }
            else SoundV<AudioSourceVC>(ClipTypes.Mistake).Play();
        }

        void Unique(ButtonTypes uniqueButton)
        {
            if (Entities.WhoseMove.IsMyMove)
            {
                var idx_sel = Entities.SelectedIdxE.IdxC.Idx;

                ref var abil = ref CellUnitEs.UniqueButton(uniqueButton, idx_sel).AbilityC;

                if (!CellUnitEs.CooldownUnique(abil.Ability, idx_sel).Cooldown.Have)
                {
                    switch (abil.Ability)
                    {
                        case AbilityTypes.FirePawn:
                            Entities.Rpc.FirePawnToMas(idx_sel);
                            TryOnHint(VideoClipTypes.SeedFire);
                            break;

                        case AbilityTypes.PutOutFirePawn:
                            Entities.Rpc.PutOutFirePawnToMas(idx_sel);
                            TryOnHint(VideoClipTypes.SeedFire);
                            break;

                        case AbilityTypes.Seed:
                            Entities.Rpc.SeedEnvToMaster(idx_sel, EnvironmentTypes.YoungForest);
                            TryOnHint(VideoClipTypes.SeedFire);
                            break;

                        case AbilityTypes.FireArcher:
                            Entities.ClickerObject.CellClickC.Click = CellClickTypes.UniqueAbility;
                            Entities.SelectedUniqueAbilityE.AbilityC.Ability = AbilityTypes.FireArcher;
                            TryOnHint(VideoClipTypes.SeedFire);
                            break;

                        case AbilityTypes.CircularAttack:
                            Entities.Rpc.CircularAttackKingToMaster(idx_sel);
                            TryOnHint(VideoClipTypes.CircularAttack);
                            break;

                        case AbilityTypes.StunElfemale:
                            {
                                Entities.ClickerObject.CellClickC.Click = CellClickTypes.UniqueAbility;
                                Entities.SelectedUniqueAbilityE.AbilityC.Ability = AbilityTypes.StunElfemale;
                                TryOnHint(VideoClipTypes.StunElfemale);
                            }
                            break;

                        case AbilityTypes.BonusNear:
                            Entities.Rpc.BonusNearUnits(idx_sel);
                            TryOnHint(VideoClipTypes.BonusKing);
                            break;

                        case AbilityTypes.ChangeCornerArcher:
                            {
                                Entities.Rpc.ChangeCornerArchToMas(idx_sel);
                            }
                            break;

                        case AbilityTypes.GrowAdultForest:
                            Entities.Rpc.GrowAdultForest(idx_sel);
                            TryOnHint(VideoClipTypes.GrowingAdForesElfemale);
                            break;

                        case AbilityTypes.ChangeDirectionWind:
                            {
                                TryOnHint(VideoClipTypes.PutOutElfemale);
                                Entities.ClickerObject.CellClickC.Click = CellClickTypes.UniqueAbility;
                                Entities.SelectedUniqueAbilityE.AbilityC.Ability = AbilityTypes.ChangeDirectionWind;
                            }
                            break;

                        case AbilityTypes.FreezeDirectEnemy:
                            {
                                Entities.ClickerObject.CellClickC.Click = CellClickTypes.UniqueAbility;
                                Entities.SelectedUniqueAbilityE.AbilityC.Ability = AbilityTypes.FreezeDirectEnemy;
                            }
                            break;

                        case AbilityTypes.IceWall:
                            {
                                Entities.Rpc.IceWallToMaster(idx_sel);
                            }
                            break;

                        case AbilityTypes.Farm:
                            {
                                Entities.Rpc.BuildToMaster(idx_sel, BuildingTypes.Farm);
                                TryOnHint(VideoClipTypes.BuldFarms);
                            }
                            break;

                        case AbilityTypes.Mine:
                            {
                                Entities.Rpc.BuildToMaster(idx_sel, BuildingTypes.Mine);
                                TryOnHint(VideoClipTypes.BuildMine);
                            }
                            break;

                        case AbilityTypes.City:
                            {
                                Entities.Rpc.BuildToMaster(idx_sel, BuildingTypes.City);
                            }
                            break;

                        case AbilityTypes.DestroyBuilding:
                            {
                                Entities.Rpc.DestroyBuildingToMaster(idx_sel);
                            }
                            break;

                        default: throw new Exception();
                    }
                }

                else SoundV<AudioSourceVC>(ClipTypes.Mistake).Play();
            }
            else SoundV<AudioSourceVC>(ClipTypes.Mistake).Play();
        }

        void ExecuteBuild_Button(ButtonTypes buildBut)
        {
            var idx_sel = Entities.SelectedIdxE.IdxC.Idx;

            if (Entities.WhoseMove.IsMyMove)
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
                        //        Entities.Rpc.BuildToMaster(EntitiesPool.SelectedIdxE.SelIdx<IdxC>().Idx, BuildTypes.City);
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
            else SoundV<AudioSourceVC>(ClipTypes.Mistake).Play();
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

