using Leopotam.Ecs;

namespace Scripts.Game
{
    internal sealed class GiveTakeTWMasSys : IEcsRunSystem
    {
        private EcsFilter<ForGiveTakeToolWeaponComp> _forGiveTakeToolWeapFilter = default;

        private EcsFilter<CellUnitDataCom, HpUnitC, StepComponent> _cellUnitFilter = default;
        private EcsFilter<CellUnitDataCom, ConditionUnitC, ToolWeaponC, UnitEffectsC, OwnerCom> _cellUnitOthFilt = default;

        public void Run()
        {
            var neededIdxCell = _forGiveTakeToolWeapFilter.Get1(0).IdxCell;

            if (neededIdxCell != default)
            {
                var tWTypeForGive = _forGiveTakeToolWeapFilter.Get1(0).ToolWeapType;
                var levelTWType = _forGiveTakeToolWeapFilter.Get1(0).LevelTWType;

                var sender = InfoC.Sender(MasGenOthTypes.Master);

                ref var unitCForGive = ref _cellUnitFilter.Get1(neededIdxCell);
                ref var stepUnitC_forGive = ref _cellUnitFilter.Get3(neededIdxCell);

                ref var condUnitC_forGive = ref _cellUnitOthFilt.Get2(neededIdxCell);
                ref var twUnitC_forGive = ref _cellUnitOthFilt.Get3(neededIdxCell);
                ref var effUnitC_forGive = ref _cellUnitOthFilt.Get4(neededIdxCell);
                ref var ownUnitC = ref _cellUnitOthFilt.Get5(neededIdxCell);


                if (unitCForGive.Is(UnitTypes.Pawn))
                {
                    if (stepUnitC_forGive.HaveMinSteps)
                    {
                        if (_cellUnitFilter.Get2(neededIdxCell).HaveMaxHpUnit(effUnitC_forGive, unitCForGive.UnitType))
                        {
                            if (twUnitC_forGive.HaveToolWeap)
                            {
                                InventorTWCom.AddAmountTools(ownUnitC.PlayerType, twUnitC_forGive.ToolWeapType, twUnitC_forGive.LevelTWType);

                                stepUnitC_forGive.TakeSteps();
                                condUnitC_forGive.DefCondition();

                                if (twUnitC_forGive.Is(ToolWeaponTypes.Shield)
                                    && tWTypeForGive == ToolWeaponTypes.Shield
                                    && twUnitC_forGive.LevelTWType != levelTWType)
                                {
                                    twUnitC_forGive.LevelTWType = levelTWType;
                                }
                                else
                                {
                                    twUnitC_forGive.ToolWeapType = default;
                                    twUnitC_forGive.LevelTWType = default;
                                }
     

                                RpcSys.SoundToGeneral(sender, SoundEffectTypes.PickMelee);
                            }


                            else if (InventorTWCom.HaveTW(ownUnitC.PlayerType, tWTypeForGive, levelTWType))
                            {
                                InventorTWCom.TakeAmountTools(ownUnitC.PlayerType, tWTypeForGive, levelTWType);

                                twUnitC_forGive.ToolWeapType = tWTypeForGive;
                                twUnitC_forGive.LevelTWType = levelTWType;
                                if(twUnitC_forGive.Is(ToolWeaponTypes.Shield)) twUnitC_forGive.AddShieldProtect(levelTWType);

                                stepUnitC_forGive.TakeSteps();

                                condUnitC_forGive.DefCondition();

                                RpcSys.SoundToGeneral(sender, SoundEffectTypes.PickMelee);
                            }

                            else if (tWTypeForGive == ToolWeaponTypes.Pick)
                            {
                                if (InventResourcesC.CanBuyTW(ownUnitC.PlayerType, ToolWeaponTypes.Pick, levelTWType, out var needRes))
                                {
                                    InventResourcesC.BuyTW(ownUnitC.PlayerType, ToolWeaponTypes.Pick, levelTWType);

                                    twUnitC_forGive.ToolWeapType = tWTypeForGive;
                                    twUnitC_forGive.LevelTWType = levelTWType;

                                    condUnitC_forGive.DefCondition();
                                    stepUnitC_forGive.TakeSteps();

                                    RpcSys.SoundToGeneral(sender, SoundEffectTypes.PickMelee);
                                }
                                else
                                {
                                    RpcSys.MistakeEconomyToGeneral(sender, needRes);
                                }
                            }

                            else if (tWTypeForGive == ToolWeaponTypes.Sword)
                            {
                                if (InventResourcesC.CanBuyTW(ownUnitC.PlayerType, ToolWeaponTypes.Sword, levelTWType, out var needRes))
                                {
                                    InventResourcesC.BuyTW(ownUnitC.PlayerType, ToolWeaponTypes.Sword, levelTWType);

                                    twUnitC_forGive.ToolWeapType = tWTypeForGive;
                                    twUnitC_forGive.LevelTWType = levelTWType;

                                    condUnitC_forGive.DefCondition();
                                    stepUnitC_forGive.TakeSteps();

                                    RpcSys.SoundToGeneral(sender, SoundEffectTypes.PickMelee);
                                }
                                else
                                {
                                    RpcSys.MistakeEconomyToGeneral(sender, needRes);
                                }
                            }

                            else if (tWTypeForGive == ToolWeaponTypes.Shield)
                            {
                                if (InventResourcesC.CanBuyTW(ownUnitC.PlayerType, tWTypeForGive, levelTWType, out var needRes))
                                {
                                    InventResourcesC.BuyTW(ownUnitC.PlayerType, tWTypeForGive, levelTWType);

                                    twUnitC_forGive.ToolWeapType = tWTypeForGive;
                                    twUnitC_forGive.LevelTWType = levelTWType;
                                    twUnitC_forGive.AddShieldProtect(levelTWType);

                                    condUnitC_forGive.DefCondition();
                                    stepUnitC_forGive.TakeSteps();

                                    RpcSys.SoundToGeneral(sender, SoundEffectTypes.PickMelee);
                                }
                                else
                                {
                                    RpcSys.MistakeEconomyToGeneral(sender, needRes);
                                }
                            }
                        }
                        else RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreHealth, sender);
                    }
                    else RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }
        }
    }
}
