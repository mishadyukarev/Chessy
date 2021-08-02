using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers.Game.Else.Info.Units;
using System;
using static Assets.Scripts.Workers.RpcMasterDataContainer;

internal sealed class ConditionMasterSystem : SystemMasterReduction
{
    public override void Run()
    {
        base.Run();

        var unitType = CellUnitsDataContainer.UnitType(XyCellForCondition);
        var isMasterClient = CellUnitsDataContainer.IsMasterClient(XyCellForCondition);

        switch (NeededConditionType)
        {
            case ConditionUnitTypes.None:
                if (CellUnitsDataContainer.IsConditionType(ConditionUnitTypes.Protected, XyCellForCondition))
                {
                    InfoUnitsDataContainer.RemoveUnitInCondition(ConditionUnitTypes.Protected, unitType, isMasterClient, XyCellForCondition);
                }
                else if (CellUnitsDataContainer.IsConditionType(ConditionUnitTypes.Relaxed, XyCellForCondition))
                {
                    InfoUnitsDataContainer.RemoveUnitInCondition(ConditionUnitTypes.Relaxed, unitType, isMasterClient, XyCellForCondition);
                }

                InfoUnitsDataContainer.AddUnitInCondition(ConditionUnitTypes.None, unitType, isMasterClient, XyCellForCondition);
                CellUnitsDataContainer.ResetConditionType(XyCellForCondition);
                break;

            case ConditionUnitTypes.Protected:
                if (CellUnitsDataContainer.IsConditionType(ConditionUnitTypes.Protected, XyCellForCondition))
                {
                    PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.ClickToTable);

                    CellUnitsDataContainer.ResetConditionType(XyCellForCondition);
                    InfoUnitsDataContainer.RemoveUnitInCondition(NeededConditionType, unitType, isMasterClient, XyCellForCondition);
                }

                else if (CellUnitsDataContainer.HaveMaxAmountSteps(XyCellForCondition))
                {
                    if (CellUnitsDataContainer.IsConditionType(ConditionUnitTypes.Relaxed, XyCellForCondition))
                    {
                        PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.ClickToTable);

                        InfoUnitsDataContainer.RemoveUnitInCondition(ConditionUnitTypes.Relaxed, unitType, isMasterClient, XyCellForCondition);
                        InfoUnitsDataContainer.AddUnitInCondition(ConditionUnitTypes.Protected, unitType, isMasterClient, XyCellForCondition);

                        CellUnitsDataContainer.SetConditionType(NeededConditionType, XyCellForCondition);

                        CellUnitsDataContainer.ResetAmountSteps(XyCellForCondition);
                    }
                    else
                    {
                        PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.ClickToTable);

                        CellUnitsDataContainer.SetConditionType(NeededConditionType, XyCellForCondition);
                        InfoUnitsDataContainer.RemoveUnitInCondition(ConditionUnitTypes.None, unitType, isMasterClient, XyCellForCondition);
                        InfoUnitsDataContainer.AddUnitInCondition(NeededConditionType, unitType, isMasterClient, XyCellForCondition);

                        CellUnitsDataContainer.ResetAmountSteps(XyCellForCondition);
                    }
                }

                else
                {
                    PhotonPunRPC.MistakeStepsUnitToGeneral(InfoFrom.Sender);
                    PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Mistake);
                }
                break;


            case ConditionUnitTypes.Relaxed:
                if (CellUnitsDataContainer.IsConditionType(ConditionUnitTypes.Relaxed, XyCellForCondition))
                {
                    PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.ClickToTable);
                    CellUnitsDataContainer.ResetConditionType(XyCellForCondition);

                    InfoUnitsDataContainer.RemoveUnitInCondition(ConditionUnitTypes.Relaxed, unitType, isMasterClient, XyCellForCondition);
                }

                else if (CellUnitsDataContainer.HaveMaxAmountSteps(XyCellForCondition))
                {
                    if (CellUnitsDataContainer.IsConditionType(ConditionUnitTypes.Protected, XyCellForCondition))
                    {
                        InfoUnitsDataContainer.RemoveUnitInCondition(ConditionUnitTypes.Protected, unitType, isMasterClient, XyCellForCondition);
                        InfoUnitsDataContainer.AddUnitInCondition(ConditionUnitTypes.Relaxed, unitType, isMasterClient, XyCellForCondition);

                        PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.ClickToTable);
                        CellUnitsDataContainer.SetConditionType(NeededConditionType, XyCellForCondition);
                        CellUnitsDataContainer.ResetAmountSteps(XyCellForCondition);
                    }
                    else
                    {
                        InfoUnitsDataContainer.RemoveUnitInCondition(ConditionUnitTypes.None, unitType, isMasterClient, XyCellForCondition);
                        InfoUnitsDataContainer.AddUnitInCondition(NeededConditionType, unitType, isMasterClient, XyCellForCondition);

                        PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.ClickToTable);
                        CellUnitsDataContainer.SetConditionType(NeededConditionType, XyCellForCondition);
                        CellUnitsDataContainer.ResetAmountSteps(XyCellForCondition);
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
