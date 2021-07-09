using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Photon.Pun;
using System;

internal sealed class ProtectRelaxMasterSystem : RPCMasterSystemReduction
{
    private PhotonMessageInfo Info => _eGM.RpcGeneralEnt_RPCCom.FromInfo;
    private int[] XyCell => _eMM.RPCMasterEnt_RPCMasterCom.XyCell;
    private ProtectRelaxTypes ProtectRelaxType => _eMM.ProtectRelaxEnt_ProtectRelaxCom.ProtectRelaxType;

    public override void Run()
    {
        base.Run();

        var unitType = _eGM.CellUnitEnt_UnitTypeCom(XyCell).UnitType;

        switch (ProtectRelaxType)
        {
            case ProtectRelaxTypes.None:
                break;

            case ProtectRelaxTypes.Protected:
                if (_eGM.CellUnitEnt_ProtectRelaxCom(XyCell).IsProtected)
                {
                    _photonPunRPC.SoundToGeneral(Info.Sender, SoundEffectTypes.ClickToTable);
                    _eGM.CellUnitEnt_ProtectRelaxCom(XyCell).ResetProtectedRelaxedType();
                    return;
                }
                break;

            case ProtectRelaxTypes.Relaxed:
                if (_eGM.CellUnitEnt_ProtectRelaxCom(XyCell).IsRelaxed)
                {
                    _photonPunRPC.SoundToGeneral(Info.Sender, SoundEffectTypes.ClickToTable);
                    _eGM.CellUnitEnt_ProtectRelaxCom(XyCell).ResetProtectedRelaxedType();
                    return;
                }
                break;

            default:
                break;
        }

        if (_eGM.CellUnitEnt_CellUnitCom(XyCell).HaveMaxSteps(unitType))
        {
            switch (ProtectRelaxType)
            {
                case ProtectRelaxTypes.None:
                    throw new Exception();

                case ProtectRelaxTypes.Protected:
                    _photonPunRPC.SoundToGeneral(Info.Sender, SoundEffectTypes.ClickToTable);
                    _eGM.CellUnitEnt_ProtectRelaxCom(XyCell).SetProtectedRelaxedType(ProtectRelaxType);
                    _eGM.CellUnitEnt_CellUnitCom(XyCell).ResetAmountSteps();
                    break;

                case ProtectRelaxTypes.Relaxed:
                    _photonPunRPC.SoundToGeneral(Info.Sender, SoundEffectTypes.ClickToTable);
                    _eGM.CellUnitEnt_ProtectRelaxCom(XyCell).SetProtectedRelaxedType(ProtectRelaxType);
                    _eGM.CellUnitEnt_CellUnitCom(XyCell).ResetAmountSteps();
                    break;

                default:
                    throw new Exception();
            }
        }

        else
        {
            _photonPunRPC.SoundToGeneral(Info.Sender, SoundEffectTypes.Mistake);
        }
    }
}
