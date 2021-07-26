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
            case ConditionUnitTypes.None:
                if (CellUnitsDataWorker.IsProtectRelaxType(ConditionUnitTypes.Protected, XyCellForProtectRelax))
                {
                    InfoUnitsWorker.TakeUnitInStandartCondition(ConditionUnitTypes.Protected, unitType, isMasterClient, XyCellForProtectRelax);
                }
                else if (CellUnitsDataWorker.IsProtectRelaxType(ConditionUnitTypes.Relaxed, XyCellForProtectRelax))
                {
                    InfoUnitsWorker.TakeUnitInStandartCondition(ConditionUnitTypes.Relaxed, unitType, isMasterClient, XyCellForProtectRelax);
                }

                InfoUnitsWorker.AddUnitInStandartCondition(ConditionUnitTypes.None, unitType, isMasterClient, XyCellForProtectRelax);
                CellUnitsDataWorker.ResetProtectedRelaxType(XyCellForProtectRelax);
                break;

            case ConditionUnitTypes.Protected:
                if (CellUnitsDataWorker.IsProtectRelaxType(ConditionUnitTypes.Protected, XyCellForProtectRelax))
                {
                    PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.ClickToTable);

                    CellUnitsDataWorker.ResetProtectedRelaxType(XyCellForProtectRelax);
                    InfoUnitsWorker.TakeUnitInStandartCondition(NeededProtectRelaxType, unitType, isMasterClient, XyCellForProtectRelax);
                }

                else if (CellUnitsDataWorker.HaveMaxAmountSteps(XyCellForProtectRelax))
                {
                    if (CellUnitsDataWorker.IsProtectRelaxType(ConditionUnitTypes.Relaxed, XyCellForProtectRelax))
                    {
                        PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.ClickToTable);

                        InfoUnitsWorker.TakeUnitInStandartCondition(ConditionUnitTypes.Relaxed, unitType, isMasterClient, XyCellForProtectRelax);
                        InfoUnitsWorker.AddUnitInStandartCondition(ConditionUnitTypes.Protected, unitType, isMasterClient, XyCellForProtectRelax);

                        CellUnitsDataWorker.SetProtectRelaxType(NeededProtectRelaxType, XyCellForProtectRelax);

                        CellUnitsDataWorker.ResetAmountSteps(XyCellForProtectRelax);
                    }
                    else
                    {
                        PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.ClickToTable);

                        CellUnitsDataWorker.SetProtectRelaxType(NeededProtectRelaxType, XyCellForProtectRelax);
                        InfoUnitsWorker.TakeUnitInStandartCondition(ConditionUnitTypes.None, unitType, isMasterClient, XyCellForProtectRelax);
                        InfoUnitsWorker.AddUnitInStandartCondition(NeededProtectRelaxType, unitType, isMasterClient, XyCellForProtectRelax);

                        CellUnitsDataWorker.ResetAmountSteps(XyCellForProtectRelax);
                    }
                }

                else
                {
                    PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Mistake);
                }
                break;


            case ConditionUnitTypes.Relaxed:
                if (CellUnitsDataWorker.IsProtectRelaxType(ConditionUnitTypes.Relaxed, XyCellForProtectRelax))
                {
                    PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.ClickToTable);
                    CellUnitsDataWorker.ResetProtectedRelaxType(XyCellForProtectRelax);

                    InfoUnitsWorker.TakeUnitInStandartCondition(ConditionUnitTypes.Relaxed, UnitTypes.Pawn, CellUnitsDataWorker.IsMasterClient(XyCellForProtectRelax), XyCellForProtectRelax);
                }

                else if (CellUnitsDataWorker.HaveMaxAmountSteps(XyCellForProtectRelax))
                {
                    if (CellUnitsDataWorker.IsProtectRelaxType(ConditionUnitTypes.Protected, XyCellForProtectRelax))
                    {
                        InfoUnitsWorker.TakeUnitInStandartCondition(ConditionUnitTypes.Protected, unitType, isMasterClient, XyCellForProtectRelax);
                        InfoUnitsWorker.AddUnitInStandartCondition(ConditionUnitTypes.Relaxed, unitType, isMasterClient, XyCellForProtectRelax);

                        PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.ClickToTable);
                        CellUnitsDataWorker.SetProtectRelaxType(NeededProtectRelaxType, XyCellForProtectRelax);
                        CellUnitsDataWorker.ResetAmountSteps(XyCellForProtectRelax);
                    }
                    else
                    {
                        InfoUnitsWorker.TakeUnitInStandartCondition(ConditionUnitTypes.None, unitType, isMasterClient, XyCellForProtectRelax);
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
