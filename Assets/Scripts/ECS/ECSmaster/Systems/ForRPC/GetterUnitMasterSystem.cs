using Leopotam.Ecs;
using Photon.Pun;
using System;


internal class GetterUnitMasterSystem : RPCMasterSystemReduction, IEcsRunSystem
{
    internal UnitTypes UnitType => _eMM.MasterRPCEntUnitTypeCom.UnitType;
    internal PhotonMessageInfo Info => _eGM.GeneralRPCEntFromInfoCom.FromInfo;

    internal GetterUnitMasterSystem(ECSmanager eCSmanager) : base(eCSmanager)
    {

    }

    public void Run()
    {
        bool isGetted = false;

        switch (UnitType)
        {
            case UnitTypes.None:
                break;


            case UnitTypes.King:

                if (Info.Sender.IsMasterClient)
                    isGetted = _eGM.InfoEntityUnitsInfoComponent.AmountKingDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;

                else isGetted = _eGM.InfoEntityUnitsInfoComponent.AmountKingDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;

                break;


            case UnitTypes.Pawn:

                if (Info.Sender.IsMasterClient)
                {
                    if (_eGM.InfoEntityUnitsInfoComponent.AmountPawnDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.AMOUNT_FOR_TAKE_UNIT)
                    {
                        isGetted = true;
                    }
                    else isGetted = false;
                }
                else
                {
                    if (_eGM.InfoEntityUnitsInfoComponent.AmountPawnDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.AMOUNT_FOR_TAKE_UNIT)
                    {
                        isGetted = true;
                    }
                    else isGetted = false;
                }

                break;


            case UnitTypes.Rook:

                if (Info.Sender.IsMasterClient)
                {
                    if (_eGM.InfoEntityUnitsInfoComponent.AmountRookDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.AMOUNT_FOR_TAKE_UNIT)
                    {
                        isGetted = true;
                    }
                    else isGetted = false;
                }
                else
                {
                    if (_eGM.InfoEntityUnitsInfoComponent.AmountRookDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.AMOUNT_FOR_TAKE_UNIT)
                    {
                        isGetted = true;
                    }
                    else isGetted = false;
                }

                break;


            case UnitTypes.Bishop:

                if (Info.Sender.IsMasterClient)
                    isGetted = _eGM.InfoEntityUnitsInfoComponent.AmountBishopDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;

                else isGetted = _eGM.InfoEntityUnitsInfoComponent.AmountBishopDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;

                break;


            default:
                break;

        }
        _photonPunRPC.GetUnitToGeneral(Info.Sender, isGetted, UnitType);
    }
}
