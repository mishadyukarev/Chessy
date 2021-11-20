using System;
using System.Collections.Generic;

namespace Game.Game
{
    public struct WindC
    {
        private static DirectTypes _curDirWind;
        private static Dictionary<DirectTypes, byte> _directs;

        public static DirectTypes CurDirWind => _curDirWind;
        public static Dictionary<DirectTypes, byte> Directs
        {
            get
            {
                var dir = new Dictionary<DirectTypes, byte>();
                foreach (var item in _directs) dir.Add(item.Key, item.Value);
                return dir;
            }
        }
        public static bool Have(byte idx)
        {
            return _directs.ContainsValue(idx);
        }

        static WindC()
        {
            _directs = new Dictionary<DirectTypes, byte>();
            for (var dir = DirectTypes.First; dir < DirectTypes.End; dir++)
            {
                _directs.Add(dir, 0);
            }
        }

        public WindC(DirectTypes dir)
        {
            _curDirWind = dir;

            for (var i = 0; i < _directs.Keys.Count; i++)
            {
                _directs[(DirectTypes)i] = 0;
            }
        }


        public static void Set(DirectTypes dir)
        {
            _curDirWind = dir;
        }
        public static void Set(DirectTypes dir, byte idx)
        {
            if (!_directs.ContainsKey(dir)) throw new Exception();

            _directs[dir] = idx;
        }
        public static void Set(byte idx)
        {
            if (!_directs.ContainsValue(idx)) throw new Exception();

            foreach (var item in _directs)
            {
                if(item.Value == idx)
                {
                    _curDirWind = item.Key;
                }
            }
        }


        public static void Sync(DirectTypes dir)
        {
            _curDirWind = dir;
        }
        public static void Sync(DirectTypes dir, byte idx)
        {
            _directs[dir] = idx;
        }
    }
}