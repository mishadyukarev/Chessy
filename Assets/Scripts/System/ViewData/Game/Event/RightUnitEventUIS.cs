using System;
using static Game.Game.CellUnitEntities;
using static Game.Game.EntityPool;
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

            RightUIEntities.Building(ButtonTypes.First).Button.AddListener(delegate { ExecuteBuild_Button(ButtonTypes.First); });
            RightUIEntities.Building(ButtonTypes.Second).Button.AddListener(delegate { ExecuteBuild_Button(ButtonTypes.Second); });
            RightUIEntities.Building(ButtonTypes.Third).Button.AddListener(delegate { ExecuteBuild_Button(ButtonTypes.Third); });

            RightProtectUIE.Button<ButtonUIC>().AddListener(delegate { ConditionAbilityButton(ConditionUnitTypes.Protected); });
            RightRelaxUIE.Button<ButtonUIC>().AddListener(delegate { ConditionAbilityButton(ConditionUnitTypes.Relaxed); });
        }

        void ConditionAbilityButton(ConditionUnitTypes condUnitType)
        {
            if (WhoseMoveE.IsMyMove)
            {
                TryOnHint(VideoClipTypes.ProtRelax);

                if (CellUnitEntities.Else(EntitiesPool.SelectedIdxE.IdxC.Idx).ConditionC.Is(condUnitType))
                {
                    EntityPool.Rpc.ConditionUnitToMaster(ConditionUnitTypes.None, EntitiesPool.SelectedIdxE.IdxC.Idx);
                }
                else
                {
                    EntityPool.Rpc.ConditionUnitToMaster(condUnitType, EntitiesPool.SelectedIdxE.IdxC.Idx);
                }
            }
            else SoundV<AudioSourceVC>(ClipTypes.Mistake).Play();
        }

        void Unique(ButtonTypes uniqueButton)
        {
            if (WhoseMoveE.IsMyMove)
            {
                ref var abil = ref CellUnitUniqueButtonsEs.Ability(uniqueButton, EntitiesPool.SelectedIdxE.IdxC.Idx);

                if (!CellUnitEntities.CooldownUnique(abil.Ability, EntitiesPool.SelectedIdxE.IdxC.Idx).Cooldown.Have)
                {
                    switch (abil.Ability)
                    {
                        case UniqueAbilityTypes.FirePawn:
                            EntityPool.Rpc.FirePawnToMas(EntitiesPool.SelectedIdxE.IdxC.Idx);
                            TryOnHint(VideoClipTypes.SeedFire);
                            break;

                        case UniqueAbilityTypes.PutOutFirePawn:
                            EntityPool.Rpc.PutOutFirePawnToMas(EntitiesPool.SelectedIdxE.IdxC.Idx);
                            TryOnHint(VideoClipTypes.SeedFire);
                            break;

                        case UniqueAbilityTypes.Seed:
                            EntityPool.Rpc.SeedEnvToMaster(EntitiesPool.SelectedIdxE.IdxC.Idx, EnvironmentTypes.YoungForest);
                            TryOnHint(VideoClipTypes.SeedFire);
                            break;

                        case UniqueAbilityTypes.FireArcher:
                            ClickerObject<CellClickC>().Click = CellClickTypes.UniqueAbility;
                            SelectedUniqueAbilityC.AbilityC.Ability = UniqueAbilityTypes.FireArcher;
                            TryOnHint(VideoClipTypes.SeedFire);
                            break;

                        case UniqueAbilityTypes.CircularAttack:
                            EntityPool.Rpc.CircularAttackKingToMaster(EntitiesPool.SelectedIdxE.IdxC.Idx);
                            TryOnHint(VideoClipTypes.CircularAttack);
                            break;

                        case UniqueAbilityTypes.StunElfemale:
                            {
                                ClickerObject<CellClickC>().Click = CellClickTypes.UniqueAbility;
                                SelectedUniqueAbilityC.AbilityC.Ability = UniqueAbilityTypes.StunElfemale;
                                TryOnHint(VideoClipTypes.StunElfemale);
                            }
                            break;

                        case UniqueAbilityTypes.BonusNear:
                            EntityPool.Rpc.BonusNearUnits(EntitiesPool.SelectedIdxE.IdxC.Idx);
                            TryOnHint(VideoClipTypes.BonusKing);
                            break;

                        case UniqueAbilityTypes.ChangeCornerArcher:
                            {
                                EntityPool.Rpc.ChangeCornerArchToMas(EntitiesPool.SelectedIdxE.IdxC.Idx);
                            }
                            break;

                        case UniqueAbilityTypes.GrowAdultForest:
                            EntityPool.Rpc.GrowAdultForest(EntitiesPool.SelectedIdxE.IdxC.Idx);
                            TryOnHint(VideoClipTypes.GrowingAdForesElfemale);
                            break;

                        case UniqueAbilityTypes.ChangeDirectionWind:
                            {
                                TryOnHint(VideoClipTypes.PutOutElfemale);
                                ClickerObject<CellClickC>().Click = CellClickTypes.UniqueAbility;
                                SelectedUniqueAbilityC.AbilityC.Ability = UniqueAbilityTypes.ChangeDirectionWind;
                            }
                            break;

                        case UniqueAbilityTypes.FreezeDirectEnemy:
                            {
                                ClickerObject<CellClickC>().Click = CellClickTypes.UniqueAbility;
                                SelectedUniqueAbilityC.AbilityC.Ability = UniqueAbilityTypes.FreezeDirectEnemy;
                            }
                            break;

                        case UniqueAbilityTypes.IceWall:
                            {
                                EntityPool.Rpc.IceWallToMaster(EntitiesPool.SelectedIdxE.IdxC.Idx);
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
            var idx_sel = EntitiesPool.SelectedIdxE.IdxC.Idx;

            if (WhoseMoveE.IsMyMove)
            {
                switch (buildBut)
                {
                    case ButtonTypes.None:
                        throw new Exception();

                    case ButtonTypes.First:
                        EntityPool.Rpc.BuildToMaster(idx_sel, BuildingTypes.Farm);
                        TryOnHint(VideoClipTypes.BuldFarms);
                        break;

                    case ButtonTypes.Second:
                        EntityPool.Rpc.BuildToMaster(idx_sel, BuildingTypes.Mine);
                        TryOnHint(VideoClipTypes.BuildMine);
                        break;

                    case ButtonTypes.Third:
                        var buildAbility = CellUnitEntities.BuildingButton(ButtonTypes.Third, idx_sel).BuildingTC.Build;
                        if (buildAbility == BuildingTypes.None)Rpc.DestroyBuildingToMaster(idx_sel);
                        else Rpc.BuildToMaster(idx_sel, CellUnitEntities.BuildingButton(ButtonTypes.Third, idx_sel).BuildingTC.Build);

                        //switch (BuildAbilC.AbilityType(buildBut))
                        //{
                        //    case BuildAbilityTypes.None: throw new Exception();
                        //    case BuildAbilityTypes.FarmBuild: throw new Exception();
                        //    case BuildAbilityTypes.MineBuild: throw new Exception();
                        //    case BuildAbilityTypes.CityBuild:
                        //        EntityPool.Rpc.BuildToMaster(EntitiesPool.SelectedIdxE.SelIdx<IdxC>().Idx, BuildTypes.City);
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

