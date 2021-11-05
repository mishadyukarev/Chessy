using Leopotam.Ecs;
using Scripts.Common;

namespace Scripts.Menu
{
    public sealed class SyncSys : IEcsRunSystem
    {
        public void Run()
        {
            SoundComC.Volume = CenterZoneUICom.MusicVolume;

            ShopZoneUICom.SetTextInfo(LanguageComCom.GetText(ComLanguageTypes.InfoBuy));

            HintComC.IsOnHint = CenterZoneUICom.IsOn;
        }
    }
}
