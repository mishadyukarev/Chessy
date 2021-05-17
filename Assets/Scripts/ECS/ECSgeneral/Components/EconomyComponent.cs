using static MainGame;

internal struct EconomyComponent
{
    internal int FoodMaster;
    internal int FoodOther;

    internal int WoodMaster;
    internal int WoodOther;

    internal int OreMaster;
    internal int OreOther;

    internal int IronMaster;
    internal int IronOther;

    internal int GoldMaster;
    internal int GoldOther;


    internal EconomyComponent(StartValuesGameConfig startValuesGameConfig)
    {
        GoldMaster = startValuesGameConfig.AMOUNT_GOLD_MASTER;
        GoldOther = startValuesGameConfig.AMOUNT_GOLD_OTHER;

        FoodMaster = startValuesGameConfig.AMOUNT_FOOD_MASTER;
        FoodOther = startValuesGameConfig.AMOUNT_FOOD_OTHER;

        WoodMaster = startValuesGameConfig.AMOUNT_WOOD_MASTER;
        WoodOther = startValuesGameConfig.AMOUNT_WOOD_OTHER;

        OreMaster = startValuesGameConfig.AMOUNT_ORE_MASTER;
        OreOther = startValuesGameConfig.AMOUNT_ORE_OTHER;

        IronMaster = startValuesGameConfig.AMOUNT_IRON_MASTER;
        IronOther = startValuesGameConfig.AMOUNT_IRON_OTHER;
    }

    internal int CurrentFood
    {
        get
        {
            if (InstanceGame.IsMasterClient) return FoodMaster;
            else return FoodOther;
        }
        set
        {
            if (InstanceGame.IsMasterClient) FoodMaster = value;
            else FoodOther = value;
        }
    }

    internal int CurrentWood
    {
        get
        {
            if (InstanceGame.IsMasterClient) return WoodMaster;
            else return WoodOther;
        }
        set
        {
            if (InstanceGame.IsMasterClient) WoodMaster = value;
            else WoodOther = value;
        }
    }

    internal int CurrentOre
    {
        get
        {
            if (InstanceGame.IsMasterClient) return OreMaster;
            else return OreOther;
        }
        set
        {
            if (InstanceGame.IsMasterClient) OreMaster = value;
            else OreOther = value;
        }
    }

    internal int CurrentIron
    {
        get
        {
            if (InstanceGame.IsMasterClient) return IronMaster;
            else return IronOther;
        }
        set
        {
            if (InstanceGame.IsMasterClient) IronMaster = value;
            else IronOther = value;
        }
    }

    internal int CurrentGold
    {
        get
        {
            if (InstanceGame.IsMasterClient) return GoldMaster;
            else return GoldOther;
        }
        set
        {
            if (InstanceGame.IsMasterClient) GoldMaster = value;
            else GoldOther = value;
        }
    }
}

