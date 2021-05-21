using Leopotam.Ecs;


internal class UniquePawnAbilityMasterSystem : SystemMasterReduction, IEcsRunSystem
{
    private UniqueAbilitiesPawnTypes UniqueAbilitiesPawnType => _eMM.RPCMasterEnt_RPCMasterCom.UniqueAbilitiesPawnType;
    private int[] XyCell => _eMM.RPCMasterEnt_RPCMasterCom.XyCell;

    internal UniquePawnAbilityMasterSystem(ECSmanager eCSmanager) : base(eCSmanager)
    {

    }

    public void Run()
    {
        if (_eGM.CellUnitEnt_CellUnitCom(XyCell).HaveMaxSteps)
        {
            if (_eGM.CellEnvEnt_CellEnvironmentCom(XyCell).HaveTree || _eGM.CellEnvEnt_CellEnvironmentCom(XyCell).HaveFood)
            {
                switch (UniqueAbilitiesPawnType)
                {
                    case UniqueAbilitiesPawnTypes.AbilityOne:
                        if (_eGM.CellEffectEnt_CellEffectCom(XyCell).HaveFire)
                            _eGM.CellEffectEnt_CellEffectCom(XyCell).SetEffect(false, EffectTypes.Fire);
                        if (!_eGM.CellEffectEnt_CellEffectCom(XyCell).HaveFire)
                            _eGM.CellEffectEnt_CellEffectCom(XyCell).SetEffect(true, EffectTypes.Fire);
                        _eGM.CellUnitEnt_CellUnitCom(XyCell).AmountSteps = 0;
                        break;

                    case UniqueAbilitiesPawnTypes.AbilityTwo:
                        break;

                    case UniqueAbilitiesPawnTypes.AbilityThree:
                        break;

                    default:
                        break;
                }
            }
        }
    }
}
