using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers.Info;
using System;
using static Assets.Scripts.Workers.RpcWorker;

internal sealed class ConditionMasterSystem : SystemMasterReduction
{
    public override void Run()
    {
        base.Run();

        var unitType = CellUnitsDataWorker.UnitType(XyCellForCondition);
        var isMasterClient = CellUnitsDataWorker.IsMasterClient(XyCellForCondition);

        switch (NeededConditionType)
        {
            case ConditionUnitTypes.None:
                if (CellUnitsDataWorker.IsProtectRelaxType(ConditionUnitTypes.Protected, XyCellForCondition))
                {
                    InfoUnitsConditionWorker.RemoveUnitInCondition(ConditionUnitTypes.Protected, unitType, isMasterClient, XyCellForCondition);
                }
                else if (CellUnitsDataWorker.IsProtectRelaxType(ConditionUnitTypes.Relaxed, XyCellForCondition))
                {
                    InfoUnitsConditionWorker.RemoveUnitInCondition(ConditionUnitTypes.Relaxed, unitType, isMasterClient, XyCellForCondition);
                }

                InfoUnitsConditionWorker.AddUnitInCondition(ConditionUnitTypes.None, unitType, isMasterClient, XyCellForCondition);
                CellUnitsDataWorker.ResetConditionType(XyCellForCondition);
                break;

            case ConditionUnitTypes.Protected:
                if (CellUnitsDataWorker.IsProtectRelaxType(ConditionUnitTypes.Protected, XyCellForCondition))
                {
                    PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.ClickToTable);

                    CellUnitsDataWorker.ResetConditionType(XyCellForCondition);
                    InfoUnitsConditionWorker.RemoveUnitInCondition(NeededConditionType, unitType, isMasterClient, XyCellForCondition);
                }

                else if (CellUnitsDataWorker.HaveMaxAmountSteps(XyCellForCondition))
                {
                    if (CellUnitsDataWorker.IsProtectRelaxType(ConditionUnitTypes.Relaxed, XyCellForCondition))
                    {
                        PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.ClickToTable);

                        InfoUnitsConditionWorker.RemoveUnitInCondition(ConditionUnitTypes.Relaxed, unitType, isMasterClient, XyCellForCondition);
                        InfoUnitsConditionWorker.AddUnitInCondition(ConditionUnitTypes.Protected, unitType, isMasterClient, XyCellForCondition);

                        CellUnitsDataWorker.SetProtectRelaxType(NeededConditionType, XyCellForCondition);

                        CellUnitsDataWorker.ResetAmountSteps(XyCellForCondition);
                    }
                    else
                    {
                        PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.ClickToTable);

                        CellUnitsDataWorker.SetProtectRelaxType(NeededConditionType, XyCellForCondition);
                        InfoUnitsConditionWorker.RemoveUnitInCondition(ConditionUnitTypes.None, unitType, isMasterClient, XyCellForCondition);
                        InfoUnitsConditionWorker.AddUnitInCondition(NeededConditionType, unitType, isMasterClient, XyCellForCondition);

                        CellUnitsDataWorker.ResetAmountSteps(XyCellForCondition);
                    }
                }

                else
                {
                    PhotonPunRPC.MistakeStepsUnitToGeneral(InfoFrom.Sender);
                    PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Mistake);
                }
                break;


            case ConditionUnitTypes.Relaxed:
                if (CellUnitsDataWorker.IsProtectRelaxType(ConditionUnitTypes.Relaxed, XyCellForCondition))
                {
                    PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.ClickToTable);
                    CellUnitsDataWorker.ResetConditionType(XyCellForCondition);

                    InfoUnitsConditionWorker.RemoveUnitInCondition(ConditionUnitTypes.Relaxed, unitType, isMasterClient, XyCellForCondition);
                }

                else if (CellUnitsDataWorker.HaveMaxAmountSteps(XyCellForCondition))
                {
                    if (CellUnitsDataWorker.IsProtectRelaxType(ConditionUnitTypes.Protected, XyCellForCondition))
                    {
                        InfoUnitsConditionWorker.RemoveUnitInCondition(ConditionUnitTypes.Protected, unitType, isMasterClient, XyCellForCondition);
                        InfoUnitsConditionWorker.AddUnitInCondition(ConditionUnitTypes.Relaxed, unitType, isMasterClient, XyCellForCondition);

                        PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.ClickToTable);
                        CellUnitsDataWorker.SetProtectRelaxType(NeededConditionType, XyCellForCondition);
                        CellUnitsDataWorker.ResetAmountSteps(XyCellForCondition);
                    }
                    else
                    {
                        InfoUnitsConditionWorker.RemoveUnitInCondition(ConditionUnitTypes.None, unitType, isMasterClient, XyCellForCondition);
                        InfoUnitsConditionWorker.AddUnitInCondition(NeededConditionType, unitType, isMasterClient, XyCellForCondition);

                        PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.ClickToTable);
                        CellUnitsDataWorker.SetProtectRelaxType(NeededConditionType, XyCellForCondition);
                        CellUnitsDataWorker.ResetAmountSteps(XyCellForCondition);
                    }
                }

                else
                {
                    PhotonPunRPC.MistakeStepsUnitToGeneral(InfoFrom.Sender);
                    PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Mistake);
                }
                break;


            default:
                throw new Exception();
        }
    }
}
