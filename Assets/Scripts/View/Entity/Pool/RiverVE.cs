using Chessy.Model;
using Chessy.View.Component;
using UnityEngine;

namespace Chessy.View.Entity
{
    public struct RiverVE
    {
        readonly SpriteRendererVC[] _rivers;

        public readonly TransformVC ParentTransformVC;

        public SpriteRendererVC River(in DirectTypes dir) => _rivers[(byte)dir];

        public RiverVE(in Transform cell)
        {
            ParentTransformVC = new TransformVC(cell.Find("River"));


            _rivers = new SpriteRendererVC[(byte)DirectTypes.End];

            for (var dir = DirectTypes.Up; dir < DirectTypes.End; dir++)
            {
                if (dir == DirectTypes.Up || dir == DirectTypes.Right || dir == DirectTypes.Down || dir == DirectTypes.Left)
                {
                    _rivers[(byte)dir] = new SpriteRendererVC(cell.Find("River").Find(dir.ToString()).GetComponent<SpriteRenderer>());
                }
            }
        }
    }
}