using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.Game;
using Assets.Scripts.ECS.Component.Game.Master;
using Assets.Scripts.ECS.Game.General.Systems.StartFill;
using Assets.Scripts.ECS.System.Data.Game.General.Cell;
using Leopotam.Ecs;
using Photon.Realtime;

namespace Assets.Scripts.ECS.Game.Master.Systems.PunRPC
{
    internal class UpgradeMasterSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _currentGameWorld;
        private EcsFilter<InfoMasCom> _infoFilter;
        private EcsFilter<UpgradeMasCom, XyCellForDoingMasCom> _upgradeFilter;
        private EcsFilter<XyUnitsComponent> _xyUnitsFilter;
        private EcsFilter<InventorResourcesComponent> _inventResFilt;

        private const byte FOR_NEXT_UPGRADE = 1;

        private Player Sender => _infoFilter.Get1(0).FromInfo.Sender;

        private UpgradeModTypes UpgradeModType => _upgradeFilter.Get1(0).UpgradeModType;
        private int[] XyCellForUpgrade => _upgradeFilter.Get2(0).XyCellForDoing;


        private UnitTypes CurrentUnitType => CellUnitsDataSystem.UnitType(XyCellForUpgrade);
        private UnitTypes NeededUnitTypeForUpgrade => CellUnitsDataSystem.UnitType(XyCellForUpgrade) + FOR_NEXT_UPGRADE;
        private BuildingTypes NeededBuildingTypeForUpgrade => _upgradeFilter.Get1(0).BuildingType;


        public void Init()
        {
            _currentGameWorld.NewEntity()
                .Replace(new UpgradeMasCom())
                .Replace(new XyCellForDoingMasCom(new int[2]));
        }

        public void Run()
        {



            ref var xyUnitsCom = ref _xyUnitsFilter.Get1(0);
            ref var invResCom = ref _inventResFilt.Get1(0);

            bool[] haves;

            switch (UpgradeModType)
            {
                case UpgradeModTypes.None:
                    break;

                case UpgradeModTypes.Unit:
                    if (CellUnitsDataSystem.HaveAnyUnit(XyCellForUpgrade))
                    {
                        if (CellUnitsDataSystem.HaveOwner(XyCellForUpgrade))
                        {
                            if (CellUnitsDataSystem.IsHim(Sender, XyCellForUpgrade))
                            {
                                if (invResCom.CanUpgradeUnit(Sender, CurrentUnitType, out haves))
                                {
                                    invResCom.BuyUpgradeUnit(Sender, CurrentUnitType);


                                    var preConditionType = CellUnitsDataSystem.ConditionType(XyCellForUpgrade);
                                    var preUnitType = CellUnitsDataSystem.UnitType(XyCellForUpgrade);
                                    var preKey = CellUnitsDataSystem.IsMasterClient(XyCellForUpgrade);
                                    var preMaxHealth = CellUnitsDataSystem.MaxAmountHealth(preUnitType);

                                    MainGameSystem.XyUnitsContitionCom.RemoveUnitInCondition(preConditionType, preUnitType, preKey, XyCellForUpgrade);
                                    xyUnitsCom.RemoveAmountUnitsInGame(preUnitType, preKey, XyCellForUpgrade);


                                    CellUnitsDataSystem.SetUnitType(NeededUnitTypeForUpgrade, XyCellForUpgrade);

                                    var newUnitType = CellUnitsDataSystem.UnitType(XyCellForUpgrade);
                                    var newMaxHealth = CellUnitsDataSystem.MaxAmountHealth(newUnitType);

                                    CellUnitsDataSystem.AddAmountHealth(XyCellForUpgrade, newMaxHealth - preMaxHealth);

                                    xyUnitsCom.AddAmountUnitInGame(newUnitType, preKey, XyCellForUpgrade);
                                    MainGameSystem.XyUnitsContitionCom.AddUnitInCondition(preConditionType, newUnitType, preKey, XyCellForUpgrade);


                                    if (CellUnitsDataSystem.IsMelee(XyCellForUpgrade))
                                    {
                                        RPCGameSystem.SoundToGeneral(Sender, SoundEffectTypes.UpgradeUnitMelee);
                                    }
                                    else
                                    {
                                        RPCGameSystem.SoundToGeneral(Sender, SoundEffectTypes.UpgradeUnitArcher);
                                    }
                                }
                                else
                                {
                                    RPCGameSystem.SoundToGeneral(Sender, SoundEffectTypes.Mistake);
                                    RPCGameSystem.MistakeEconomyToGeneral(Sender, haves);
                                }
                            }
                        }
                    }
                    break;

                case UpgradeModTypes.Building:
                    if (invResCom.CanUpgradeBuildings(Sender, NeededBuildingTypeForUpgrade, out haves))
                    {
                        invResCom.BuyUpgradeBuildings(Sender, NeededBuildingTypeForUpgrade);

                        RPCGameSystem.SoundToGeneral(Sender, SoundEffectTypes.SoundGoldPack);
                    }
                    else
                    {
                        RPCGameSystem.SoundToGeneral(Sender, SoundEffectTypes.Mistake);
                        RPCGameSystem.MistakeEconomyToGeneral(Sender, haves);
                    }
                    break;

                default:
                    break;
            }
        }
    }
}
