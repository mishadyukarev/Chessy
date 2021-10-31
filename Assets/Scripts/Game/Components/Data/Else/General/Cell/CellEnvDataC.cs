using System;
using System.Collections.Generic;

namespace Scripts.Game
{
    public struct CellEnvDataC
    {
        private Dictionary<EnvTypes, bool> _haveEnvir;
        private Dictionary<EnvTypes, int> _amountResours;

        public Dictionary<EnvTypes, bool> Envronments
        {
            get
            {
                var envrs = new Dictionary<EnvTypes, bool>();

                for (var envType = Support.MinEnvironType; envType < Support.MaxEnvironType; envType++)
                {
                    if (_haveEnvir[envType]) envrs.Add(envType, true);
                    else envrs.Add(envType, false);
                }

                return envrs;
            }
        }

        public CellEnvDataC(Dictionary<EnvTypes, bool> haveCellEnvironments)
        {
            _haveEnvir = haveCellEnvironments;
            _amountResours = new Dictionary<EnvTypes, int>();

            for (var envirType = Support.MinEnvironType; envirType < Support.MaxEnvironType; envirType++)
            {
                _haveEnvir.Add(envirType, default);
                _amountResours.Add(envirType, default);
            }
        }

        public bool Have(EnvTypes envType)
        {
            if (envType == default) throw new Exception();
            return _haveEnvir[envType];
        }
        public bool Have(EnvTypes[] envTypes)
        {
            foreach (var envType in envTypes) if (Have(envType)) return true;
            return false;
        }
        public int AmountRes(EnvTypes envType)
        {
            if (envType == default) throw new Exception();
            return _amountResours[envType];
        }
        public void SetAmountRes(EnvTypes envType, int value)
        {
            if (envType == default) throw new Exception();
            _amountResours[envType] = value;
        }
        public byte MaxAmountRes(EnvTypes envType)
        {
            if (envType == default) throw new Exception();
            return EnvironValues.MaxAmount(envType);
        }
        public void SetMaxAmountRes(EnvTypes envType) => SetAmountRes(envType, MaxAmountRes(envType));
        public void AddAmountRes(EnvTypes envType, int adding = 1) => SetAmountRes(envType, AmountRes(envType) + adding);
        public void AddMaxAmountRes(EnvTypes envType) => SetAmountRes(envType, AmountRes(envType) + MaxAmountRes(envType));
        public void TakeAmountRes(EnvTypes envType, int taking = 1) => SetAmountRes(envType, AmountRes(envType) - taking);
        public bool HaveRes(EnvTypes envType) => AmountRes(envType) > 0;
        public bool HaveMaxRes(EnvTypes envType) => AmountRes(envType) >= MaxAmountRes(envType);
        public void Set(EnvTypes envType, bool haveEnv, byte amountRes)
        {
            _haveEnvir[envType] = haveEnv;
            _amountResours[envType] = amountRes;
        }
        public void Reset(EnvTypes envType)
        {
            if (!Have(envType)) throw new Exception();
            Set(envType, default, default);
        }
        public void SetNew(EnvTypes envType)
        {
            if (Have(envType)) throw new Exception();

            byte randAmountRes = 0;
            switch (envType)
            {
                case EnvTypes.None:
                    throw new Exception();

                case EnvTypes.Fertilizer:
                    randAmountRes = (byte)UnityEngine.Random.Range(EnvironValues.MinAmount(envType), EnvironValues.MaxAmount(envType) + 1);
                    break;

                case EnvTypes.YoungForest:
                    break;

                case EnvTypes.AdultForest:
                    randAmountRes = (byte)UnityEngine.Random.Range(EnvironValues.MinAmount(envType), EnvironValues.MaxAmount(envType) + 1);
                    break;

                case EnvTypes.Hill:
                    randAmountRes = 0;
                    break;

                case EnvTypes.Mountain:
                    break;

                default:
                    throw new Exception();
            }
            Set(envType, true, randAmountRes);
        }
    }
}
