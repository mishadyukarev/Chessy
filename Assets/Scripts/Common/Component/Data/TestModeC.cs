namespace Game.Common
{
    public struct TestModeC
    {
        public static TestModes TestMode { get; private set; }

        public static bool Is(params TestModes[] testModes)
        {
            foreach (var testMode in testModes)
            {
                if (TestMode == testMode) return true;
            }
            return false;
        }

        public TestModeC(TestModes testMode)
        {
            TestMode = testMode;
        }
    }
}