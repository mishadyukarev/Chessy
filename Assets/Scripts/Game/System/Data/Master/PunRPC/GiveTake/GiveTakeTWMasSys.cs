﻿using Leopotam.Ecs;
using Chessy.Common;

namespace Chessy.Game
{
    public sealed class GiveTakeTWMasSys : IEcsRunSystem
    {
        private EcsFilter<ForGiveTakeToolWeaponComp> _forGiveTakeToolWeapFilter = default;

        private EcsFilter<UnitC, LevelC, OwnerC> _unitF = default;
        private EcsFilter<HpC, StepC> _statUnitF = default;
        private EcsFilter<ToolWeaponC> _twUnitF = default;

        public void Run()
        {
            var idx_0 = _forGiveTakeToolWeapFilter.Get1(0).IdxCell;

            if (idx_0 != default)
            {
                var tWTypeForGive = _forGiveTakeToolWeapFilter.Get1(0).ToolWeapType;
                var levelTWType = _forGiveTakeToolWeapFilter.Get1(0).LevelTWType;

                var sender = InfoC.Sender(MGOTypes.Master);

                ref var unit_0 = ref _unitF.Get1(idx_0);

                ref var levUnit_0 = ref _unitF.Get2(idx_0);
                ref var ownUnit_0 = ref _unitF.Get3(idx_0);

                ref var stepUnit_0 = ref _statUnitF.Get2(idx_0);

                ref var twUnit_0 = ref _twUnitF.Get1(idx_0);


                if (unit_0.Is(UnitTypes.Pawn))
                {
                    if (stepUnit_0.HaveMinSteps)
                    {

                        if (twUnit_0.HaveToolWeap)
                        {
                            InvToolWeapC.AddAmountTools(ownUnit_0.Owner, twUnit_0.ToolWeapType, twUnit_0.LevelTWType);
                            WhereUnitsC.Remove(ownUnit_0.Owner, unit_0.Unit, levUnit_0.Level, idx_0);

                            stepUnit_0.TakeSteps();

                            if (twUnit_0.Is(ToolWeaponTypes.Shield)
                                && tWTypeForGive == ToolWeaponTypes.Shield
                                && twUnit_0.LevelTWType != levelTWType)
                            {
                                twUnit_0.LevelTWType = levelTWType;
                            }
                            else
                            {
                                twUnit_0.ToolWeapType = default;
                                twUnit_0.LevelTWType = default;
                            }
                            WhereUnitsC.Add(ownUnit_0.Owner, unit_0.Unit, levUnit_0.Level, idx_0);

                            RpcSys.SoundToGeneral(sender, ClipTypes.PickMelee);
                        }


                        else if (InvToolWeapC.HaveTW(ownUnit_0.Owner, tWTypeForGive, levelTWType))
                        {
                            InvToolWeapC.TakeAmountTools(ownUnit_0.Owner, tWTypeForGive, levelTWType);
                            WhereUnitsC.Remove(ownUnit_0.Owner, unit_0.Unit, levUnit_0.Level, idx_0);

                            twUnit_0.ToolWeapType = tWTypeForGive;
                            twUnit_0.LevelTWType = levelTWType;
                            if (twUnit_0.Is(ToolWeaponTypes.Shield)) twUnit_0.AddShieldProtect(levelTWType);
                            WhereUnitsC.Add(ownUnit_0.Owner, unit_0.Unit, levUnit_0.Level, idx_0);

                            stepUnit_0.TakeSteps();

                            RpcSys.SoundToGeneral(sender, ClipTypes.PickMelee);
                        }

                        else if (tWTypeForGive == ToolWeaponTypes.Pick)
                        {
                            if (InvResC.CanBuyTW(ownUnit_0.Owner, ToolWeaponTypes.Pick, levelTWType, out var needRes))
                            {
                                InvResC.BuyTW(ownUnit_0.Owner, ToolWeaponTypes.Pick, levelTWType);

                                WhereUnitsC.Remove(ownUnit_0.Owner, unit_0.Unit, levUnit_0.Level, idx_0);
                                twUnit_0.ToolWeapType = tWTypeForGive;
                                twUnit_0.LevelTWType = levelTWType;
                                WhereUnitsC.Add(ownUnit_0.Owner, unit_0.Unit, levUnit_0.Level, idx_0);

                                stepUnit_0.TakeSteps();

                                RpcSys.SoundToGeneral(sender, ClipTypes.PickMelee);
                            }
                            else
                            {
                                RpcSys.MistakeEconomyToGeneral(sender, needRes);
                            }
                        }

                        else if (tWTypeForGive == ToolWeaponTypes.Sword)
                        {
                            if (InvResC.CanBuyTW(ownUnit_0.Owner, ToolWeaponTypes.Sword, levelTWType, out var needRes))
                            {
                                InvResC.BuyTW(ownUnit_0.Owner, ToolWeaponTypes.Sword, levelTWType);

                                WhereUnitsC.Remove(ownUnit_0.Owner, unit_0.Unit, levUnit_0.Level, idx_0);
                                twUnit_0.ToolWeapType = tWTypeForGive;
                                twUnit_0.LevelTWType = levelTWType;
                                WhereUnitsC.Add(ownUnit_0.Owner, unit_0.Unit, levUnit_0.Level, idx_0);

                                stepUnit_0.TakeSteps();

                                RpcSys.SoundToGeneral(sender, ClipTypes.PickMelee);
                            }
                            else
                            {
                                RpcSys.MistakeEconomyToGeneral(sender, needRes);
                            }
                        }

                        else if (tWTypeForGive == ToolWeaponTypes.Shield)
                        {
                            if (InvResC.CanBuyTW(ownUnit_0.Owner, tWTypeForGive, levelTWType, out var needRes))
                            {
                                InvResC.BuyTW(ownUnit_0.Owner, tWTypeForGive, levelTWType);

                                WhereUnitsC.Remove(ownUnit_0.Owner, unit_0.Unit, levUnit_0.Level, idx_0);
                                twUnit_0.ToolWeapType = tWTypeForGive;
                                twUnit_0.LevelTWType = levelTWType;
                                twUnit_0.AddShieldProtect(levelTWType);
                                WhereUnitsC.Add(ownUnit_0.Owner, unit_0.Unit, levUnit_0.Level, idx_0);

                                stepUnit_0.TakeSteps();

                                RpcSys.SoundToGeneral(sender, ClipTypes.PickMelee);
                            }
                            else
                            {
                                RpcSys.MistakeEconomyToGeneral(sender, needRes);
                            }
                        }
                    }
                    else RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }
        }
    }
}
