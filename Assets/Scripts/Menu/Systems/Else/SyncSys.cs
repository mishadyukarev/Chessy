using Leopotam.Ecs;
using Scripts.Common;

namespace Scripts.Menu
{
    internal sealed class SyncSys : IEcsRunSystem
    {
        public void Run()
        {
            SoundComComp.Volume = CenterZoneUICom.MusicVolume;

            ShopZoneUICom.SetTextInfo(LanguageComCom.GetText(ComLanguageTypes.InfoBuy));

            HintComC.EnabledHint = CenterZoneUICom.InteractableHint;

        }
    }
}
