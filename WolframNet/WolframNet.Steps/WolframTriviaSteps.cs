using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TechTalk.SpecFlow;
using WolframNet.Pages;
using WolframNet.Web;

namespace WolframNet.Steps
{
    [Binding]
    public class WolframTriviaSteps
    {
        readonly WolframPage page = new WolframPage();

        [Given]
        public void Given_I_navigate_to_Wolfram_Alpha()
        {
            try
            {
                Console.WriteLine("Given");
                page.Go();
            }
            catch(Exception ex)
            {
                FailStep(ex);
            }
        }
        
        [When]
        public void When_I_ask_QUESTION(string question)
        {
            try
            { 
                page.Ask(question);
            }
            catch(Exception ex)
            {
                FailStep(ex);
            }
        }
        
        [Then]
        public void Then_Wolfram_Alpha_answers_ANSWER(string answer)
        {
            try
            {
                Assert.IsTrue(page.GetAnswer().Contains(answer));
                if (answer.Equals("3720:1")) {
                    Driver.Quit();
                }
            }
            catch (Exception ex)
            {
                FailStep(ex);
            }
        }

        private void FailStep(Exception ex)
        {
            Assert.Fail(ex.Message);
        }
    }
}
