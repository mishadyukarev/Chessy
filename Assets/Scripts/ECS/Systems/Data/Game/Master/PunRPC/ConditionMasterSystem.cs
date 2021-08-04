using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Game.General.Systems.StartFill;
using Assets.Scripts.ECS.System.Data.Game.General.Cell;
using System;
using static Assets.Scripts.Workers.RpcMasterDataContainer;

internal sealed class ConditionMasterSystem : SystemMasterReduction
{
    public override void Run()
    {
        base.Run();

        var unitType = CellUnitsDataSystem.UnitType(XyCellForCondition);
        var isMasterClient = CellUnitsDataSystem.IsMasterClient(XyCellForCondition);

        switch (NeededConditionType)
        {
            case ConditionUnitTypes.None:
                if (CellUnitsDataSystem.IsConditionType(ConditionUnitTypes.Protected, XyCellForCondition))
                {
                    InitSystem.XyUnitsContitionCom.RemoveUnitInCondition(ConditionUnitTypes.Protected, unitType, isMasterClient, XyCellForCondition);
                }
                else if (CellUnitsDataSystem.IsConditionType(ConditionUnitTypes.Relaxed, XyCellForCondition))
                {
                    InitSystem.XyUnitsContitionCom.RemoveUnitInCondition(ConditionUnitTypes.Relaxed, unitType, isMasterClient, XyCellForCondition);
                }

                InitSystem.XyUnitsContitionCom.AddUnitInCondition(ConditionUnitTypes.None, unitType, isMasterClient, XyCellForCondition);
                CellUnitsDataSystem.ResetConditionType(XyCellForCondition);
                break;

            case ConditionUnitTypes.Protected:
                if (CellUnitsDataSystem.IsConditionType(ConditionUnitTypes.Protected, XyCellForCondition))
                {
                    PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.ClickToTable);

                    CellUnitsDataSystem.ResetConditionType(XyCellForCondition);
                    InitSystem.XyUnitsContitionCom.RemoveUnitInCondition(NeededConditionType, unitType, isMasterClient, XyCellForCondition);
                }

                else if (CellUnitsDataSystem.HaveMaxAmountSteps(XyCellForCondition))
                {
                    if (CellUnitsDataSystem.IsConditionType(ConditionUnitTypes.Relaxed, XyCellForCondition))
                    {
                        PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.ClickToTable);

                        InitSystem.XyUnitsContitionCom.RemoveUnitInCondition(ConditionUnitTypes.Relaxed, unitType, isMasterClient, XyCellForCondition);
                        InitSystem.XyUnitsContitionCom.AddUnitInCondition(ConditionUnitTypes.Protected, unitType, isMasterClient, XyCellForCondition);

                        CellUnitsDataSystem.SetConditionType(NeededConditionType, XyCellForCondition);

                        CellUnitsDataSystem.ResetAmountSteps(XyCellForCondition);
                    }
                    else
                    {
                        PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.ClickToTable);

                        CellUnitsDataSystem.SetConditionType(NeededConditionType, XyCellForCondition);
                        InitSystem.XyUnitsContitionCom.RemoveUnitInCondition(ConditionUnitTypes.None, unitType, isMasterClient, XyCellForCondition);
                        InitSystem.XyUnitsContitionCom.AddUnitInCondition(NeededConditionType, unitType, isMasterClient, XyCellForCondition);

                        CellUnitsDataSystem.ResetAmountSteps(XyCellForCondition);
                    }
                }

                else
                {
                    PhotonPunRPC.MistakeStepsUnitToGeneral(InfoFrom.Sender);
                    PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Mistake);
                }
                break;


            case ConditionUnitTypes.Relaxed:
                if (CellUnitsDataSystem.IsConditionType(ConditionUnitTypes.Relaxed, XyCellForCondition))
                {
                    PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.ClickToTable);
                    CellUnitsDataSystem.ResetConditionType(XyCellForCondition);

                    InitSystem.XyUnitsContitionCom.RemoveUnitInCondition(ConditionUnitTypes.Relaxed, unitType, isMasterClient, XyCellForCondition);
                }

                else if (CellUnitsDataSystem.HaveMaxAmountSteps(XyCellForCondition))
                {
                    if (CellUnitsDataSystem.IsConditionType(ConditionUnitTypes.Protected, XyCellForCondition))
                    {
                        InitSystem.XyUnitsContitionCom.RemoveUnitInCondition(ConditionUnitTypes.Protected, unitType, isMasterClient, XyCellForCondition);
                        InitSystem.XyUnitsContitionCom.AddUnitInCondition(ConditionUnitTypes.Relaxed, unitType, isMasterClient, XyCellForCondition);

                        PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.ClickToTable);
                        CellUnitsDataSystem.SetConditionType(NeededConditionType, XyCellForCondition);
                        CellUnitsDataSystem.ResetAmountSteps(XyCellForCondition);
                    }
                    else
                    {
                        InitSystem.XyUnitsContitionCom.RemoveUnitInCondition(ConditionUnitTypes.None, unitType, isMasterClient, XyCellForCondition);
                        InitSystem.XyUnitsContitionCom.AddUnitInCondition(NeededConditionType, unitType, isMasterClient, XyCellForCondition);

                        PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.ClickToTable);
                        CellUnitsDataSystem.SetConditionType(NeededConditionType, XyCellForCondition);
                        CellUnitsDataSystem.ResetAmountSteps(XyCellForCondition);
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
