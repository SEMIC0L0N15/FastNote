using FastNote.Core;
using NUnit.Framework;

namespace FastNote.Test
{
    [SetUpFixture]
    public class GlobalTestsSetup
    {
        [OneTimeSetUp]
        public void GlobalSetup()
        {
            IoC.Setup();
        }
    }
}
