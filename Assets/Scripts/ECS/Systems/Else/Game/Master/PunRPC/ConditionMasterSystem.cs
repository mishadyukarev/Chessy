using Leopotam.Ecs;

internal sealed class ConditionMasterSystem : IEcsInitSystem, IEcsRunSystem
{
    //private EcsWorld _currentGameWorld;
    //private EcsFilter<InfoMasCom> _infoFilter;
    //private EcsFilter<ConditionMasCom, XyCellForDoingMasCom> _conditionFilter;

    //private Player Sender => _infoFilter.Get1(0).FromInfo.Sender;
    //private ConditionUnitTypes NeededConditionUnitType => _conditionFilter.Get1(0).NeededConditionUnitType;
    //private int[] XyCellForCondition => _conditionFilter.Get2(0).XyCellForDoing;


    public void Init()
    {
        //_currentGameWorld.NewEntity()
        //    .Replace(new ConditionMasCom())
        //    .Replace(new XyCellForDoingMasCom(new int[2]));
    }

    public void Run()
    {
        //var unitType = CellUnitsDataSystem.UnitType(XyCellForCondition);
        //var isMasterClient = CellUnitsDataSystem.IsMasterClient(XyCellForCondition);

        //switch (NeededConditionUnitType)
        //{
        //    case ConditionUnitTypes.None:
        //        if (CellUnitsDataSystem.IsConditionType(ConditionUnitTypes.Protected, XyCellForCondition))
        //        {
        //            MainGameSystem.XyUnitsContitionCom.RemoveUnitInCondition(ConditionUnitTypes.Protected, unitType, isMasterClient, XyCellForCondition);
        //        }
        //        else if (CellUnitsDataSystem.IsConditionType(ConditionUnitTypes.Relaxed, XyCellForCondition))
        //        {
        //            MainGameSystem.XyUnitsContitionCom.RemoveUnitInCondition(ConditionUnitTypes.Relaxed, unitType, isMasterClient, XyCellForCondition);
        //        }

        //        MainGameSystem.XyUnitsContitionCom.AddUnitInCondition(ConditionUnitTypes.None, unitType, isMasterClient, XyCellForCondition);
        //        CellUnitsDataSystem.ResetConditionType(XyCellForCondition);
        //        break;

        //    case ConditionUnitTypes.Protected:
        //        if (CellUnitsDataSystem.IsConditionType(ConditionUnitTypes.Protected, XyCellForCondition))
        //        {
        //            RPCGameSystem.SoundToGeneral(Sender, SoundEffectTypes.ClickToTable);

        //            CellUnitsDataSystem.ResetConditionType(XyCellForCondition);
        //            MainGameSystem.XyUnitsContitionCom.RemoveUnitInCondition(NeededConditionUnitType, unitType, isMasterClient, XyCellForCondition);
        //        }

        //        else if (CellUnitsDataSystem.HaveMaxAmountSteps(XyCellForCondition))
        //        {
        //            if (CellUnitsDataSystem.IsConditionType(ConditionUnitTypes.Relaxed, XyCellForCondition))
        //            {
        //                RPCGameSystem.SoundToGeneral(Sender, SoundEffectTypes.ClickToTable);

        //                MainGameSystem.XyUnitsContitionCom.RemoveUnitInCondition(ConditionUnitTypes.Relaxed, unitType, isMasterClient, XyCellForCondition);
        //                MainGameSystem.XyUnitsContitionCom.AddUnitInCondition(ConditionUnitTypes.Protected, unitType, isMasterClient, XyCellForCondition);

        //                CellUnitsDataSystem.SetConditionType(NeededConditionUnitType, XyCellForCondition);

        //                CellUnitsDataSystem.ResetAmountSteps(XyCellForCondition);
        //            }
        //            else
        //            {
        //                RPCGameSystem.SoundToGeneral(Sender, SoundEffectTypes.ClickToTable);

        //                CellUnitsDataSystem.SetConditionType(NeededConditionUnitType, XyCellForCondition);
        //                MainGameSystem.XyUnitsContitionCom.RemoveUnitInCondition(ConditionUnitTypes.None, unitType, isMasterClient, XyCellForCondition);
        //                MainGameSystem.XyUnitsContitionCom.AddUnitInCondition(NeededConditionUnitType, unitType, isMasterClient, XyCellForCondition);

        //                CellUnitsDataSystem.ResetAmountSteps(XyCellForCondition);
        //            }
        //        }

        //        else
        //        {
        //            RPCGameSystem.MistakeStepsUnitToGeneral(Sender);
        //            RPCGameSystem.SoundToGeneral(Sender, SoundEffectTypes.Mistake);
        //        }
        //        break;


        //    case ConditionUnitTypes.Relaxed:
        //        if (CellUnitsDataSystem.IsConditionType(ConditionUnitTypes.Relaxed, XyCellForCondition))
        //        {
        //            RPCGameSystem.SoundToGeneral(Sender, SoundEffectTypes.ClickToTable);
        //            CellUnitsDataSystem.ResetConditionType(XyCellForCondition);

        //            MainGameSystem.XyUnitsContitionCom.RemoveUnitInCondition(ConditionUnitTypes.Relaxed, unitType, isMasterClient, XyCellForCondition);
        //        }

        //        else if (CellUnitsDataSystem.HaveMaxAmountSteps(XyCellForCondition))
        //        {
        //            if (CellUnitsDataSystem.IsConditionType(ConditionUnitTypes.Protected, XyCellForCondition))
        //            {
        //                MainGameSystem.XyUnitsContitionCom.RemoveUnitInCondition(ConditionUnitTypes.Protected, unitType, isMasterClient, XyCellForCondition);
        //                MainGameSystem.XyUnitsContitionCom.AddUnitInCondition(ConditionUnitTypes.Relaxed, unitType, isMasterClient, XyCellForCondition);

        //                RPCGameSystem.SoundToGeneral(Sender, SoundEffectTypes.ClickToTable);
        //                CellUnitsDataSystem.SetConditionType(NeededConditionUnitType, XyCellForCondition);
        //                CellUnitsDataSystem.ResetAmountSteps(XyCellForCondition);
        //            }
        //            else
        //            {
        //                MainGameSystem.XyUnitsContitionCom.RemoveUnitInCondition(ConditionUnitTypes.None, unitType, isMasterClient, XyCellForCondition);
        //                MainGameSystem.XyUnitsContitionCom.AddUnitInCondition(NeededConditionUnitType, unitType, isMasterClient, XyCellForCondition);

        //                RPCGameSystem.SoundToGeneral(Sender, SoundEffectTypes.ClickToTable);
        //                CellUnitsDataSystem.SetConditionType(NeededConditionUnitType, XyCellForCondition);
        //                CellUnitsDataSystem.ResetAmountSteps(XyCellForCondition);
        //            }
        //        }

        //        else
        //        {
        //            RPCGameSystem.MistakeStepsUnitToGeneral(Sender);
        //            RPCGameSystem.SoundToGeneral(Sender, SoundEffectTypes.Mistake);
        //        }
        //        break;


        //    default:
        //        throw new Exception();
        //}
    }
}
