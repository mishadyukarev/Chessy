using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers.Info;
using System;
using static Assets.Scripts.Workers.RpcWorker;

internal sealed class ProtectRelaxMasterSystem : SystemMasterReduction
{
    public override void Run()
    {
        base.Run();

        var unitType = CellUnitsDataWorker.UnitType(XyCellForProtectRelax);
        var isMasterClient = CellUnitsDataWorker.IsMasterClient(XyCellForProtectRelax);

        switch (NeededProtectRelaxType)
        {
            case ConditionTypes.None:
                if (CellUnitsDataWorker.IsProtectRelaxType(ConditionTypes.Protected, XyCellForProtectRelax))
                {
                    InfoUnitsWorker.TakeUnitInStandartCondition(ConditionTypes.Protected, unitType, isMasterClient, XyCellForProtectRelax);
                }
                else if (CellUnitsDataWorker.IsProtectRelaxType(ConditionTypes.Relaxed, XyCellForProtectRelax))
                {
                    InfoUnitsWorker.TakeUnitInStandartCondition(ConditionTypes.Relaxed, unitType, isMasterClient, XyCellForProtectRelax);
                }

                InfoUnitsWorker.AddUnitInStandartCondition(ConditionTypes.None, unitType, isMasterClient, XyCellForProtectRelax);
                CellUnitsDataWorker.ResetProtectedRelaxType(XyCellForProtectRelax);
                break;

            case ConditionTypes.Protected:
                if (CellUnitsDataWorker.IsProtectRelaxType(ConditionTypes.Protected, XyCellForProtectRelax))
                {
                    PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.ClickToTable);

                    CellUnitsDataWorker.ResetProtectedRelaxType(XyCellForProtectRelax);
                    InfoUnitsWorker.TakeUnitInStandartCondition(NeededProtectRelaxType, unitType, isMasterClient, XyCellForProtectRelax);
                }

                else if (CellUnitsDataWorker.HaveMaxAmountSteps(XyCellForProtectRelax))
                {
                    if (CellUnitsDataWorker.IsProtectRelaxType(ConditionTypes.Relaxed, XyCellForProtectRelax))
                    {
                        PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.ClickToTable);

                        InfoUnitsWorker.TakeUnitInStandartCondition(ConditionTypes.Relaxed, unitType, isMasterClient, XyCellForProtectRelax);
                        InfoUnitsWorker.AddUnitInStandartCondition(ConditionTypes.Protected, unitType, isMasterClient, XyCellForProtectRelax);

                        CellUnitsDataWorker.SetProtectRelaxType(NeededProtectRelaxType, XyCellForProtectRelax);

                        CellUnitsDataWorker.ResetAmountSteps(XyCellForProtectRelax);
                    }
                    else
                    {
                        PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.ClickToTable);

                        CellUnitsDataWorker.SetProtectRelaxType(NeededProtectRelaxType, XyCellForProtectRelax);
                        InfoUnitsWorker.TakeUnitInStandartCondition(ConditionTypes.None, unitType, isMasterClient, XyCellForProtectRelax);
                        InfoUnitsWorker.AddUnitInStandartCondition(NeededProtectRelaxType, unitType, isMasterClient, XyCellForProtectRelax);

                        CellUnitsDataWorker.ResetAmountSteps(XyCellForProtectRelax);
                    }
                }

                else
                {
                    PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Mistake);
                }
                break;


            case ConditionTypes.Relaxed:
                if (CellUnitsDataWorker.IsProtectRelaxType(ConditionTypes.Relaxed, XyCellForProtectRelax))
                {
                    PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.ClickToTable);
                    CellUnitsDataWorker.ResetProtectedRelaxType(XyCellForProtectRelax);

                    InfoUnitsWorker.TakeUnitInStandartCondition(ConditionTypes.Relaxed, UnitTypes.Pawn, CellUnitsDataWorker.IsMasterClient(XyCellForProtectRelax), XyCellForProtectRelax);
                }

                else if (CellUnitsDataWorker.HaveMaxAmountSteps(XyCellForProtectRelax))
                {
                    if (CellUnitsDataWorker.IsProtectRelaxType(ConditionTypes.Protected, XyCellForProtectRelax))
                    {
                        InfoUnitsWorker.TakeUnitInStandartCondition(ConditionTypes.Protected, unitType, isMasterClient, XyCellForProtectRelax);
                        InfoUnitsWorker.AddUnitInStandartCondition(ConditionTypes.Relaxed, unitType, isMasterClient, XyCellForProtectRelax);

                        PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.ClickToTable);
                        CellUnitsDataWorker.SetProtectRelaxType(NeededProtectRelaxType, XyCellForProtectRelax);
                        CellUnitsDataWorker.ResetAmountSteps(XyCellForProtectRelax);
                    }
                    else
                    {
                        InfoUnitsWorker.TakeUnitInStandartCondition(ConditionTypes.None, unitType, isMasterClient, XyCellForProtectRelax);
                        InfoUnitsWorker.AddUnitInStandartCondition(NeededProtectRelaxType, unitType, CellUnitsDataWorker.IsMasterClient(XyCellForProtectRelax), XyCellForProtectRelax);

                        PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.ClickToTable);
                        CellUnitsDataWorker.SetProtectRelaxType(NeededProtectRelaxType, XyCellForProtectRelax);
                        CellUnitsDataWorker.ResetAmountSteps(XyCellForProtectRelax);
                    }
                }

                else
                {
                    PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Mistake);
                }
                break;


            default:
                throw new Exception();
        }
    }
}
