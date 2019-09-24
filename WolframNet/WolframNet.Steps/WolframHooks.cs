using System;
using TechTalk.SpecFlow;
using WolframNet.Web;

namespace WolframNet.Features
{
    [Binding]
    public sealed class WolframHooks
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks
        /*
        [AfterTestRun]
        public static void AfterTestRun()
        {
            Driver.Quit();
        }
        */

        [AfterFeature]
        public static void AfterFeature()
        {
            Console.WriteLine("AfterFeature");
            //Driver.Quit();
        }
    }
}
