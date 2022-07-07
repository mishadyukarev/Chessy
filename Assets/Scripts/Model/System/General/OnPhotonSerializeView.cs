using Chessy.Model.Values;
using Photon.Pun;

namespace Chessy.Model.System
{
    public partial class SystemsModel : IUpdate
    {
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(_e.IsStartedGame);
                stream.SendNext(_e.WinnerPlayerT);
                stream.SendNext(_e.WinnerPlayerT);
                stream.SendNext(_e.DirectWindT);
                stream.SendNext(_e.SpeedWind);
                stream.SendNext(_e.CenterCloudCellIdx);
                stream.SendNext(_e.SunSideT);

                for (var playerT = (PlayerTypes)1; playerT < PlayerTypes.End; playerT++)
                {
                    stream.SendNext(_e.PlayerInfoC(playerT).IsReadyForStartOnlineGame);
                    stream.SendNext(_e.PlayerInfoC(playerT).WoodForBuyHouse);
                    stream.SendNext(_e.BuildingsInTownInfoC(playerT).HaveBuildingsClone);
                    stream.SendNext(_e.PlayerInfoC(playerT).HaveKingInInventor);

                    stream.SendNext(_e.PawnPeopleInfoC(playerT).PeopleInCity);
                    stream.SendNext(_e.PawnPeopleInfoC(playerT).AmountInGame);

                    stream.SendNext(_e.GodInfoC(playerT).HaveGodInInventor);
                    stream.SendNext(_e.GodInfoC(playerT).UnitT);
                    stream.SendNext(_e.GodInfoC(playerT).CooldownInSecondsForNextAppearance);

                    for (var levelT = (LevelTypes)1; levelT < LevelTypes.End; levelT++)
                    {
                        for (var twT = (ToolsWeaponsWarriorTypes)1; twT < ToolsWeaponsWarriorTypes.End; twT++)
                        {
                            stream.SendNext(_e.ToolWeaponsInInventor(playerT, levelT, twT));
                        }
                    }

                    for (var resT = (ResourceTypes)1; resT < ResourceTypes.End; resT++)
                    {
                        stream.SendNext(_e.ResourcesInInventory(playerT, resT));
                    }
                }
            }
            else
            {
                _e.IsStartedGame = (bool)stream.ReceiveNext();
                _e.WinnerPlayerT = (PlayerTypes)stream.ReceiveNext();
                _e.WinnerPlayerT = (PlayerTypes)stream.ReceiveNext();
                _e.DirectWindT = (DirectTypes)stream.ReceiveNext();
                _e.SpeedWind = (byte)stream.ReceiveNext();
                _e.CenterCloudCellIdx = (byte)stream.ReceiveNext();
                _e.SunSideT = (SunSideTypes)stream.ReceiveNext();

                for (var playerT = (PlayerTypes)1; playerT < PlayerTypes.End; playerT++)
                {
                    _e.PlayerInfoC(playerT).IsReadyForStartOnlineGame = (bool)stream.ReceiveNext();
                    _e.PlayerInfoC(playerT).WoodForBuyHouse = (float)stream.ReceiveNext();
                    _e.BuildingsInTownInfoC(playerT).Sync((bool[])stream.ReceiveNext());
                    _e.PlayerInfoC(playerT).HaveKingInInventor = (bool)stream.ReceiveNext();

                    _e.PawnPeopleInfoC(playerT).PeopleInCity = (int)stream.ReceiveNext();
                    _e.PawnPeopleInfoC(playerT).AmountInGame = (int)stream.ReceiveNext();

                    _e.GodInfoC(playerT).HaveGodInInventor = (bool)stream.ReceiveNext();
                    _e.GodInfoC(playerT).UnitT = (UnitTypes)stream.ReceiveNext();
                    _e.GodInfoC(playerT).CooldownInSecondsForNextAppearance = (int)stream.ReceiveNext();

                    for (var levelT = (LevelTypes)1; levelT < LevelTypes.End; levelT++)
                    {
                        for (var twT = (ToolsWeaponsWarriorTypes)1; twT < ToolsWeaponsWarriorTypes.End; twT++)
                        {
                            _e.SetToolWeaponsInInventor(playerT, levelT, twT, (int)stream.ReceiveNext());
                        }
                    }

                    for (var resT = (ResourceTypes)1; resT < ResourceTypes.End; resT++)
                    {
                        _e.SetResourcesInInventory(playerT, resT, (float)stream.ReceiveNext());
                    }
                }


                //GetDataCellsS.GetDataCellsM();
            }
        }
    }
}