using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Systems.General.UI.RunUpdate.CenterZone
{
    internal sealed class MistakeBarUISystem : IEcsInitSystem, IEcsRunSystem
    {
        private float _neededTimeForFading = 1.3f;
        private float _currentTime;

        public void Init()
        {
            //MiddleVisUIWorker.AddListenerMistakeBar(ActiveBar);
        }

        public void Run()
        {
            //switch (MiddleVisUIWorker.MistakeTypeBar)
            //{
            //    case MistakeTypes.None:
            //        MiddleVisUIWorker.SetActiveMistakeBar(false);
            //        break;

            //    case MistakeTypes.EconomyType:
            //        MiddleVisUIWorker.TextMistakeBar = "Need more resources";
            //        MiddleVisUIWorker.SetActiveMistakeBar(true);

            //        _currentTime += Time.deltaTime;

            //        if (_currentTime >= _neededTimeForFading)
            //        {
            //            _currentTime = 0;
            //            MiddleVisUIWorker.SetActiveMistakeBar(false);
            //            MiddleVisUIWorker.MistakeTypeBar = default;
            //        }
            //        break;

            //    case MistakeTypes.UnitType:
            //        break;

            //    default:
            //        throw new Exception();
            //}
        }

        private void ActiveBar()
        {

        }
    }
}
