using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Photon.Pun;
using System;

internal sealed class ProtectRelaxMasterSystem : RPCMasterSystemReduction
{
    private PhotonMessageInfo InfoFrom => _eMM.FromInfoEnt_FromInfoCom.InfoFrom;

    private int[] XyCellForProtectRelax => _eMM.ProtectRelaxEnt_XyCellCom.XyCell;
    private ProtectRelaxTypes NeededProtectRelaxType => _eMM.ProtectRelaxEnt_ProtectRelaxCom.ProtectRelaxType;

    public override void Run()
    {
        base.Run();

        var unitType = _eGM.CellUnitEnt_UnitTypeCom(XyCellForProtectRelax).UnitType;

        switch (NeededProtectRelaxType)
        {
            case ProtectRelaxTypes.None:
                break;

            case ProtectRelaxTypes.Protected:
                if (_eGM.CellUnitEnt_ProtectRelaxCom(XyCellForProtectRelax).IsType(ProtectRelaxTypes.Protected))
                {
                    PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.ClickToTable);
                    _eGM.CellUnitEnt_ProtectRelaxCom(XyCellForProtectRelax).Reset();
                    return;
                }
                break;

            case ProtectRelaxTypes.Relaxed:
                if (_eGM.CellUnitEnt_ProtectRelaxCom(XyCellForProtectRelax).IsType(ProtectRelaxTypes.Relaxed))
                {
                    PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.ClickToTable);
                    _eGM.CellUnitEnt_ProtectRelaxCom(XyCellForProtectRelax).Reset();
                    return;
                }
                break;

            default:
                break;
        }

        if (_eGM.CellUnitEnt_CellUnitCom(XyCellForProtectRelax).HaveMaxSteps(unitType))
        {
            switch (NeededProtectRelaxType)
            {
                case ProtectRelaxTypes.None:
                    throw new Exception();

                case ProtectRelaxTypes.Protected:
                    PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.ClickToTable);
                    _eGM.CellUnitEnt_ProtectRelaxCom(XyCellForProtectRelax).ProtectRelaxType = NeededProtectRelaxType;
                    _eGM.CellUnitEnt_CellUnitCom(XyCellForProtectRelax).ResetAmountSteps();
                    break;

                case ProtectRelaxTypes.Relaxed:
                    PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.ClickToTable);
                    _eGM.CellUnitEnt_ProtectRelaxCom(XyCellForProtectRelax).ProtectRelaxType = NeededProtectRelaxType;
                    _eGM.CellUnitEnt_CellUnitCom(XyCellForProtectRelax).ResetAmountSteps();
                    break;

                default:
                    throw new Exception();
            }
        }

        else
        {
            PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Mistake);
        }
    }
}
