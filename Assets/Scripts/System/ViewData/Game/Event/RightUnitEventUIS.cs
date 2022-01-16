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

            UIEntRightProtect.Button<ButtonUIC>().AddListener(delegate { ConditionAbilityButton(ConditionUnitTypes.Protected); });
            UIEntRelax.Button<ButtonUIC>().AddListener(delegate { ConditionAbilityButton(ConditionUnitTypes.Relaxed); });
        }

        void ConditionAbilityButton(ConditionUnitTypes condUnitType)
        {
            if (WhoseMoveE.IsMyMove)
            {
                TryOnHint(VideoClipTypes.ProtRelax);

                if (Unit<ConditionUnitC>(SelIdx<IdxC>().Idx).Is(condUnitType))
                {
                    EntityPool.Rpc<RpcC>().ConditionUnitToMaster(ConditionUnitTypes.None, SelIdx<IdxC>().Idx);
                }
                else
                {
                    EntityPool.Rpc<RpcC>().ConditionUnitToMaster(condUnitType, SelIdx<IdxC>().Idx);
                }
            }
            else SoundV<AudioSourceVC>(ClipTypes.Mistake).Play();
        }

        void UniqBut(ButtonTypes uniqBut)
        {
            if (WhoseMoveE.IsMyMove)
            {
                ref var abil = ref UnitBuildButton<UniqueAbilityC>(uniqBut, SelIdx<IdxC>().Idx);


                if (!Unit<CooldownC>(abil.Ability, SelIdx<IdxC>().Idx).HaveCooldown)
                {
                    switch (uniqBut)
                    {
                        case ButtonTypes.None: throw new Exception();

                        case ButtonTypes.First:
                            {
                                switch (abil.Ability)
                                {
                                    case UniqueAbilityTypes.None: throw new Exception();

                                    case UniqueAbilityTypes.FirePawn:
                                        EntityPool.Rpc<RpcC>().FirePawnToMas(SelIdx<IdxC>().Idx);
                                        TryOnHint(VideoClipTypes.SeedFire);
                                        break;

                                    case UniqueAbilityTypes.PutOutFirePawn:
                                        EntityPool.Rpc<RpcC>().PutOutFirePawnToMas(SelIdx<IdxC>().Idx);
                                        TryOnHint(VideoClipTypes.SeedFire);
                                        break;

                                    case UniqueAbilityTypes.Seed:
                                        EntityPool.Rpc<RpcC>().SeedEnvToMaster(SelIdx<IdxC>().Idx, EnvTypes.YoungForest);
                                        TryOnHint(VideoClipTypes.SeedFire);
                                        break;

                                    case UniqueAbilityTypes.FireArcher:
                                        ClickerObject<CellClickC>().Click = CellClickTypes.UniqAbil;
                                        SelUniqAbilC.UniqAbil = UniqueAbilityTypes.FireArcher;
                                        TryOnHint(VideoClipTypes.SeedFire);
                                        break;

                                    case UniqueAbilityTypes.CircularAttack:
                                        EntityPool.Rpc<RpcC>().CircularAttackKingToMaster(SelIdx<IdxC>().Idx);
                                        TryOnHint(VideoClipTypes.CircularAttack);
                                        break;

                                    case UniqueAbilityTypes.GrowAdultForest:
                                        EntityPool.Rpc<RpcC>().GrowAdultForest(SelIdx<IdxC>().Idx);
                                        TryOnHint(VideoClipTypes.GrowingAdForesElfemale);
                                        break;
                                    default: throw new Exception();
                                }
                            }
                            break;

                        case ButtonTypes.Second:
                            {
                                switch (abil.Ability)
                                {
                                    case UniqueAbilityTypes.None: throw new Exception();

                                    case UniqueAbilityTypes.BonusNear:
                                        EntityPool.Rpc<RpcC>().BonusNearUnits(SelIdx<IdxC>().Idx);
                                        TryOnHint(VideoClipTypes.BonusKing);
                                        break;

                                    case UniqueAbilityTypes.StunElfemale:
                                        {
                                            ClickerObject<CellClickC>().Click = CellClickTypes.UniqAbil;
                                            SelUniqAbilC.UniqAbil = UniqueAbilityTypes.StunElfemale;
                                            TryOnHint(VideoClipTypes.StunElfemale);
                                        }
                                        break;

                                    case UniqueAbilityTypes.ChangeCornerArcher:
                                        {
                                            EntityPool.Rpc<RpcC>().ChangeCornerArchToMas(SelIdx<IdxC>().Idx);
                                        }
                                        break;

                                    default: throw new Exception();
                                }
                            }
                            break;

                        case ButtonTypes.Third:
                            {
                                switch (abil.Ability)
                                {
                                    case UniqueAbilityTypes.None: throw new Exception();
                                    case UniqueAbilityTypes.ChangeDirWind:
                                        {
                                            TryOnHint(VideoClipTypes.PutOutElfemale);
                                            ClickerObject<CellClickC>().Click = CellClickTypes.UniqAbil;
                                            SelUniqAbilC.UniqAbil = UniqueAbilityTypes.ChangeDirWind;
                                        }
                                        break;
                                    default: throw new Exception();
                                }
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
            var idx_sel = SelIdx<IdxC>().Idx;

            if (WhoseMoveE.IsMyMove)
            {
                switch (buildBut)
                {
                    case ButtonTypes.None:
                        throw new Exception();

                    case ButtonTypes.First:
                        EntityPool.Rpc<RpcC>().BuildToMaster(idx_sel, BuildTypes.Farm);
                        TryOnHint(VideoClipTypes.BuldFarms);
                        break;

                    case ButtonTypes.Second:
                        EntityPool.Rpc<RpcC>().BuildToMaster(idx_sel, BuildTypes.Mine);
                        TryOnHint(VideoClipTypes.BuildMine);
                        break;

                    case ButtonTypes.Third:
                        var buildAbility = UnitBuildButton<BuildingC>(ButtonTypes.Third, idx_sel).Build;
                        if (buildAbility == BuildTypes.None)Rpc<RpcC>().DestroyBuildingToMaster(idx_sel);
                        else Rpc<RpcC>().BuildToMaster(idx_sel, UnitBuildButton<BuildingC>(ButtonTypes.Third, idx_sel).Build);

                        //switch (BuildAbilC.AbilityType(buildBut))
                        //{
                        //    case BuildAbilityTypes.None: throw new Exception();
                        //    case BuildAbilityTypes.FarmBuild: throw new Exception();
                        //    case BuildAbilityTypes.MineBuild: throw new Exception();
                        //    case BuildAbilityTypes.CityBuild:
                        //        EntityPool.Rpc<RpcC>().BuildToMaster(SelIdx<IdxC>().Idx, BuildTypes.City);
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

