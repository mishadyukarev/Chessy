using Game.Common;
using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Game
{
    public sealed class CreateEnts : IEcsInitSystem
    {
        private EcsWorld _curGameW = default;

        private EcsFilter<CellVC> _cellVF = default;

        public void Init()
        {
            byte idx_cur = 0;

            for (byte x = 0; x < CellValues.CELL_COUNT_X; x++)
                for (byte y = 0; y < CellValues.CELL_COUNT_Y; y++)
                {
                    _curGameW.NewEntity()
                        .Replace(new XyC(idx_cur, new byte[] { x, y }))
                        .Replace(new CellC(_cellVF.Get1(idx_cur).Cell))
                        .Replace(new EnvC(new Dictionary<EnvTypes, bool>()))
                        .Replace(new EnvResC(true))
                        .Replace(new FireC())
                        .Replace(new CloudC())
                        .Replace(new RiverC(true));


                    _curGameW.NewEntity()
                         .Replace(new BuildC())
                         .Replace(new OwnerC())
                         .Replace(new VisibleC(true));



                    #region Unit

                    _curGameW.NewEntity()
                         .Replace(new UnitC())
                         .Replace(new LevelC())
                         .Replace(new OwnerC())
                         .Replace(new VisibleC(true));



                    _curGameW.NewEntity()
                         .Replace(new ConditionUnitC())
                         .Replace(new MoveInCondC(true))
                         .Replace(new UnitEffectsC(true))
                         .Replace(new StunC());


                    _curGameW.NewEntity()
                         .Replace(new HpC())
                         .Replace(new DamageC())
                         .Replace(new StepC())
                         .Replace(new WaterC());


                    _curGameW.NewEntity()
                         .Replace(new UniqAbilC(true))
                         .Replace(new CooldownUniqC(true));


                    _curGameW.NewEntity()
                        .Replace(new ToolWeaponC());


                    _curGameW.NewEntity()
                         .Replace(new CornerArcherC());

                    #endregion


                    _curGameW.NewEntity()
                        .Replace(new TrailC(new Dictionary<DirectTypes, int>()))
                        .Replace(new VisibleC(true));


                    ++idx_cur;
                }


           
        }
    }
}