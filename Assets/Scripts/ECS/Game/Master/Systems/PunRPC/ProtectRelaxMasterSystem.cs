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

        var unitType = CellUnitWorker.UnitType(XyCellForProtectRelax);
        var isMasterClient = CellUnitWorker.IsMasterClient(XyCellForProtectRelax);

        switch (NeededProtectRelaxType)
        {
            case ConditionTypes.None:
                if (CellUnitWorker.IsProtectRelaxType(ConditionTypes.Protected, XyCellForProtectRelax))
                {
                    InfoUnitsWorker.TakeUnitInStandartCondition(ConditionTypes.Protected, unitType, isMasterClient, XyCellForProtectRelax);
                }
                else if (CellUnitWorker.IsProtectRelaxType(ConditionTypes.Relaxed, XyCellForProtectRelax))
                {
                    InfoUnitsWorker.TakeUnitInStandartCondition(ConditionTypes.Relaxed, unitType, isMasterClient, XyCellForProtectRelax);
                }

                InfoUnitsWorker.AddUnitInStandartCondition(ConditionTypes.None, unitType, isMasterClient, XyCellForProtectRelax);
                CellUnitWorker.ResetProtectedRelaxType(XyCellForProtectRelax);
                break;

            case ConditionTypes.Protected:
                if (CellUnitWorker.IsProtectRelaxType(ConditionTypes.Protected, XyCellForProtectRelax))
                {
                    PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.ClickToTable);

                    CellUnitWorker.ResetProtectedRelaxType(XyCellForProtectRelax);
                    InfoUnitsWorker.TakeUnitInStandartCondition(NeededProtectRelaxType, unitType, isMasterClient, XyCellForProtectRelax);
                }

                else if (CellUnitWorker.HaveMaxAmountSteps(XyCellForProtectRelax))
                {
                    if (CellUnitWorker.IsProtectRelaxType(ConditionTypes.Relaxed, XyCellForProtectRelax))
                    {
                        PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.ClickToTable);

                        InfoUnitsWorker.TakeUnitInStandartCondition(ConditionTypes.Relaxed, unitType, isMasterClient, XyCellForProtectRelax);
                        InfoUnitsWorker.AddUnitInStandartCondition(ConditionTypes.Protected, unitType, isMasterClient, XyCellForProtectRelax);

                        CellUnitWorker.SetProtectRelaxType(NeededProtectRelaxType, XyCellForProtectRelax);

                        CellUnitWorker.ResetAmountSteps(XyCellForProtectRelax);
                    }
                    else
                    {
                        PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.ClickToTable);

                        CellUnitWorker.SetProtectRelaxType(NeededProtectRelaxType, XyCellForProtectRelax);
                        InfoUnitsWorker.TakeUnitInStandartCondition(ConditionTypes.None, unitType, isMasterClient, XyCellForProtectRelax);
                        InfoUnitsWorker.AddUnitInStandartCondition(NeededProtectRelaxType, unitType, isMasterClient, XyCellForProtectRelax);

                        CellUnitWorker.ResetAmountSteps(XyCellForProtectRelax);
                    }
                }

                else
                {
                    PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Mistake);
                }
                break;


            case ConditionTypes.Relaxed:
                if (CellUnitWorker.IsProtectRelaxType(ConditionTypes.Relaxed, XyCellForProtectRelax))
                {
                    PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.ClickToTable);
                    CellUnitWorker.ResetProtectedRelaxType(XyCellForProtectRelax);

                    InfoUnitsWorker.TakeUnitInStandartCondition(ConditionTypes.Relaxed, UnitTypes.Pawn, CellUnitWorker.IsMasterClient(XyCellForProtectRelax), XyCellForProtectRelax);
                }

                else if (CellUnitWorker.HaveMaxAmountSteps(XyCellForProtectRelax))
                {
                    if (CellUnitWorker.IsProtectRelaxType(ConditionTypes.Protected, XyCellForProtectRelax))
                    {
                        InfoUnitsWorker.TakeUnitInStandartCondition(ConditionTypes.Protected, unitType, isMasterClient, XyCellForProtectRelax);
                        InfoUnitsWorker.AddUnitInStandartCondition(ConditionTypes.Relaxed, unitType, isMasterClient, XyCellForProtectRelax);

                        PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.ClickToTable);
                        CellUnitWorker.SetProtectRelaxType(NeededProtectRelaxType, XyCellForProtectRelax);
                        CellUnitWorker.ResetAmountSteps(XyCellForProtectRelax);
                    }
                    else
                    {
                        InfoUnitsWorker.TakeUnitInStandartCondition(ConditionTypes.None, unitType, isMasterClient, XyCellForProtectRelax);
                        InfoUnitsWorker.AddUnitInStandartCondition(NeededProtectRelaxType, unitType, CellUnitWorker.IsMasterClient(XyCellForProtectRelax), XyCellForProtectRelax);

                        PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.ClickToTable);
                        CellUnitWorker.SetProtectRelaxType(NeededProtectRelaxType, XyCellForProtectRelax);
                        CellUnitWorker.ResetAmountSteps(XyCellForProtectRelax);
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
