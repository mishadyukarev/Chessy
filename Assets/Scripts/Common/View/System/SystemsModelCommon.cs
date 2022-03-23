using Chessy.Common.Entity;
using System.Collections.Generic;

namespace Chessy.Common.Model.System
{
    public sealed class SystemsModelCommon : IEcsRunSystem
    {
        readonly List<IEcsRunSystem> _updates;

        public SystemsModelCommon(in EntitiesModelCommon eMCommon)
        {
            _updates = new List<IEcsRunSystem>()
            {
                new AdLaunchS(eMCommon),
            };
        }

        public void Run()
        {
            _updates.ForEach((IEcsRunSystem iRun) => iRun.Run());
        }
    }
}