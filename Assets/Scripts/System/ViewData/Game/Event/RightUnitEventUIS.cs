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

            RightUIEntities.Building(ButtonTypes.First).Button.AddListener(delegate { ExecuteBuild_Button(ButtonTypes.First); });
            RightUIEntities.Building(ButtonTypes.Second).Button.AddListener(delegate { ExecuteBuild_Button(ButtonTypes.Second); });
            RightUIEntities.Building(ButtonTypes.Third).Button.AddListener(delegate { ExecuteBuild_Button(ButtonTypes.Third); });

            RightProtectUIE.Button<ButtonUIC>().AddListener(delegate { ConditionAbilityButton(ConditionUnitTypes.Protected); });
            RightRelaxUIE.Button<ButtonUIC>().AddListener(delegate { ConditionAbilityButton(ConditionUnitTypes.Relaxed); });
        }

        void ConditionAbilityButton(ConditionUnitTypes condUnitType)
        {
            if (Entities.WhoseMoveE.IsMyMove)
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
            if (Entities.WhoseMoveE.IsMyMove)
            {
                ref var abil = ref CellUnitEs.UniqueButton(uniqueButton, Entities.SelectedIdxE.IdxC.Idx).AbilityC;

                if (!CellUnitEs.CooldownUnique(abil.Ability, Entities.SelectedIdxE.IdxC.Idx).Cooldown.Have)
                {
                    switch (abil.Ability)
                    {
                        case UniqueAbilityTypes.FirePawn:
                            Entities.Rpc.FirePawnToMas(Entities.SelectedIdxE.IdxC.Idx);
                            TryOnHint(VideoClipTypes.SeedFire);
                            break;

                        case UniqueAbilityTypes.PutOutFirePawn:
                            Entities.Rpc.PutOutFirePawnToMas(Entities.SelectedIdxE.IdxC.Idx);
                            TryOnHint(VideoClipTypes.SeedFire);
                            break;

                        case UniqueAbilityTypes.Seed:
                            Entities.Rpc.SeedEnvToMaster(Entities.SelectedIdxE.IdxC.Idx, EnvironmentTypes.YoungForest);
                            TryOnHint(VideoClipTypes.SeedFire);
                            break;

                        case UniqueAbilityTypes.FireArcher:
                            Entities.ClickerObject.CellClickC.Click = CellClickTypes.UniqueAbility;
                            Entities.SelectedUniqueAbilityE.AbilityC.Ability = UniqueAbilityTypes.FireArcher;
                            TryOnHint(VideoClipTypes.SeedFire);
                            break;

                        case UniqueAbilityTypes.CircularAttack:
                            Entities.Rpc.CircularAttackKingToMaster(Entities.SelectedIdxE.IdxC.Idx);
                            TryOnHint(VideoClipTypes.CircularAttack);
                            break;

                        case UniqueAbilityTypes.StunElfemale:
                            {
                                Entities.ClickerObject.CellClickC.Click = CellClickTypes.UniqueAbility;
                                Entities.SelectedUniqueAbilityE.AbilityC.Ability = UniqueAbilityTypes.StunElfemale;
                                TryOnHint(VideoClipTypes.StunElfemale);
                            }
                            break;

                        case UniqueAbilityTypes.BonusNear:
                            Entities.Rpc.BonusNearUnits(Entities.SelectedIdxE.IdxC.Idx);
                            TryOnHint(VideoClipTypes.BonusKing);
                            break;

                        case UniqueAbilityTypes.ChangeCornerArcher:
                            {
                                Entities.Rpc.ChangeCornerArchToMas(Entities.SelectedIdxE.IdxC.Idx);
                            }
                            break;

                        case UniqueAbilityTypes.GrowAdultForest:
                            Entities.Rpc.GrowAdultForest(Entities.SelectedIdxE.IdxC.Idx);
                            TryOnHint(VideoClipTypes.GrowingAdForesElfemale);
                            break;

                        case UniqueAbilityTypes.ChangeDirectionWind:
                            {
                                TryOnHint(VideoClipTypes.PutOutElfemale);
                                Entities.ClickerObject.CellClickC.Click = CellClickTypes.UniqueAbility;
                                Entities.SelectedUniqueAbilityE.AbilityC.Ability = UniqueAbilityTypes.ChangeDirectionWind;
                            }
                            break;

                        case UniqueAbilityTypes.FreezeDirectEnemy:
                            {
                                Entities.ClickerObject.CellClickC.Click = CellClickTypes.UniqueAbility;
                                Entities.SelectedUniqueAbilityE.AbilityC.Ability = UniqueAbilityTypes.FreezeDirectEnemy;
                            }
                            break;

                        case UniqueAbilityTypes.IceWall:
                            {
                                Entities.Rpc.IceWallToMaster(Entities.SelectedIdxE.IdxC.Idx);
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

            if (Entities.WhoseMoveE.IsMyMove)
            {
                switch (buildBut)
                {
                    case ButtonTypes.None:
                        throw new Exception();

                    case ButtonTypes.First:
                        Entities.Rpc.BuildToMaster(idx_sel, BuildingTypes.Farm);
                        TryOnHint(VideoClipTypes.BuldFarms);
                        break;

                    case ButtonTypes.Second:
                        Entities.Rpc.BuildToMaster(idx_sel, BuildingTypes.Mine);
                        TryOnHint(VideoClipTypes.BuildMine);
                        break;

                    case ButtonTypes.Third:
                        var buildAbility = CellUnitEs.BuildingButton(ButtonTypes.Third, idx_sel).BuildingTC.Build;
                        if (buildAbility == BuildingTypes.None) Entities.Rpc.DestroyBuildingToMaster(idx_sel);
                        else Entities.Rpc.BuildToMaster(idx_sel, CellUnitEs.BuildingButton(ButtonTypes.Third, idx_sel).BuildingTC.Build);

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

