using System.Collections.Generic;

namespace Chessy.Game.System.View.UI
{
    public readonly struct SystemsViewUI
    {
        public readonly SystemViewUIUpdate UpdateS;

        public readonly RelaxUIS RelaxS;
        public readonly EconomyUpUIS EconomyUpS;

        public SystemsViewUI(in bool def) : this()
        {
            EconomyUpS = new EconomyUpUIS(new Dictionary<ResourceTypes, float>());
        }
    }
}