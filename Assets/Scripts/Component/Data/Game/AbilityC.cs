﻿namespace Game.Game
{
    public struct AbilityC
    {
        public AbilityTypes Ability;

        public bool Is(in AbilityTypes unique) => Ability == unique;

        public void Reset() => Ability = default;
    }
}