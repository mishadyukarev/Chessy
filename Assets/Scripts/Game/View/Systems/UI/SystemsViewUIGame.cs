using Chessy.Game.System.View.UI.Center;
using System.Collections.Generic;

namespace Chessy.Game.System.View.UI
{
    public readonly struct SystemsViewUIGame
    {
        public readonly UpdateViewUIS UpdateS;

        public readonly RelaxUIS RelaxS;
        public readonly EconomyUpUIS EconomyUpS;
        public readonly EffectsUIS EffectsS;
        public readonly SyncBookUIS SyncBookUIS;
        public readonly ProtectUIS ProtectS;

        public SystemsViewUIGame(in bool def) : this()
        {
            EconomyUpS = new EconomyUpUIS(new Dictionary<ResourceTypes, float>());
            EffectsS = new EffectsUIS(new Dictionary<EffectTypes, bool>());
        }
    }
}