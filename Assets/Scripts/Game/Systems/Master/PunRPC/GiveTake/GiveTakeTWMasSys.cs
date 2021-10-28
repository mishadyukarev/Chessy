using Leopotam.Ecs;

namespace Scripts.Game
{
    internal sealed class GiveTakeTWMasSys : IEcsRunSystem
    {
        private EcsFilter<ForGiveTakeToolWeaponComp> _forGiveTakeToolWeapFilter = default;

        private EcsFilter<CellUnitDataCom, OwnerCom> _cellUnitFilter = default;

        public void Run()
        {
            var neededIdxCell = _forGiveTakeToolWeapFilter.Get1(0).IdxCell;

            if (neededIdxCell != default)
            {
                var tWTypeForGive = _forGiveTakeToolWeapFilter.Get1(0).ToolWeapType;
                var levelTWType = _forGiveTakeToolWeapFilter.Get1(0).LevelTWType;

                var sender = InfoC.Sender(MasGenOthTypes.Master);

                ref var unitCForGive = ref _cellUnitFilter.Get1(neededIdxCell);
                ref var ownUnitC = ref _cellUnitFilter.Get2(neededIdxCell);


                if (unitCForGive.Is(UnitTypes.Pawn))
                {
                    if (unitCForGive.HaveMinAmountSteps || unitCForGive.Have(StatTypes.Steps))
                    {
                        if (unitCForGive.HaveMaxAmountHealth)
                        {
                            if (unitCForGive.HaveExtraTW)
                            {
                                InventorTWCom.AddAmountTools(ownUnitC.PlayerType, unitCForGive.TWExtraType, unitCForGive.LevelTWType);

                                if (unitCForGive.Have(StatTypes.Steps)) unitCForGive.DefStat(StatTypes.Steps);
                                else unitCForGive.TakeAmountSteps();
                                unitCForGive.CondUnitType = default;

                                if (unitCForGive.HaveShield 
                                    && tWTypeForGive == ToolWeaponTypes.Shield
                                    && unitCForGive.LevelTWType != levelTWType)
                                {
                                    unitCForGive.LevelTWType = levelTWType;
                                }
                                else
                                {
                                    unitCForGive.TWExtraType = default;
                                    unitCForGive.LevelTWType = default;
                                }
     

                                RpcSys.SoundToGeneral(sender, SoundEffectTypes.PickMelee);
                            }


                            else if (InventorTWCom.HaveTW(ownUnitC.PlayerType, tWTypeForGive, levelTWType))
                            {
                                InventorTWCom.TakeAmountTools(ownUnitC.PlayerType, tWTypeForGive, levelTWType);

                                unitCForGive.TWExtraType = tWTypeForGive;
                                unitCForGive.LevelTWType = levelTWType;
                                if(unitCForGive.HaveShield) unitCForGive.AddShieldProtect(levelTWType);

                                if (unitCForGive.Have(StatTypes.Steps)) unitCForGive.DefStat(StatTypes.Steps);
                                else unitCForGive.TakeAmountSteps();

                                unitCForGive.CondUnitType = default;

                                RpcSys.SoundToGeneral(sender, SoundEffectTypes.PickMelee);
                            }

                            else if (tWTypeForGive == ToolWeaponTypes.Pick)
                            {
                                if (InventResourcesC.CanBuyTW(ownUnitC.PlayerType, ToolWeaponTypes.Pick, levelTWType, out var needRes))
                                {
                                    InventResourcesC.BuyTW(ownUnitC.PlayerType, ToolWeaponTypes.Pick, levelTWType);

                                    unitCForGive.TWExtraType = tWTypeForGive;
                                    unitCForGive.LevelTWType = levelTWType;

                                    unitCForGive.CondUnitType = default;
                                    if (unitCForGive.Have(StatTypes.Steps)) unitCForGive.DefStat(StatTypes.Steps);
                                    else unitCForGive.TakeAmountSteps();

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

                                    unitCForGive.TWExtraType = tWTypeForGive;
                                    unitCForGive.LevelTWType = levelTWType;

                                    unitCForGive.CondUnitType = default;
                                    if (unitCForGive.Have(StatTypes.Steps)) unitCForGive.DefStat(StatTypes.Steps);
                                    else unitCForGive.TakeAmountSteps();

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

                                    unitCForGive.TWExtraType = tWTypeForGive;
                                    unitCForGive.LevelTWType = levelTWType;
                                    unitCForGive.AddShieldProtect(levelTWType);

                                    unitCForGive.CondUnitType = default;
                                    if (unitCForGive.Have(StatTypes.Steps)) unitCForGive.DefStat(StatTypes.Steps);
                                    else unitCForGive.TakeAmountSteps();

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
