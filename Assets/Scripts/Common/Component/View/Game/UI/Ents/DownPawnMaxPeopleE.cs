using ECS;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace Game.Game
{
    public sealed class DownPawnMaxPeopleE : EntityAbstract
    {
        ref TextUIC TextUICRef => ref Ent.Get<TextUIC>();

        internal DownPawnMaxPeopleE(in TextMeshProUGUI text, in EcsWorld gameW) : base(gameW)
        {
            Ent.Add(new TextUIC(text));
        }

        public void SetMaxPeople(in int maxPeople) => TextUICRef.Text = maxPeople.ToString();
    }
}