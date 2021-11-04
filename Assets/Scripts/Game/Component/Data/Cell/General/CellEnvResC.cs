using System;
using System.Collections.Generic;

namespace Scripts.Game
{
    public struct CellEnvResC
    {
        private Dictionary<EnvTypes, int> _amountResours;

        public CellEnvResC(bool needNew)
        {
            if (needNew)
            {
                _amountResours = new Dictionary<EnvTypes, int>();

                for (var envirType = Support.MinEnvironType; envirType < Support.MaxEnvironType; envirType++)
                {
                    _amountResours.Add(envirType, default);
                }
            }
            else throw new Exception();
        }

        public int AmountRes(EnvTypes envType)
        {
            if (envType == default) throw new Exception();
            return _amountResours[envType];
        }
        public void SetRes(EnvTypes envType, int value)
        {
            if (envType == default) throw new Exception();
            _amountResours[envType] = value;
        }
        public byte MaxAmountRes(EnvTypes envType)
        {
            if (envType == default) throw new Exception();
            return EnvironValues.MaxAmount(envType);
        }
        public void SetMaxAmountRes(EnvTypes envType) => SetRes(envType, MaxAmountRes(envType));
        public void AddAmountRes(EnvTypes envType, int adding = 1) => SetRes(envType, AmountRes(envType) + adding);
        public void AddMaxAmountRes(EnvTypes envType) => SetRes(envType, AmountRes(envType) + MaxAmountRes(envType));
        public void TakeAmountRes(EnvTypes envType, int taking = 1) => SetRes(envType, AmountRes(envType) - taking);
        public bool HaveRes(EnvTypes envType) => AmountRes(envType) > 0;
        public bool HaveMaxRes(EnvTypes envType) => AmountRes(envType) >= MaxAmountRes(envType);

        public void SetNew(EnvTypes envType)
        {
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
            SetRes(envType, randAmountRes);
        }
    }
}