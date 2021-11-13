using System;
using System.Collections.Generic;

namespace Chessy.Game
{
    public struct RiverC
    {
        private Dictionary<DirectTypes, bool> _directs;

        public RiverTypes Type { get; set; }
        public Dictionary<DirectTypes, bool> Directs
        {
            get
            {
                var dic = new Dictionary<DirectTypes, bool>();
                foreach (var item_0 in _directs)
                {
                    dic.Add(item_0.Key, item_0.Value);
                }
                return dic;
            }
        }

        public bool HaveNearRiver => Type != default;

        public RiverC(bool needNew)
        {
            if (needNew)
            {
                Type = default;
                _directs = new Dictionary<DirectTypes, bool>();

                _directs.Add(DirectTypes.Up, false);
                _directs.Add(DirectTypes.Right, false);
                _directs.Add(DirectTypes.Down, false);
                _directs.Add(DirectTypes.Left, false);
            }

            else throw new Exception();
        }

        public void AddDir(DirectTypes dir)
        {
            if (_directs[dir] == true) throw new Exception();
            if (!_directs.ContainsKey(dir)) throw new Exception();
            _directs[dir] = true;
        }

        public void Sync(DirectTypes dir, bool have)
        {
            _directs[dir] = have;
        }
    }
}