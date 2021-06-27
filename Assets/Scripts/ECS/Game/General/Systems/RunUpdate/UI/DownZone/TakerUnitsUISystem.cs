using Assets.Scripts;
using static Assets.Scripts.Main;

internal sealed class TakerUnitsUISystem : RPCGeneralSystemReduction
{
    public override void Run()
    {
        base.Run();

        if (_eGM.UnitInventorEnt_UnitInventorCom.IsSettedKing(Instance.IsMasterClient))
            _eGM.TakerKingEnt_ButtonCom.SetActive(false);
        else _eGM.TakerKingEnt_ButtonCom.SetActive(true);
    }
}
