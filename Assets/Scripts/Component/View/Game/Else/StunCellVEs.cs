using ECS;
using UnityEngine;

namespace Game.Game
{
    public struct StunCellVEs
    {
        static Entity[] _stuns;

        public static ref C Stun<C>(in byte idx) where C : struct, IStunCellVE => ref _stuns[idx].Get<C>();

        public StunCellVEs(in EcsWorld gameW, in GameObject[] cells)
        {
            _stuns = new Entity[cells.Length];

            for (var idx = 0; idx < _stuns.Length; idx++)
            {
                _stuns[idx] = gameW.NewEntity()
                    .Add(new SpriteRendererVC(cells[idx].transform.Find("Stun_SR").GetComponent<SpriteRenderer>()));
            }
        }
    }

    public interface IStunCellVE { }
}