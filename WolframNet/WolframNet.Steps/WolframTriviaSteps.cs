using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using WolframNet.Pages;
using WolframNet.Web;

namespace WolframNet.Steps
{
    [Binding]
    public class WolframTriviaSteps
    {
        WolframPage page = new WolframPage();

        [Given]
        public void Given_I_navigate_to_Wolfram_Alpha()
        {
            page.Go();
        }
        
        [When]
        public void When_I_ask_QUESTION(string question)
        {
            page.Ask(question);
        }
        
        [Then]
        public void Then_Wolfram_Alpha_answers_ANSWER(string answer)
        {
            Assert.IsTrue(page.GetAnswer().Contains(answer));
        }
    }
}
