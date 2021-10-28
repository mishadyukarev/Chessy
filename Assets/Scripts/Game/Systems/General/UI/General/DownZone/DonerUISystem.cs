using Leopotam.Ecs;
using Photon.Pun;
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

            //if (!PhotonNetwork.OfflineMode)
            //{
                if (WhoseMoveC.IsMyMove)
                {
                    donerViewUICom.DisableWait();
                    donerViewUICom.SetColor(Color.white);
                }
                else
                {
                    donerViewUICom.EnableWait();
                    donerViewUICom.SetColor(Color.red);
                }
            //}

            //else
            //{
            //    donerViewUICom.DisableWait();
            //}
        }
    }
}