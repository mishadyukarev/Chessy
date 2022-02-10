﻿using ECS;
using UnityEngine.UI;

namespace Game.Game
{
    public sealed class LeftSmelterTogglerUIE : EntityAbstract
    {
        public ButtonUIC ButtonUIC => Ent.Get<ButtonUIC>();

        internal LeftSmelterTogglerUIE(in Button button, in EcsWorld gameW) : base(gameW)
        {
            Ent.Add(new ButtonUIC(button));
        }
    }
}