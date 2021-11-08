using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;

namespace Chessy.Game
{
    public struct InfoC
    {
        private static Dictionary<MGOTypes, PhotonMessageInfo> _info;

        static InfoC()
        {
            _info = new Dictionary<MGOTypes, PhotonMessageInfo>();
            _info.Add(MGOTypes.Master, default);
            _info.Add(MGOTypes.General, default);
            _info.Add(MGOTypes.Other, default);
        }


        public static void AddInfo(MGOTypes mGOType, PhotonMessageInfo info)
        {
            if (_info.ContainsKey(mGOType)) _info[mGOType] = info;
            else throw new System.Exception();
        }

        public static PhotonMessageInfo Info(MGOTypes mGOType)
        {
            if (_info.ContainsKey(mGOType)) return _info[mGOType];
            else throw new System.Exception();
        }
        public static Player Sender(MGOTypes mGOType)
        {
            if (_info.ContainsKey(mGOType)) return _info[mGOType].Sender;
            else throw new System.Exception();
        }
    }
}
