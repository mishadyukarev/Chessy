using Chessy.Common.Entity;

namespace Chessy.Common.Model.System
{
    public sealed class SystemsModelCommon : IEcsRunSystem
    {
        readonly EntitiesModelCommon _eMCommon;

        public SystemsModelCommon(in EntitiesModelCommon eMCommon)
        {
            _eMCommon = eMCommon;
        }

        public void Run()
        {
            new AdLaunchS().Run(ref _eMCommon.AdC, _eMCommon.SceneC);
        }
    }
}