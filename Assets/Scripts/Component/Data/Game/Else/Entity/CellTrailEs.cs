using ECS;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct CellTrailEs
    {
        static Dictionary<DirectTypes, Entity[]> _trails;
        static Dictionary<PlayerTypes, Entity[]> _trailVisibleEnts;

        public static ref T Trail<T>(in byte idx, in DirectTypes dir = default) where T : struct, ICellTrailE => ref _trails[dir][idx].Get<T>();
        public static ref T Trail<T>(in PlayerTypes player, in byte idx) where T : struct, ITrailVisibledCellE => ref _trailVisibleEnts[player][idx].Get<T>();


        public static Dictionary<DirectTypes, int> Health(in byte idx)
        {
            var dict_0 = new Dictionary<DirectTypes, int>();

            for (var dir = DirectTypes.First; dir < DirectTypes.End; dir++)
            {
                dict_0.Add(dir, Trail<AmountC>(idx, dir).Amount);
            }

            return dict_0;
        }
        public static Dictionary<DirectTypes, bool> DictTrail(in byte idx)
        {
            var dict = new Dictionary<DirectTypes, bool>();
            foreach (var item in Health(idx)) dict[item.Key] = item.Value > 0;
            return dict;
        }
        public static bool HaveAnyTrail(in byte idx)
        {
            foreach (var item in DictTrail(idx)) if (item.Value) return true;
            return false;
        }
        public static bool Have(in byte idx, in DirectTypes dir) => Health(idx)[dir] > 0;


        public CellTrailEs(in EcsWorld gameW)
        {
            _trails = new Dictionary<DirectTypes, Entity[]>();
            _trailVisibleEnts = new Dictionary<PlayerTypes, Entity[]>();

            for (var dir = DirectTypes.Start; dir < DirectTypes.End; dir++)
            {
                _trails.Add(dir, new Entity[CellStartValues.ALL_CELLS_AMOUNT]);
            }

            for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
            {
                _trailVisibleEnts.Add(player, new Entity[CellStartValues.ALL_CELLS_AMOUNT]);
            }


            byte idx = 0;

            for (idx = 0; idx < CellStartValues.ALL_CELLS_AMOUNT; idx++)
            {
                for (var dir = DirectTypes.Start; dir < DirectTypes.End; dir++)
                {
                    _trails[dir][idx] = gameW.NewEntity()
                    .Add(new AmountC());
                }

                for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
                {
                    _trailVisibleEnts[player][idx] = gameW.NewEntity()
                        .Add(new IsVisibleC());
                }
            }
        }

        public static bool TrySetNewTrail(in byte idx, in DirectTypes dir, in bool haveAdultForest)
        {
            if (haveAdultForest) Trail<AmountC>(idx, dir).Amount = 7;
            return haveAdultForest;
        }
        public static void SetAllTrail(in byte idx)
        {
            foreach (var item in Health(idx))
            {
                Trail<AmountC>(idx, item.Key).Amount = 7;
            }
        }
        public static void TakeHealth(in byte idx, in DirectTypes dir)
        {
            Trail<AmountC>(idx, dir).Amount -= 1;
        }
        public static void ResetAll(in byte idx)
        {
            foreach (var item in Health(idx))
            {
                Trail<AmountC>(idx, item.Key).Amount = 0;
            }
        }
    }
    public interface ICellTrailE { }
}