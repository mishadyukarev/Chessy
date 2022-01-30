using UnityEngine;

namespace Game.Game
{
    sealed class UpdateIceWallMS : SystemAbstract, IEcsRunSystem
    {
        public UpdateIceWallMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            var cellEs = Es.CellEs;
            var buildEs = Es.CellEs.BuildEs;


            for (byte idx_0 = 0; idx_0 < cellEs.Count; idx_0++)
            {
                if (buildEs.Build(idx_0).BuildTC.Is(BuildingTypes.IceWall))
                {
                    Debug.Log(buildEs.Build(idx_0).Health.Amount);
                    buildEs.Build(idx_0).Health.Take();
                }
            }
        }
    }
}