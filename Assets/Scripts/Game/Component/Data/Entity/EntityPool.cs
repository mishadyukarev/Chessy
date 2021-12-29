using Leopotam.Ecs;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct EntityPool
    {
        readonly static Dictionary<string, EcsEntity> _idxs;

        public static ref T SelIdx<T>() where T : struct, ISelectedIdx => ref _idxs[nameof(ISelectedIdx)].Get<T>();
        public static ref T CurIdx<T>() where T : struct, ICurrectIdx => ref _idxs[nameof(ICurrectIdx)].Get<T>();
        public static ref T PreVisIdx<T>() where T : struct, IPreVisionIdx => ref _idxs[nameof(IPreVisionIdx)].Get<T>();


        static EntityPool()
        {
            _idxs = new Dictionary<string, EcsEntity>();
        }

        public EntityPool(in EcsWorld curGameW)
        {
            _idxs[nameof(ISelectedIdx)] = curGameW.NewEntity()
                .Replace(new SelIdxC())
                .Replace(new IdxC(0));

            _idxs[nameof(ICurrectIdx)] = curGameW.NewEntity()
                .Replace(new CurIdxC())
                .Replace(new IdxC(0));

            _idxs[nameof(IPreVisionIdx)] = curGameW.NewEntity()
                .Replace(new PreVisIdxC())
                .Replace(new IdxC(0));
        }
    }
}