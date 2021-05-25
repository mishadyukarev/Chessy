using Leopotam.Ecs;
using static MainGame;

internal sealed class UpdateMotionMasterSystem : SystemMasterReduction
{
    internal UpdateMotionMasterSystem(ECSmanager eCSmanager) : base(eCSmanager) { }

    public override void Run()
    {
        base.Run();


        _sMM.TryInvokeRunSystem(nameof(FireUpdatorMasterSystem), _sMM.RPCSystems);
        _sMM.TryInvokeRunSystem(nameof(EconomyUpdatorMasterSystem), _sMM.RPCSystems);
        _sMM.TryInvokeRunSystem(nameof(EnvironmentUpdatorMasterSystem), _sMM.RPCSystems);

        for (int x = 0; x < _eGM.Xamount; x++)
        {
            for (int y = 0; y < _eGM.Yamount; y++)
            {
                _eGM.CellEnt_CellUnitCom(x, y).RefreshAmountSteps();

                if (_eGM.CellEnt_CellUnitCom(x, y).IsRelaxed)
                {
                    switch (_eGM.CellEnt_CellUnitCom(x, y).UnitType)
                    {
                        case UnitTypes.King:
                            _eGM.CellEnt_CellUnitCom(x, y).AmountHealth += StartValuesGameConfig.HEALTH_FOR_ADDING_KING;
                            if (_eGM.CellEnt_CellUnitCom(x, y).AmountHealth > StartValuesGameConfig.AMOUNT_HEALTH_KING)
                                _eGM.CellEnt_CellUnitCom(x, y).AmountHealth = StartValuesGameConfig.AMOUNT_HEALTH_KING;
                            break;

                        case UnitTypes.Pawn:
                            _eGM.CellEnt_CellUnitCom(x, y).AmountHealth += StartValuesGameConfig.HEALTH_FOR_ADDING_PAWN;
                            if (_eGM.CellEnt_CellUnitCom(x, y).AmountHealth > _eGM.CellEnt_CellUnitCom(x, y).MaxAmountHealth)
                                _eGM.CellEnt_CellUnitCom(x, y).AmountHealth = _eGM.CellEnt_CellUnitCom(x, y).MaxAmountHealth;
                            break;

                        case UnitTypes.Rook:
                            _eGM.CellEnt_CellUnitCom(x, y).AmountHealth += StartValuesGameConfig.HEALTH_FOR_ADDING_ROOK;
                            if (_eGM.CellEnt_CellUnitCom(x, y).AmountHealth > _eGM.CellEnt_CellUnitCom(x, y).MaxAmountHealth)
                                _eGM.CellEnt_CellUnitCom(x, y).AmountHealth = _eGM.CellEnt_CellUnitCom(x, y).MaxAmountHealth;
                            break;

                        case UnitTypes.Bishop:
                            _eGM.CellEnt_CellUnitCom(x, y).AmountHealth += StartValuesGameConfig.HEALTH_FOR_ADDING_BISHOP;
                            if (_eGM.CellEnt_CellUnitCom(x, y).AmountHealth > _eGM.CellEnt_CellUnitCom(x, y).MaxAmountHealth)
                                _eGM.CellEnt_CellUnitCom(x, y).AmountHealth = _eGM.CellEnt_CellUnitCom(x, y).MaxAmountHealth;
                            break;

                        default:
                            break;
                    }
                }
            }
        }

        _eGM.DonerEntityIsActivatedDictionaryComponent.IsActivatedDictionary[true] = false;
        _eGM.DonerEntityIsActivatedDictionaryComponent.IsActivatedDictionary[false] = false;

        _eGM.UpdatorEntityAmountComponent.Amount += 1;
    }
}
