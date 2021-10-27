using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;

namespace Scripts.Game
{
    internal struct InfoC
    {
       private static Dictionary<MasGenOthTypes, PhotonMessageInfo> _info;

        internal InfoC(bool needNew) : this()
        {
            if (needNew)
            {
                _info = new Dictionary<MasGenOthTypes, PhotonMessageInfo>();
                _info.Add(MasGenOthTypes.Master, default);
                _info.Add(MasGenOthTypes.General, default);
                _info.Add(MasGenOthTypes.Other, default);
            }
        }


        internal static void AddInfo(MasGenOthTypes mGOType, PhotonMessageInfo info)
        {
            if (_info.ContainsKey(mGOType)) _info[mGOType] = info;
            else throw new System.Exception();
        }

        internal static PhotonMessageInfo Info(MasGenOthTypes mGOType)
        {
            if (_info.ContainsKey(mGOType)) return _info[mGOType];
            else throw new System.Exception();
        }
        internal static Player Sender(MasGenOthTypes mGOType)
        {
            if (_info.ContainsKey(mGOType)) return _info[mGOType].Sender;
            else throw new System.Exception();
        }

        //internal PhotonMessageInfo FromInfo { get; set; }
        //internal Player Sender => FromInfo.Sender;
    }
}
