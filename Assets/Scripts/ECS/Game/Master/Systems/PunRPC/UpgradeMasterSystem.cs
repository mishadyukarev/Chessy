using Assets.Scripts.Abstractions.Enums;
using Photon.Pun;

namespace Assets.Scripts.ECS.Game.Master.Systems.PunRPC
{
    internal class UpgradeMasterSystem : RPCMasterSystemReduction
    {
        private const byte FOR_NEXT_UPGRADE = 1;

        private PhotonMessageInfo InfoFrom => _eMM.FromInfoEnt_FromInfoCom.InfoFrom;

        private UpgradeModTypes UpgradeModType => _eMM.UpgradeEnt_UpgradeTypeCom.UpgradeModType;
        private int[] XyCellForUpgrade => _eMM.UpgradeEnt_XyCellCom.XyCell;


        private UnitTypes NeededUnitTypeForUpgrade => _eGM.CellUnitEnt_UnitTypeCom(XyCellForUpgrade).UnitType;
        internal BuildingTypes NeededBuildingTypeForUpgrade => _eMM.UpgradeEnt_BuildingTypeCom.BuildingType;


        public override void Run()
        {
            base.Run();

            bool[] haves;

            switch (UpgradeModType)
            {
                case UpgradeModTypes.None:
                    break;

                case UpgradeModTypes.Unit:
                    if (_eGM.CellUnitEnt_UnitTypeCom(XyCellForUpgrade).HaveAnyUnit)
                    {
                        if (EconomyManager.CanUpgradeUnit(InfoFrom.Sender, NeededUnitTypeForUpgrade, out haves))
                        {
                            EconomyManager.UpgradeUnit(InfoFrom.Sender, NeededUnitTypeForUpgrade);
                            CellUnitWorker.ChangePlayerUnit(XyCellForUpgrade, NeededUnitTypeForUpgrade + FOR_NEXT_UPGRADE);

                            if (_eGM.CellUnitEnt_UnitTypeCom(XyCellForUpgrade).IsMelee)
                            {
                                _photonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.UpgradeUnitMelee);
                            }
                            else
                            {
                                _photonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.UpgradeUnitArcher);
                            }
                        }
                        else
                        {
                            _photonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Mistake);
                            _photonPunRPC.MistakeEconomyToGeneral(InfoFrom.Sender, haves);
                        }
                    }
                    break;

                case UpgradeModTypes.Building:
                    if (EconomyManager.CanUpgradeBuildings(InfoFrom.Sender, NeededBuildingTypeForUpgrade, out haves))
                    {
                        EconomyManager.UpgradeBuildings(InfoFrom.Sender, NeededBuildingTypeForUpgrade);

                        _photonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.SoundGoldPack);
                    }
                    else
                    {
                        _photonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Mistake);
                        _photonPunRPC.MistakeEconomyToGeneral(InfoFrom.Sender, haves);
                    }
                    break;

                default:
                    break;
            }
        }
    }
}
