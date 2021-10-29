using Leopotam.Ecs;

namespace Scripts.Game
{
    internal sealed class GiveTakeTWMasSys : IEcsRunSystem
    {
        private EcsFilter<ForGiveTakeToolWeaponComp> _forGiveTakeToolWeapFilter = default;

        private EcsFilter<CellUnitDataCom, HpComponent, StepComponent, ToolWeaponC, OwnerCom> _cellUnitFilter = default;

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
                ref var twUnitC_forGive = ref _cellUnitFilter.Get4(neededIdxCell);
                ref var ownUnitC = ref _cellUnitFilter.Get5(neededIdxCell);


                if (unitCForGive.Is(UnitTypes.Pawn))
                {
                    if (stepUnitC_forGive.HaveMinAmountSteps || unitCForGive.Have(StatTypes.Steps))
                    {
                        if (_cellUnitFilter.Get2(neededIdxCell).HaveMaxAmountHealth(unitCForGive.UnitType))
                        {
                            if (twUnitC_forGive.HaveExtraTW)
                            {
                                InventorTWCom.AddAmountTools(ownUnitC.PlayerType, twUnitC_forGive.TWExtraType, twUnitC_forGive.LevelTWType);

                                if (unitCForGive.Have(StatTypes.Steps)) unitCForGive.DefStat(StatTypes.Steps);
                                else stepUnitC_forGive.TakeAmountSteps();
                                unitCForGive.CondUnitType = default;

                                if (twUnitC_forGive.HaveShield 
                                    && tWTypeForGive == ToolWeaponTypes.Shield
                                    && twUnitC_forGive.LevelTWType != levelTWType)
                                {
                                    twUnitC_forGive.LevelTWType = levelTWType;
                                }
                                else
                                {
                                    twUnitC_forGive.TWExtraType = default;
                                    twUnitC_forGive.LevelTWType = default;
                                }
     

                                RpcSys.SoundToGeneral(sender, SoundEffectTypes.PickMelee);
                            }


                            else if (InventorTWCom.HaveTW(ownUnitC.PlayerType, tWTypeForGive, levelTWType))
                            {
                                InventorTWCom.TakeAmountTools(ownUnitC.PlayerType, tWTypeForGive, levelTWType);

                                twUnitC_forGive.TWExtraType = tWTypeForGive;
                                twUnitC_forGive.LevelTWType = levelTWType;
                                if(twUnitC_forGive.HaveShield) twUnitC_forGive.AddShieldProtect(levelTWType);

                                if (unitCForGive.Have(StatTypes.Steps)) unitCForGive.DefStat(StatTypes.Steps);
                                else stepUnitC_forGive.TakeAmountSteps();

                                unitCForGive.CondUnitType = default;

                                RpcSys.SoundToGeneral(sender, SoundEffectTypes.PickMelee);
                            }

                            else if (tWTypeForGive == ToolWeaponTypes.Pick)
                            {
                                if (InventResourcesC.CanBuyTW(ownUnitC.PlayerType, ToolWeaponTypes.Pick, levelTWType, out var needRes))
                                {
                                    InventResourcesC.BuyTW(ownUnitC.PlayerType, ToolWeaponTypes.Pick, levelTWType);

                                    twUnitC_forGive.TWExtraType = tWTypeForGive;
                                    twUnitC_forGive.LevelTWType = levelTWType;

                                    unitCForGive.CondUnitType = default;
                                    if (unitCForGive.Have(StatTypes.Steps)) unitCForGive.DefStat(StatTypes.Steps);
                                    else stepUnitC_forGive.TakeAmountSteps();

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

                                    twUnitC_forGive.TWExtraType = tWTypeForGive;
                                    twUnitC_forGive.LevelTWType = levelTWType;

                                    unitCForGive.CondUnitType = default;
                                    if (unitCForGive.Have(StatTypes.Steps)) unitCForGive.DefStat(StatTypes.Steps);
                                    else stepUnitC_forGive.TakeAmountSteps();

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

                                    twUnitC_forGive.TWExtraType = tWTypeForGive;
                                    twUnitC_forGive.LevelTWType = levelTWType;
                                    twUnitC_forGive.AddShieldProtect(levelTWType);

                                    unitCForGive.CondUnitType = default;
                                    if (unitCForGive.Have(StatTypes.Steps)) unitCForGive.DefStat(StatTypes.Steps);
                                    else stepUnitC_forGive.TakeAmountSteps();

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
