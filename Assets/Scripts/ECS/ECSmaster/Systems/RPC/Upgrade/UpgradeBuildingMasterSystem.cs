using Photon.Pun;
using System.Collections.Generic;

internal sealed class UpgradeBuildingMasterSystem : RPCMasterSystemReduction
{
    internal BuildingTypes BuildingType => _eMM.RPCMasterEnt_RPCMasterCom.BuildingType;
    internal PhotonMessageInfo Info => _eGM.RpcGeneralEnt_FromInfoCom.FromInfo;

    public override void Run()
    {
        base.Run();


        bool haveFood = true;
        bool haveWood = true;
        bool haveOre = true;
        bool haveIron = true;
        bool haveGold = true;

        Dictionary<bool, int> currentUpgradeBuildingsDict = new Dictionary<bool, int>();

        int minusFood = default;
        int minusWood = default;
        int minusOre = default;
        int minusIron = default;
        int minusGold = default;

        switch (BuildingType)
        {
            case BuildingTypes.None:
                break;

            case BuildingTypes.City:
                break;

            case BuildingTypes.Farm:
                minusFood = 0;
                minusWood = 0;
                minusOre = 0;
                minusIron = 0;
                minusGold = 1;

                currentUpgradeBuildingsDict = _eGM.InfoEnt_UpgradeInfoCom.AmountUpgradeFarmDict;
                break;

            case BuildingTypes.Woodcutter:
                minusFood = 0;
                minusWood = 0;
                minusOre = 0;
                minusIron = 0;
                minusGold = 1;

                currentUpgradeBuildingsDict = _eGM.InfoEnt_UpgradeInfoCom.AmountUpgradeWoodcutterDict;
                break;

            case BuildingTypes.Mine:
                minusFood = 0;
                minusWood = 0;
                minusOre = 0;
                minusIron = 0;
                minusGold = 1;

                currentUpgradeBuildingsDict = _eGM.InfoEnt_UpgradeInfoCom.AmountUpgradeMineDict;
                break;

            default:
                break;
        }


        haveFood = _eGM.EconomyEnt_EconomyCom.Food(Info.Sender.IsMasterClient) >= minusFood;
        haveWood = _eGM.EconomyEnt_EconomyCom.Wood(Info.Sender.IsMasterClient) >= minusWood;
        haveOre = _eGM.EconomyEnt_EconomyCom.Ore(Info.Sender.IsMasterClient) >= minusOre;
        haveIron = _eGM.EconomyEnt_EconomyCom.Iron(Info.Sender.IsMasterClient) >= minusIron;
        haveGold = _eGM.EconomyEnt_EconomyCom.Gold(Info.Sender.IsMasterClient) >= minusGold;

        if (haveFood && haveWood && haveOre && haveIron && haveGold)
        {
            _eGM.EconomyEnt_EconomyCom.TakeFood(Info.Sender.IsMasterClient, minusFood);
            _eGM.EconomyEnt_EconomyCom.TakeWood(Info.Sender.IsMasterClient, minusWood);
            _eGM.EconomyEnt_EconomyCom.TakeOre(Info.Sender.IsMasterClient, minusOre);
            _eGM.EconomyEnt_EconomyCom.TakeIron(Info.Sender.IsMasterClient, minusIron);
            _eGM.EconomyEnt_EconomyCom.TakeGold(Info.Sender.IsMasterClient, minusGold);

            currentUpgradeBuildingsDict[Info.Sender.IsMasterClient] += 1;
        }
        _photonPunRPC.MistakeEconomyToGeneral(Info.Sender, haveFood, haveWood, haveOre, haveIron, haveGold);
    }
}
