using Chessy.Common.Entity;

namespace Chessy.Menu
{
    public sealed class SystemsModelMenu : IUpdate
    {
        readonly EntitiesModelCommon _eMCommon;

        public ComeToTrainingS ComeToTrainingS;

        public SystemsModelMenu(in EntitiesModelCommon eMCommon)
        {
            _eMCommon = eMCommon;

            ComeToTrainingS = new ComeToTrainingS();
        }

        public void Update()
        {

        }
    }
}