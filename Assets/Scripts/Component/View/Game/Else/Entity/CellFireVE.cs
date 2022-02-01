using ECS;
using UnityEngine;

namespace Game.Game
{
    public sealed class CellFireVE : EntityAbstract
    {
        public ref SpriteRendererVC SR => ref Ent.Get<SpriteRendererVC>();

        public CellFireVE(in GameObject cell, in EcsWorld gameW) : base(gameW)
        {
            Ent.Add(new SpriteRendererVC(cell.transform.Find("Fire").GetComponent<SpriteRenderer>()));
        }
    }
}