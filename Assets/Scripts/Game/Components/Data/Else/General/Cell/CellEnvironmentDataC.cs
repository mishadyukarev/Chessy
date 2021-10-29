﻿using System;
using System.Collections.Generic;

namespace Scripts.Game
{
    public struct CellEnvironmentDataC
    {
        private Dictionary<EnvirTypes, bool> _haveEnvir;
        private Dictionary<EnvirTypes, int> _amountResours;

        public int NeedAmountSteps
        {
            get
            {
                int amountSteps = 1;         

                if (Have(EnvirTypes.AdultForest))
                    amountSteps += UnitValues.NeedAmountSteps(EnvirTypes.AdultForest);

                if (Have(EnvirTypes.Hill))
                    amountSteps += UnitValues.NeedAmountSteps(EnvirTypes.Hill);

                return amountSteps;
            }
        }
        public Dictionary<EnvirTypes, bool> Envronments
        {
            get
            {
                var envrs = new Dictionary<EnvirTypes, bool>();

                for (var envType = Support.MinEnvironType; envType < Support.MaxEnvironType; envType++)
                {
                    if (_haveEnvir[envType]) envrs.Add(envType, true);
                    else envrs.Add(envType, false);
                }

                return envrs;
            }
        }

        public CellEnvironmentDataC(Dictionary<EnvirTypes, bool> haveCellEnvironments)
        {
            _haveEnvir = haveCellEnvironments;
            _amountResours = new Dictionary<EnvirTypes, int>();

            for (var envirType = Support.MinEnvironType; envirType < Support.MaxEnvironType; envirType++)
            {
                _haveEnvir.Add(envirType, default);
                _amountResours.Add(envirType, default);
            }
        }

        private void CheckDef(EnvirTypes envType)
        {
            if (envType == default) throw new Exception();
        }

        public bool Have(EnvirTypes envType)
        {
            if (envType == default) throw new Exception();
            return _haveEnvir[envType];
        }

        public int AmountRes(EnvirTypes envType)
        {
            CheckDef(envType);
            return _amountResours[envType];
        }
        public void SetAmountRes(EnvirTypes envType, int value)
        {
            CheckDef(envType);
            _amountResours[envType] = value;
        }
        public byte MaxAmountRes(EnvirTypes envType)
        {
            CheckDef(envType);
            return EnvironValues.MaxAmount(envType);
        }
        public void SetMaxAmountRes(EnvirTypes envType) => SetAmountRes(envType, MaxAmountRes(envType));
        public void AddAmountRes(EnvirTypes envType, int adding = 1) => SetAmountRes(envType, AmountRes(envType) + adding);
        public void AddMaxAmountRes(EnvirTypes envType) => SetAmountRes(envType, AmountRes(envType) + MaxAmountRes(envType));
        public void TakeAmountRes(EnvirTypes envType, int taking = 1) => SetAmountRes(envType, AmountRes(envType) - taking);
        public bool HaveRes(EnvirTypes envType) => AmountRes(envType) > 0;
        public bool HaveMaxRes(EnvirTypes envType) => AmountRes(envType) >= MaxAmountRes(envType);



        public void Sync(EnvirTypes envType, bool haveEnv, byte amountRes)
        {
            _haveEnvir[envType] = haveEnv;
            _amountResours[envType] = amountRes;
        }
        public void Reset(EnvirTypes envType)
        {
            if (_haveEnvir[envType] == false) throw new Exception();
            Sync(envType, default, default);
        }
        public void SetNew(EnvirTypes envType)
        {
            byte randAmountRes = 0;
            switch (envType)
            {
                case EnvirTypes.None:
                    throw new Exception();

                case EnvirTypes.Fertilizer:
                    randAmountRes = (byte)UnityEngine.Random.Range(EnvironValues.MinAmount(envType), EnvironValues.MaxAmount(envType) + 1);
                    break;

                case EnvirTypes.YoungForest:
                    break;

                case EnvirTypes.AdultForest:
                    randAmountRes = (byte)UnityEngine.Random.Range(EnvironValues.MinAmount(envType), EnvironValues.MaxAmount(envType) + 1);
                    break;

                case EnvirTypes.Hill:
                    randAmountRes = 0;
                    break;

                case EnvirTypes.Mountain:
                    break;

                default:
                    throw new Exception();
            }
            Sync(envType, true, randAmountRes);
        }
    }
}
