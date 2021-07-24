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
            case ProtectRelaxTypes.None:
                if (CellUnitWorker.IsUnitProtectRelaxType(ProtectRelaxTypes.Protected, XyCellForProtectRelax))
                {
                    InfoUnitsWorker.TakeUnitInStandartCondition(ProtectRelaxTypes.Protected, unitType, isMasterClient, XyCellForProtectRelax);
                }
                else if (CellUnitWorker.IsUnitProtectRelaxType(ProtectRelaxTypes.Relaxed, XyCellForProtectRelax))
                {
                    InfoUnitsWorker.TakeUnitInStandartCondition(ProtectRelaxTypes.Relaxed, unitType, isMasterClient, XyCellForProtectRelax);
                }

                InfoUnitsWorker.AddUnitInStandartCondition(ProtectRelaxTypes.None, unitType, isMasterClient, XyCellForProtectRelax);
                CellUnitWorker.ResetProtectedRelaxType(XyCellForProtectRelax);
                break;

            case ProtectRelaxTypes.Protected:
                if (CellUnitWorker.IsUnitProtectRelaxType(ProtectRelaxTypes.Protected, XyCellForProtectRelax))
                {
                    PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.ClickToTable);

                    CellUnitWorker.ResetProtectedRelaxType(XyCellForProtectRelax);
                    InfoUnitsWorker.TakeUnitInStandartCondition(NeededProtectRelaxType, unitType, isMasterClient, XyCellForProtectRelax);
                }

                else if (CellUnitWorker.HaveMaxAmountSteps(XyCellForProtectRelax))
                {
                    if (CellUnitWorker.IsUnitProtectRelaxType(ProtectRelaxTypes.Relaxed, XyCellForProtectRelax))
                    {
                        PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.ClickToTable);

                        InfoUnitsWorker.TakeUnitInStandartCondition(ProtectRelaxTypes.Relaxed, unitType, isMasterClient, XyCellForProtectRelax);
                        InfoUnitsWorker.AddUnitInStandartCondition(ProtectRelaxTypes.Protected, unitType, isMasterClient, XyCellForProtectRelax);

                        CellUnitWorker.SetProtectRelaxType(NeededProtectRelaxType, XyCellForProtectRelax);

                        CellUnitWorker.ResetAmountSteps(XyCellForProtectRelax);
                    }
                    else
                    {
                        PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.ClickToTable);

                        CellUnitWorker.SetProtectRelaxType(NeededProtectRelaxType, XyCellForProtectRelax);
                        InfoUnitsWorker.TakeUnitInStandartCondition(ProtectRelaxTypes.None, unitType, isMasterClient, XyCellForProtectRelax);
                        InfoUnitsWorker.AddUnitInStandartCondition(NeededProtectRelaxType, unitType, isMasterClient, XyCellForProtectRelax);

                        CellUnitWorker.ResetAmountSteps(XyCellForProtectRelax);
                    }
                }

                else
                {
                    PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Mistake);
                }
                break;


            case ProtectRelaxTypes.Relaxed:
                if (CellUnitWorker.IsUnitProtectRelaxType(ProtectRelaxTypes.Relaxed, XyCellForProtectRelax))
                {
                    PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.ClickToTable);
                    CellUnitWorker.ResetProtectedRelaxType(XyCellForProtectRelax);

                    InfoUnitsWorker.TakeUnitInStandartCondition(ProtectRelaxTypes.Relaxed, UnitTypes.Pawn, CellUnitWorker.IsMasterClient(XyCellForProtectRelax), XyCellForProtectRelax);
                }

                else if (CellUnitWorker.HaveMaxAmountSteps(XyCellForProtectRelax))
                {
                    if (CellUnitWorker.IsUnitProtectRelaxType(ProtectRelaxTypes.Protected, XyCellForProtectRelax))
                    {
                        InfoUnitsWorker.TakeUnitInStandartCondition(ProtectRelaxTypes.Protected, unitType, isMasterClient, XyCellForProtectRelax);
                        InfoUnitsWorker.AddUnitInStandartCondition(ProtectRelaxTypes.Relaxed, unitType, isMasterClient, XyCellForProtectRelax);

                        PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.ClickToTable);
                        CellUnitWorker.SetProtectRelaxType(NeededProtectRelaxType, XyCellForProtectRelax);
                        CellUnitWorker.ResetAmountSteps(XyCellForProtectRelax);
                    }
                    else
                    {
                        InfoUnitsWorker.TakeUnitInStandartCondition(ProtectRelaxTypes.None, unitType, isMasterClient, XyCellForProtectRelax);
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
