using Chessy.Common.Entity;
using Chessy.Game.Entity.Model;
using Chessy.Game.System.View.UI.Center;
using System.Collections.Generic;

namespace Chessy.Game.System.View.UI
{
    public sealed class SystemsViewUIGame
    {
        public readonly UpdateViewUIS UpdateS;

        public readonly RelaxUIS RelaxS;
        public readonly EconomyUpUIS EconomyUpS;
        public readonly EffectsUIS EffectsS;
        public readonly SyncBookUIS SyncBookUIS;
        public readonly ProtectUIS ProtectS;

        public SystemsViewUIGame(in EntitiesModelCommon eMCommon, in EntitiesViewUIGame eUIGame, in EntitiesModelGame eMGame)
        {
            UpdateS = new UpdateViewUIS(this, eMCommon, eUIGame, eMGame);

            EconomyUpS = new EconomyUpUIS(new Dictionary<ResourceTypes, float>());
            EffectsS = new EffectsUIS(new Dictionary<EffectTypes, bool>());
        }
    }
}