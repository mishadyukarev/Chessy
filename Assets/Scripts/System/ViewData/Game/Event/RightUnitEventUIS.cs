using System;
using static Game.Game.EntityCellUnitPool;
using static Game.Game.EntityPool;
using static Game.Game.EntityVPool;

namespace Game.Game
{
    sealed class RightUnitEventUIS
    {
        internal RightUnitEventUIS()
        {
            UniqButtonsUIC.AddListener(UniqueButtonTypes.First, delegate { UniqBut(UniqueButtonTypes.First); });
            UniqButtonsUIC.AddListener(UniqueButtonTypes.Second, delegate { UniqBut(UniqueButtonTypes.Second); });
            UniqButtonsUIC.AddListener(UniqueButtonTypes.Third, delegate { UniqBut(UniqueButtonTypes.Third); });

            BuildAbilitUIC.AddListener_Button(BuildButtonTypes.First, delegate { ExecuteBuild_Button(BuildButtonTypes.First); });
            BuildAbilitUIC.AddListener_Button(BuildButtonTypes.Second, delegate { ExecuteBuild_Button(BuildButtonTypes.Second); });
            BuildAbilitUIC.AddListener_Button(BuildButtonTypes.Third, delegate { ExecuteBuild_Button(BuildButtonTypes.Third); });

            ProtectUIC.AddListener(delegate { ConditionAbilityButton(ConditionUnitTypes.Protected); });
            RelaxUIC.AddListener(delegate { ConditionAbilityButton(ConditionUnitTypes.Relaxed); });
        }

        private void ConditionAbilityButton(ConditionUnitTypes condUnitType)
        {
            if (WhoseMoveC.IsMyMove)
            {
                TryOnHint(VideoClipTypes.ProtRelax);

                if (Unit<ConditionC>(SelIdx<IdxC>().Idx).Is(condUnitType))
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

        private void UniqBut(UniqueButtonTypes uniqBut)
        {
            if (WhoseMoveC.IsMyMove)
            {
                ref var abil = ref Unit<UniqueAbilityC>(uniqBut, SelIdx<IdxC>().Idx);


                if (!Unit<CooldownC>(abil.Ability, SelIdx<IdxC>().Idx).HaveCooldown)
                {
                    switch (uniqBut)
                    {
                        case UniqueButtonTypes.None: throw new Exception();

                        case UniqueButtonTypes.First:
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
                                        ClickerObject<CellClickC>().Set(CellClickTypes.UniqAbil);
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

                        case UniqueButtonTypes.Second:
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
                                            ClickerObject<CellClickC>().Set(CellClickTypes.UniqAbil);
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

                        case UniqueButtonTypes.Third:
                            {
                                switch (abil.Ability)
                                {
                                    case UniqueAbilityTypes.None: throw new Exception();
                                    case UniqueAbilityTypes.ChangeDirWind:
                                        {
                                            TryOnHint(VideoClipTypes.PutOutElfemale);
                                            ClickerObject<CellClickC>().Set(CellClickTypes.UniqAbil);
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

        private void ExecuteBuild_Button(BuildButtonTypes buildBut)
        {
            if (WhoseMoveC.IsMyMove)
            {
                switch (buildBut)
                {
                    case BuildButtonTypes.None:
                        throw new Exception();

                    case BuildButtonTypes.First:
                        EntityPool.Rpc<RpcC>().BuildToMaster(SelIdx<IdxC>().Idx, BuildTypes.Farm);
                        TryOnHint(VideoClipTypes.BuldFarms);
                        break;

                    case BuildButtonTypes.Second:
                        EntityPool.Rpc<RpcC>().BuildToMaster(SelIdx<IdxC>().Idx, BuildTypes.Mine);
                        TryOnHint(VideoClipTypes.BuildMine);
                        break;

                    case BuildButtonTypes.Third:
                        switch (BuildAbilC.AbilityType(buildBut))
                        {
                            case BuildAbilTypes.None: throw new Exception();
                            case BuildAbilTypes.FarmBuild: throw new Exception();
                            case BuildAbilTypes.MineBuild: throw new Exception();
                            case BuildAbilTypes.CityBuild:
                                EntityPool.Rpc<RpcC>().BuildToMaster(SelIdx<IdxC>().Idx, BuildTypes.City);
                                break;

                            case BuildAbilTypes.Destroy:
                                EntityPool.Rpc<RpcC>().DestroyBuildingToMaster(SelIdx<IdxC>().Idx);
                                break;

                            default: throw new Exception();
                        }
                        break;

                    default: throw new Exception();
                }
            }
            else SoundV<AudioSourceVC>(ClipTypes.Mistake).Play();
        }

        private void TryOnHint(VideoClipTypes videoClip)
        {
            if (Common.HintC.IsOnHint)
            {
                if (!HintC.WasActived(videoClip))
                {
                    //EntityCenterHintUIPool.SetActiveHintZone(true);
                    //EntityCenterHintUIPool.SetVideoClip(videoClip);
                    HintC.SetWasActived(videoClip, true);
                }
            }
        }
    }
}

