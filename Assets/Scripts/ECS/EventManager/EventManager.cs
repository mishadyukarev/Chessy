using static Main;

internal sealed class EventManager
{
    private EntitiesGeneralManager _eGM;
    private PhotonPunRPC _photonPunRPC;

    internal EventManager(EntitiesGeneralManager eGM, PhotonPunRPC photonPunRPC)
    {
        _eGM = eGM;
        _photonPunRPC = photonPunRPC;
    }

    internal void FillEvents()
    {
        _eGM.ReadyEnt_ButtonCom.AddListener(Ready);

        _eGM.TakerKingEnt_ButtonCom.AddListener(delegate { GetUnit(_eGM.TakerKingEnt_UnitTypeCom.UnitType); });
        _eGM.TakerPawnEntityButtonComponent.AddListener(delegate { GetUnit(_eGM.TakerPawnEntityUnitTypeComponent.UnitType); });
        _eGM.TakerRookEntityButtonComponent.AddListener(delegate { GetUnit(_eGM.TakerRookEntityUnitTypeComponent.UnitType); });
        _eGM.TakerBishopEntityButtonComponent.AddListener(delegate { GetUnit(_eGM.TakerBishopEntityUnitTypeComponent.UnitType); });

        _eGM.DonerEnt_ButtonCom.AddListener(delegate { Done(); });

        _eGM.TruceEnt_ButtonCom.AddListener(Truce);

        _eGM.EnvironmentInfoEnt_ButtonCom.AddListener(EnvironmentInfo);
    }

    private void Ready() => _photonPunRPC.ReadyToMaster(!_eGM.ReadyEnt_ActivatedDictCom.IsActivated(Instance.IsMasterClient));
    private void GetUnit(UnitTypes unitType)
    {
        if (!_eGM.DonerEnt_IsActivatedDictCom.IsActivated(Instance.IsMasterClient)) _photonPunRPC.GetUnitToMaster(unitType);
    }
    private void Done() => _photonPunRPC.DoneToMaster(!_eGM.DonerEnt_IsActivatedDictCom.IsActivated(Instance.IsMasterClient));
    private void Truce() => _photonPunRPC.TruceToMaster(!_eGM.TruceEnt_ActivatedDictCom.IsActivated(Instance.IsMasterClient));
    private void EnvironmentInfo() => _eGM.EnvironmentInfoEnt_IsActivatedCom.IsActivated = !_eGM.EnvironmentInfoEnt_IsActivatedCom.IsActivated;
}