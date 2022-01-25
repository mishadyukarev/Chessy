using ECS;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Game
{
    public sealed class RightUniqueUIE : EntityAbstract
    {
        public ref GameObjectVC Paren => ref Ent.Get<GameObjectVC>();
        public ref ButtonUIC Button => ref Ent.Get<ButtonUIC>();
        public ref TextUIC Text => ref Ent.Get<TextUIC>();
        public ref ImageUIC ImageC => ref Ent.Get<ImageUIC>();

        public RightUniqueUIE(in EcsWorld gameW, in Transform button) : base(gameW)
        {
            Ent.Add(new GameObjectVC(button.gameObject))
                .Add(new ButtonUIC(button.Find("Button").GetComponent<Button>()))
                .Add(new TextUIC(button.Find("Cooldown").Find("Text (TMP)").GetComponent<TextMeshProUGUI>()))
                .Add(new ImageUIC(button.Find("Ability_Image").GetComponent<Image>()));
        }
    }
}