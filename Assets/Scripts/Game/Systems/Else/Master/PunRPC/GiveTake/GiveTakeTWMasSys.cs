using Leopotam.Ecs;

namespace Scripts.Game
{
    internal sealed class GiveTakeTWMasSys : IEcsRunSystem
    {
        private EcsFilter<InfoCom> _infoFilter = default;
        private EcsFilter<ForGiveTakeToolWeaponComp> _forGiveTakeToolWeapFilter = default;

        private EcsFilter<InventResourCom> _inventResFilter = default;
        private EcsFilter<InventorTWCom> _inventTWFilt = default;

        private EcsFilter<CellUnitDataCom, OwnerCom> _cellUnitFilter = default;

        public void Run()
        {
            var neededIdxCell = _forGiveTakeToolWeapFilter.Get1(0).IdxCell;

            if (neededIdxCell != default)
            {
                var tWTypeForGive = _forGiveTakeToolWeapFilter.Get1(0).ToolWeapType;
                var levelTWType = _forGiveTakeToolWeapFilter.Get1(0).LevelTWType;

                ref var inventTWCom = ref _inventTWFilt.Get1(0);
                ref var invResCom = ref _inventResFilter.Get1(0);

                var sender = _infoFilter.Get1(0).FromInfo.Sender;

                ref var unitDatComForGive = ref _cellUnitFilter.Get1(neededIdxCell);
                ref var ownUnitCom = ref _cellUnitFilter.Get2(neededIdxCell);


                if (unitDatComForGive.Is(UnitTypes.Pawn))
                {
                    if (unitDatComForGive.HaveMinAmountSteps)
                    {
                        if (unitDatComForGive.HaveMaxAmountHealth)
                        {
                            if (unitDatComForGive.HaveExtraTW)
                            {
                                inventTWCom.AddAmountTools(ownUnitCom.PlayerType, unitDatComForGive.TWExtraType, unitDatComForGive.LevelTWType);

                                unitDatComForGive.TWExtraType = default;
                                unitDatComForGive.LevelTWType = default;

                                unitDatComForGive.CondUnitType = default;
                                unitDatComForGive.TakeAmountSteps();

                                RpcSys.SoundToGeneral(sender, SoundEffectTypes.PickMelee);
                            }

                            else
                            {
                                if (inventTWCom.HaveTW(ownUnitCom.PlayerType, tWTypeForGive, levelTWType))
                                {
                                    inventTWCom.TakeAmountTools(ownUnitCom.PlayerType, tWTypeForGive, levelTWType);

                                    unitDatComForGive.TWExtraType = tWTypeForGive;
                                    unitDatComForGive.LevelTWType = levelTWType;

                                    unitDatComForGive.TakeAmountSteps();
                                    unitDatComForGive.CondUnitType = default;

                                    RpcSys.SoundToGeneral(sender, SoundEffectTypes.PickMelee);
                                }

                                else if (tWTypeForGive == ToolWeaponTypes.Pick)
                                {
                                    if (invResCom.CanBuyTW(ownUnitCom.PlayerType, ToolWeaponTypes.Pick, levelTWType, out var needRes))
                                    {
                                        invResCom.BuyTW(ownUnitCom.PlayerType, ToolWeaponTypes.Pick, levelTWType);

                                        unitDatComForGive.TWExtraType = tWTypeForGive;
                                        unitDatComForGive.LevelTWType = levelTWType;

                                        unitDatComForGive.CondUnitType = default;
                                        unitDatComForGive.TakeAmountSteps();

                                        RpcSys.SoundToGeneral(sender, SoundEffectTypes.PickMelee);
                                    }
                                    else
                                    {
                                        RpcSys.MistakeEconomyToGeneral(sender, needRes);
                                    }
                                }

                                else if (tWTypeForGive == ToolWeaponTypes.Sword)
                                {
                                    if (invResCom.CanBuyTW(ownUnitCom.PlayerType, ToolWeaponTypes.Sword, levelTWType, out var needRes))
                                    {
                                        invResCom.BuyTW(ownUnitCom.PlayerType, ToolWeaponTypes.Sword, levelTWType);

                                        unitDatComForGive.TWExtraType = tWTypeForGive;
                                        unitDatComForGive.LevelTWType = levelTWType;

                                        unitDatComForGive.CondUnitType = default;
                                        unitDatComForGive.TakeAmountSteps();

                                        RpcSys.SoundToGeneral(sender, SoundEffectTypes.PickMelee);
                                    }
                                    else
                                    {
                                        RpcSys.MistakeEconomyToGeneral(sender, needRes);
                                    }
                                }

                                else if (tWTypeForGive == ToolWeaponTypes.Shield)
                                {
                                    if (invResCom.CanBuyTW(ownUnitCom.PlayerType, tWTypeForGive, levelTWType, out var needRes))
                                    {
                                        invResCom.BuyTW(ownUnitCom.PlayerType, tWTypeForGive, levelTWType);

                                        unitDatComForGive.TWExtraType = tWTypeForGive;
                                        unitDatComForGive.LevelTWType = levelTWType;
                                        unitDatComForGive.AddShieldProtect(levelTWType);

                                        unitDatComForGive.CondUnitType = default;
                                        unitDatComForGive.TakeAmountSteps();

                                        RpcSys.SoundToGeneral(sender, SoundEffectTypes.PickMelee);
                                    }
                                    else
                                    {
                                        RpcSys.MistakeEconomyToGeneral(sender, needRes);
                                    }
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
