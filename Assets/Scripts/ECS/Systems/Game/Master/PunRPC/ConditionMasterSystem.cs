using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers.Game.Else;
using Assets.Scripts.Workers.Game.Else.Info.Units;
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
                if (CellUnitsDataWorker.IsConditionType(ConditionUnitTypes.Protected, XyCellForCondition))
                {
                    InfoUnitsContainer.RemoveUnitInCondition(ConditionUnitTypes.Protected, unitType, isMasterClient, XyCellForCondition);
                }
                else if (CellUnitsDataWorker.IsConditionType(ConditionUnitTypes.Relaxed, XyCellForCondition))
                {
                    InfoUnitsContainer.RemoveUnitInCondition(ConditionUnitTypes.Relaxed, unitType, isMasterClient, XyCellForCondition);
                }

                InfoUnitsContainer.AddUnitInCondition(ConditionUnitTypes.None, unitType, isMasterClient, XyCellForCondition);
                CellUnitsDataWorker.ResetConditionType(XyCellForCondition);
                break;

            case ConditionUnitTypes.Protected:
                if (CellUnitsDataWorker.IsConditionType(ConditionUnitTypes.Protected, XyCellForCondition))
                {
                    PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.ClickToTable);

                    CellUnitsDataWorker.ResetConditionType(XyCellForCondition);
                    InfoUnitsContainer.RemoveUnitInCondition(NeededConditionType, unitType, isMasterClient, XyCellForCondition);
                }

                else if (CellUnitsDataWorker.HaveMaxAmountSteps(XyCellForCondition))
                {
                    if (CellUnitsDataWorker.IsConditionType(ConditionUnitTypes.Relaxed, XyCellForCondition))
                    {
                        PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.ClickToTable);

                        InfoUnitsContainer.RemoveUnitInCondition(ConditionUnitTypes.Relaxed, unitType, isMasterClient, XyCellForCondition);
                        InfoUnitsContainer.AddUnitInCondition(ConditionUnitTypes.Protected, unitType, isMasterClient, XyCellForCondition);

                        CellUnitsDataWorker.SetConditionType(NeededConditionType, XyCellForCondition);

                        CellUnitsDataWorker.ResetAmountSteps(XyCellForCondition);
                    }
                    else
                    {
                        PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.ClickToTable);

                        CellUnitsDataWorker.SetConditionType(NeededConditionType, XyCellForCondition);
                        InfoUnitsContainer.RemoveUnitInCondition(ConditionUnitTypes.None, unitType, isMasterClient, XyCellForCondition);
                        InfoUnitsContainer.AddUnitInCondition(NeededConditionType, unitType, isMasterClient, XyCellForCondition);

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
                if (CellUnitsDataWorker.IsConditionType(ConditionUnitTypes.Relaxed, XyCellForCondition))
                {
                    PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.ClickToTable);
                    CellUnitsDataWorker.ResetConditionType(XyCellForCondition);

                    InfoUnitsContainer.RemoveUnitInCondition(ConditionUnitTypes.Relaxed, unitType, isMasterClient, XyCellForCondition);
                }

                else if (CellUnitsDataWorker.HaveMaxAmountSteps(XyCellForCondition))
                {
                    if (CellUnitsDataWorker.IsConditionType(ConditionUnitTypes.Protected, XyCellForCondition))
                    {
                        InfoUnitsContainer.RemoveUnitInCondition(ConditionUnitTypes.Protected, unitType, isMasterClient, XyCellForCondition);
                        InfoUnitsContainer.AddUnitInCondition(ConditionUnitTypes.Relaxed, unitType, isMasterClient, XyCellForCondition);

                        PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.ClickToTable);
                        CellUnitsDataWorker.SetConditionType(NeededConditionType, XyCellForCondition);
                        CellUnitsDataWorker.ResetAmountSteps(XyCellForCondition);
                    }
                    else
                    {
                        InfoUnitsContainer.RemoveUnitInCondition(ConditionUnitTypes.None, unitType, isMasterClient, XyCellForCondition);
                        InfoUnitsContainer.AddUnitInCondition(NeededConditionType, unitType, isMasterClient, XyCellForCondition);

                        PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.ClickToTable);
                        CellUnitsDataWorker.SetConditionType(NeededConditionType, XyCellForCondition);
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
