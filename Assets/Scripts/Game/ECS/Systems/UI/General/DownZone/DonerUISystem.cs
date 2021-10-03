using Leopotam.Ecs;
using Scripts.Common;
using UnityEngine;

namespace Scripts.Game
{
    internal sealed class DonerUISystem : IEcsRunSystem
    {
        private EcsFilter<DonerUICom> _donerUIFilter = default;

        public void Run()
        {
            ref var donerViewUICom = ref _donerUIFilter.Get1(0);

            if (GameModesCom.IsOnlineMode)
            {
                //donerViewUICom.SetTextDoner(LanguageComCom.GetText(GameLanguageTypes.Done));
                donerViewUICom.SetTextWait(LanguageComCom.GetText(GameLanguageTypes.WaitPlayer));


                if (WhoseMoveCom.IsMyOnlineMove)
                {
                    donerViewUICom.DisableWait();
                    donerViewUICom.SetColor(Color.white);
                }
                else
                {
                    donerViewUICom.EnableWait();
                    donerViewUICom.SetColor(Color.red);
                }
            }

            else
            {
                donerViewUICom.DisableWait();
            }
        }
    }
}