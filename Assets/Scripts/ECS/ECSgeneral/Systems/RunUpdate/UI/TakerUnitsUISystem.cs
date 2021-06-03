using static Main;

internal sealed class TakerUnitsUISystem : RPCGeneralReduction
{
    public override void Init()
    {
        base.Init();

        _eGM.TakerKingEntityButtonComponent.Button.onClick.AddListener(delegate { GetUnit(_eGM.TakerKingEntityUnitTypeComponent.UnitType); });
        _eGM.TakerPawnEntityButtonComponent.Button.onClick.AddListener(delegate { GetUnit(_eGM.TakerPawnEntityUnitTypeComponent.UnitType); });
        _eGM.TakerRookEntityButtonComponent.Button.onClick.AddListener(delegate { GetUnit(_eGM.TakerRookEntityUnitTypeComponent.UnitType); });
        _eGM.TakerBishopEntityButtonComponent.Button.onClick.AddListener(delegate { GetUnit(_eGM.TakerBishopEntityUnitTypeComponent.UnitType); });
    }

    public override void Run()
    {
        base.Run();

        if (_eGM.InfoEnt_UnitsInfoCom.IsSettedKingDict[Instance.IsMasterClient])
            _eGM.TakerKingEntityButtonComponent.Button.gameObject.SetActive(false);
        else _eGM.TakerKingEntityButtonComponent.Button.gameObject.SetActive(true);
    }

    private void GetUnit(UnitTypes unitType)
    {
        if (!_eGM.DonerEntityIsActivatedDictionaryComponent.IsActivatedDictionary[Instance.IsMasterClient]) _photonPunRPC.GetUnitToMaster(unitType);
    }
}
