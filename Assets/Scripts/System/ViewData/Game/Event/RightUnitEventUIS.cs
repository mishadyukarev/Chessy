using System;
using static Game.Game.CellUnitEs;
using static Game.Game.EntityPool;
using static Game.Game.EntityVPool;

namespace Game.Game
{
    sealed class RightUnitEventUIS
    {
        internal RightUnitEventUIS()
        {
            UIEntRightUnique.Buttons<ButtonUIC>(ButtonTypes.First).AddListener(delegate { UniqBut(ButtonTypes.First); });
            UIEntRightUnique.Buttons<ButtonUIC>(ButtonTypes.Second).AddListener(delegate { UniqBut(ButtonTypes.Second); });
            UIEntRightUnique.Buttons<ButtonUIC>(ButtonTypes.Third).AddListener(delegate { UniqBut(ButtonTypes.Third); });

            UIEntBuild.Button<ButtonUIC>(ButtonTypes.First).AddListener(delegate { ExecuteBuild_Button(ButtonTypes.First); });
            UIEntBuild.Button<ButtonUIC>(ButtonTypes.Second).AddListener(delegate { ExecuteBuild_Button(ButtonTypes.Second); });
            UIEntBuild.Button<ButtonUIC>(ButtonTypes.Third).AddListener(delegate { ExecuteBuild_Button(ButtonTypes.Third); });

            RightProtectUIE.Button<ButtonUIC>().AddListener(delegate { ConditionAbilityButton(ConditionUnitTypes.Protected); });
            RightRelaxUIE.Button<ButtonUIC>().AddListener(delegate { ConditionAbilityButton(ConditionUnitTypes.Relaxed); });
        }

        void ConditionAbilityButton(ConditionUnitTypes condUnitType)
        {
            if (WhoseMoveE.IsMyMove)
            {
                TryOnHint(VideoClipTypes.ProtRelax);

                if (CellUnitElseEs.Condition(SelectedIdxE.IdxC.Idx).Is(condUnitType))
                {
                    EntityPool.Rpc.ConditionUnitToMaster(ConditionUnitTypes.None, SelectedIdxE.IdxC.Idx);
                }
                else
                {
                    EntityPool.Rpc.ConditionUnitToMaster(condUnitType, SelectedIdxE.IdxC.Idx);
                }
            }
            else SoundV<AudioSourceVC>(ClipTypes.Mistake).Play();
        }

        void UniqBut(ButtonTypes uniqBut)
        {
            if (WhoseMoveE.IsMyMove)
            {
                ref var abil = ref CellUnitUniqueButtonsEs.Ability(uniqBut, SelectedIdxE.IdxC.Idx);

                if (!CellUnitAbilityUniqueEs.Cooldown<CooldownC>(abil.Ability, SelectedIdxE.IdxC.Idx).HaveCooldown)
                {
                    switch (abil.Ability)
                    {
                        case UniqueAbilityTypes.FirePawn:
                            EntityPool.Rpc.FirePawnToMas(SelectedIdxE.IdxC.Idx);
                            TryOnHint(VideoClipTypes.SeedFire);
                            break;

                        case UniqueAbilityTypes.PutOutFirePawn:
                            EntityPool.Rpc.PutOutFirePawnToMas(SelectedIdxE.IdxC.Idx);
                            TryOnHint(VideoClipTypes.SeedFire);
                            break;

                        case UniqueAbilityTypes.Seed:
                            EntityPool.Rpc.SeedEnvToMaster(SelectedIdxE.IdxC.Idx, EnvironmentTypes.YoungForest);
                            TryOnHint(VideoClipTypes.SeedFire);
                            break;

                        case UniqueAbilityTypes.FireArcher:
                            ClickerObject<CellClickC>().Click = CellClickTypes.UniqueAbility;
                            SelUniqAbilC.UniqAbil = UniqueAbilityTypes.FireArcher;
                            TryOnHint(VideoClipTypes.SeedFire);
                            break;

                        case UniqueAbilityTypes.CircularAttack:
                            EntityPool.Rpc.CircularAttackKingToMaster(SelectedIdxE.IdxC.Idx);
                            TryOnHint(VideoClipTypes.CircularAttack);
                            break;

                        case UniqueAbilityTypes.StunElfemale:
                            {
                                ClickerObject<CellClickC>().Click = CellClickTypes.UniqueAbility;
                                SelUniqAbilC.UniqAbil = UniqueAbilityTypes.StunElfemale;
                                TryOnHint(VideoClipTypes.StunElfemale);
                            }
                            break;

                        case UniqueAbilityTypes.BonusNear:
                            EntityPool.Rpc.BonusNearUnits(SelectedIdxE.IdxC.Idx);
                            TryOnHint(VideoClipTypes.BonusKing);
                            break;

                        case UniqueAbilityTypes.ChangeCornerArcher:
                            {
                                EntityPool.Rpc.ChangeCornerArchToMas(SelectedIdxE.IdxC.Idx);
                            }
                            break;

                        case UniqueAbilityTypes.GrowAdultForest:
                            EntityPool.Rpc.GrowAdultForest(SelectedIdxE.IdxC.Idx);
                            TryOnHint(VideoClipTypes.GrowingAdForesElfemale);
                            break;

                        case UniqueAbilityTypes.ChangeDirectionWind:
                            {
                                TryOnHint(VideoClipTypes.PutOutElfemale);
                                ClickerObject<CellClickC>().Click = CellClickTypes.UniqueAbility;
                                SelUniqAbilC.UniqAbil = UniqueAbilityTypes.ChangeDirectionWind;
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
            var idx_sel = SelectedIdxE.IdxC.Idx;

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
                        var buildAbility = CellUnitBuildingButtonEs.UnitBuildButton<BuildingTC>(ButtonTypes.Third, idx_sel).Build;
                        if (buildAbility == BuildingTypes.None)Rpc.DestroyBuildingToMaster(idx_sel);
                        else Rpc.BuildToMaster(idx_sel, CellUnitBuildingButtonEs.UnitBuildButton<BuildingTC>(ButtonTypes.Third, idx_sel).Build);

                        //switch (BuildAbilC.AbilityType(buildBut))
                        //{
                        //    case BuildAbilityTypes.None: throw new Exception();
                        //    case BuildAbilityTypes.FarmBuild: throw new Exception();
                        //    case BuildAbilityTypes.MineBuild: throw new Exception();
                        //    case BuildAbilityTypes.CityBuild:
                        //        EntityPool.Rpc.BuildToMaster(SelectedIdxE.SelIdx<IdxC>().Idx, BuildTypes.City);
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

