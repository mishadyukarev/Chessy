using Chessy.Game.Model.Component;
using System.Collections.Generic;

namespace Chessy.Game
{
    public struct BuildingE
    {
        public BuildingTC BuildingTC;
        public PlayerTC PlayerTC;
        public LevelTC LevelTC;
        public HealthC HealthC;
        public readonly VisibleC VisibleC;

        public ResourcesC WoodcutterExtractC;
        public ResourcesC FarmExtractC;

        internal BuildingE(in bool b) : this()
        {
            var isVisibled = new Dictionary<PlayerTypes, bool>();

            for (var playerT = PlayerTypes.None + 1; playerT < PlayerTypes.End; playerT++)
                isVisibled.Add(playerT, default);

            VisibleC = new VisibleC(isVisibled);
        }
    }
}