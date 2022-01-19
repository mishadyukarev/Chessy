using ECS;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct CellTrailEs
    {
        static Dictionary<DirectTypes, Entity[]> _trails;
        static Dictionary<PlayerTypes, Entity[]> _trailVisibleEnts;

        public static ref AmountC Health(in DirectTypes dir, in byte idx) => ref _trails[dir][idx].Get<AmountC>();
        public static ref IsVisibleC IsVisible(in PlayerTypes player, in byte idx) => ref _trailVisibleEnts[player][idx].Get<IsVisibleC>();


        public static HashSet<DirectTypes> Keys
        {
            get
            {
                var keys = new HashSet<DirectTypes>();
                foreach (var item in _trails) keys.Add(item.Key);
                return keys;
            }
        }
        public static bool HaveAnyTrail(in byte idx)
        {
            foreach (var item in Keys) if (Health(item, idx).Have) return true;
            return false;
        }


        public CellTrailEs(in EcsWorld gameW)
        {
            _trails = new Dictionary<DirectTypes, Entity[]>();
            _trailVisibleEnts = new Dictionary<PlayerTypes, Entity[]>();

            for (var dir = DirectTypes.None + 1; dir < DirectTypes.End; dir++)
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
                for (var dir = DirectTypes.None + 1; dir < DirectTypes.End; dir++)
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
            if (haveAdultForest) Health(dir, idx).Amount = 7;
            return haveAdultForest;
        }
        public static void SetAllTrail(in byte idx)
        {
            foreach (var item in Keys)
            {
                Health(item, idx).Amount = 7;
            }
        }
        public static void TakeHealth(in byte idx, in DirectTypes dir)
        {
            Health(dir, idx).Amount -= 1;
        }
        public static void ResetAll(in byte idx)
        {
            foreach (var item in Keys)
            {
                Health(item, idx).Amount = 0;
            }
        }
    }
}