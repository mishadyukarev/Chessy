using ECS;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Game
{
    public sealed class RightEffectUIE : EntityAbstract
    {
        public ref GameObjectVC GO => ref Ent.Get<GameObjectVC>();
        public ref ImageUIC ImageUIC => ref Ent.Get<ImageUIC>();

        internal RightEffectUIE(in Transform rightEffectT, in byte number, in EcsWorld gameW) : base(gameW)
        {
            var trans = rightEffectT.Find("Effect_" + number.ToString());

            Ent.Add(new GameObjectVC(trans.gameObject))
                .Add(new ImageUIC(trans.Find("Image").GetComponent<Image>()));
        }
    }
}