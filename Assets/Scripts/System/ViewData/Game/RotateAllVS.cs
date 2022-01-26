using Game.Common;
using System;
using UnityEngine;

namespace Game.Game
{
    struct RotateAllVS : IEcsRunSystem
    {
        public void Run()
        {
            var curPlayer = Entities.WhoseMoveE.CurPlayerI;

            foreach (byte idx_0 in CellEs.Idxs)
            {
                if (curPlayer == PlayerTypes.None) throw new Exception();
                CellVEs.Cell<SpriteRendererVC>(idx_0).RotParent = curPlayer == PlayerTypes.First 
                    ? new Quaternion(0, 0, 0, 0) : new Quaternion(0, 0, 180, 0);

                switch (curPlayer)
                {
                    case PlayerTypes.None: throw new Exception();

                    case PlayerTypes.First:
                        CellTrailVEs.TrailCellVC<ParentTransformVC>(DirectTypes.Up, idx_0).Transform.localEulerAngles = new Vector3(0, 0, 0);
                        break;

                    case PlayerTypes.Second:
                        CellTrailVEs.TrailCellVC<ParentTransformVC>(DirectTypes.Up, idx_0).Transform.localEulerAngles = new Vector3(0, 0, 180);
                        break;

                    default: throw new Exception();
                }
            }

            CameraVC.SetPosRotClient(curPlayer, MainGoVC.Pos);
        }

        public void SetRotForClient(PlayerTypes player)
        {
            
        }
    }
}