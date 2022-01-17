using System.Collections.Generic;
using static Game.Game.CellTrailEs;

namespace Game.Game
{
    public struct TrailCellEC : ICellTrailE
    {
        readonly byte _idx;

        public Dictionary<DirectTypes, int> Health
        {
            get
            {
                var dict_0 = new Dictionary<DirectTypes, int>();

                for (var dir = DirectTypes.First; dir < DirectTypes.End; dir++)
                {
                    dict_0.Add(dir, Trail<AmountC>(_idx, dir).Amount);
                }

                return dict_0;
            }
        }
        public Dictionary<DirectTypes, bool> DictTrail
        {
            get
            {
                var dict = new Dictionary<DirectTypes, bool>();
                foreach (var item in Health) dict[item.Key] = item.Value > 0;
                return dict;
            }
        }
        public bool HaveAnyTrail
        {
            get
            {
                foreach (var item in DictTrail) if (item.Value) return true;
                return false;
            }
        }
        public bool Have(DirectTypes dir) => Health[dir] > 0;


        internal TrailCellEC(in byte idx) => _idx = idx;


        public bool TrySetNewTrail(in DirectTypes dir, in bool haveAdultForest)
        {
            if (haveAdultForest) Trail<AmountC>(_idx, dir).Amount = 7;
            return haveAdultForest;
        }
        public void SetAllTrail()
        {
            foreach (var item in Health)
            {
                Trail<AmountC>(_idx, item.Key).Amount = 7;
            }
        }
        public void TakeHealth(in DirectTypes dir)
        {
            Trail<AmountC>(_idx, dir).Amount -= 1;
        }
        public void ResetAll()
        {
            foreach (var item in Health)
            {
                Trail<AmountC>(_idx, item.Key).Amount = 0;
            }
        }

        public void SyncTrail(in DirectTypes dir, in int hp)
        {
            Trail<AmountC>(_idx, dir).Amount = hp;
        }
    }
}