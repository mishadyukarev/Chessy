using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;

namespace Scripts.Game
{
    public struct InfoC
    {
       private static Dictionary<MasGenOthTypes, PhotonMessageInfo> _info;

        public InfoC(bool needNew) : this()
        {
            if (needNew)
            {
                _info = new Dictionary<MasGenOthTypes, PhotonMessageInfo>();
                _info.Add(MasGenOthTypes.Master, default);
                _info.Add(MasGenOthTypes.General, default);
                _info.Add(MasGenOthTypes.Other, default);
            }
        }


        public static void AddInfo(MasGenOthTypes mGOType, PhotonMessageInfo info)
        {
            if (_info.ContainsKey(mGOType)) _info[mGOType] = info;
            else throw new System.Exception();
        }

        public static PhotonMessageInfo Info(MasGenOthTypes mGOType)
        {
            if (_info.ContainsKey(mGOType)) return _info[mGOType];
            else throw new System.Exception();
        }
        public static Player Sender(MasGenOthTypes mGOType)
        {
            if (_info.ContainsKey(mGOType)) return _info[mGOType].Sender;
            else throw new System.Exception();
        }

        //public PhotonMessageInfo FromInfo { get; set; }
        //public Player Sender => FromInfo.Sender;
    }
}
