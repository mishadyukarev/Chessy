using Leopotam.Ecs;
using UnityEngine.UI;
using static MainGame;

internal class TakerUnitsSystem : RPCGeneralReduction, IEcsRunSystem
{
    internal TakerUnitsSystem(ECSmanager eCSmanager): base(eCSmanager)
    {
        _eGM.TakerKingEntityButtonComponent.Button.onClick.AddListener(delegate { GetUnit(_eGM.TakerKingEntityUnitTypeComponent.UnitType); });
        _eGM.TakerPawnEntityButtonComponent.Button.onClick.AddListener(delegate { GetUnit(_eGM.TakerPawnEntityUnitTypeComponent.UnitType); });
        _eGM.TakerRookEntityButtonComponent.Button.onClick.AddListener(delegate { GetUnit(_eGM.TakerRookEntityUnitTypeComponent.UnitType); });
        _eGM.TakerBishopEntityButtonComponent.Button.onClick.AddListener(delegate { GetUnit(_eGM.TakerBishopEntityUnitTypeComponent.UnitType); });
    }

    public void Run()
    {
        if (_eGM.InfoEntityUnitsInfoComponent.IsSettedKingDict[InstanceGame.IsMasterClient]) 
            _eGM.TakerKingEntityButtonComponent.Button.gameObject.SetActive(false);
    }

    private void GetUnit(UnitTypes unitType)
    {
        if (!_eGM.DonerEntityIsActivatedDictionaryComponent.IsActivatedDictionary[InstanceGame.IsMasterClient]) _photonPunRPC.GetUnitToMaster(unitType);
    }
}
