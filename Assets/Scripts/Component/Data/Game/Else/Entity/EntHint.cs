using ECS;

namespace Game.Game
{
    public struct EntHint
    {
        static Entity _entity;
        //static readonly Dictionary<VideoClipTypes, bool> _wasActivated;
        //public static int CurStartNumber;

        public EntHint(in EcsWorld gameW)
        {
            _entity = gameW.NewEntity()
                .Add(new WasActivatedC());

            //CurStartNumber = 1;
            //_wasActivated = hint;

            //for (var video = (VideoClipTypes)1; video < (VideoClipTypes)typeof(VideoClipTypes).GetEnumNames().Length; video++)
            //{
            //    _wasActivated.Add(video, false);
            //}
        }
    }
}