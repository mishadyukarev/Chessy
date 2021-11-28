using System;

namespace Game.Game
{
    public struct ShieldC : ITWCellE
    {
        int _protection;

        public int Protection => _protection;
        public bool Have => _protection > 0;



        public void SetShieldProtect(LevelTypes level)
        {
            switch (level)
            {
                case LevelTypes.None: throw new Exception();
                case LevelTypes.First: _protection = 1; return;
                case LevelTypes.Second: _protection = 3; return;
                default: throw new Exception();
            }
        }
        public void Set(ShieldC shieldC)
        {
            _protection = shieldC._protection;
        }
        public void TakeShieldProtect(int taking = 1)
        {
            _protection -= taking;
            //if (Protection <= 0)
            //{
            //    TW = TWTypes.None;
            //}
        }
        public void Sync(int shieldProt) => _protection = shieldProt;
    }
}