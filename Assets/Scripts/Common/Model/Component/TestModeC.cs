﻿namespace Chessy.Common
{
    public struct TestModeC
    {
        public TestModes TestModeT;

        public bool Is(params TestModes[] testModes)
        {
            foreach (var testMode in testModes)
            {
                if (TestModeT == testMode) return true;
            }
            return false;
        }

        public TestModeC(TestModes testMode) => TestModeT = testMode;
    }
}