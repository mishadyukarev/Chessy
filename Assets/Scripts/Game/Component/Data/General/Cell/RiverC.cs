using System;
using System.Collections.Generic;

namespace Game.Game
{
    public struct RiverC : IElseCell
    {
        private RiverTypes _river;
        private List<DirectTypes> _directs;

        public RiverTypes River => _river;
        public List<DirectTypes> Directs
        {
            get
            {
                var list = new List<DirectTypes>();
                foreach (var item in _directs) list.Add(item);
                return list;
            }
        }
        public Dictionary<DirectTypes, bool> DirectsDict
        {
            get
            {
                var dic = new Dictionary<DirectTypes, bool>();

                if (_directs.Contains(DirectTypes.Up)) dic.Add(DirectTypes.Up, true);
                else dic.Add(DirectTypes.Up, false);

                if (_directs.Contains(DirectTypes.Right)) dic.Add(DirectTypes.Right, true);
                else dic.Add(DirectTypes.Right, false);

                if (_directs.Contains(DirectTypes.Down)) dic.Add(DirectTypes.Down, true);
                else dic.Add(DirectTypes.Down, false);

                if (_directs.Contains(DirectTypes.Left)) dic.Add(DirectTypes.Left, true);
                else dic.Add(DirectTypes.Left, false);

                return dic;
            }
        }
        public bool HaveNearRiver => River != default;
        public bool Is(params DirectTypes[] dirs)
        {
            foreach (var dir in dirs) if (_directs.Contains(dir)) return true;
            return false;
        }



        public RiverC(bool needNew)
        {
            if (needNew)
            {
                _river = default;
                _directs = new List<DirectTypes>();
            }

            else throw new Exception();
        }

        public void SetStart(params DirectTypes[] dirs)
        {
            if (dirs == default) throw new Exception();

            _river = RiverTypes.Start;
            foreach (var item in dirs) _directs.Add(item);
        }

        public void SetEnd(params DirectTypes[] dirs)
        {
            if (dirs == default) throw new Exception();

            _river = RiverTypes.End;
        }

        public void SetCorner()
        {
            _river = RiverTypes.Corner;
        }


        public void Sync(RiverTypes river)
        {
            _river = river;
        }
        public void Sync(DirectTypes dir, bool isActive)
        {
            if (isActive) _directs.Add(dir);
        }
    }
}