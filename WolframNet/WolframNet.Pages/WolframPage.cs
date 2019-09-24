using System;
using System.Threading;
using WolframNet.Web;

namespace WolframNet.Pages
{
    public class WolframPage : WebPage
    {
        public WolframPage() : base("https://www.wolframalpha.com/")
        {
        }

        public void Go()
        {
            NavigateToStartingUrl();
            Pause();
        }

        public void Ask(string question)
        {
            string questionTextXPath = "//input[@placeholder='Enter what you want to calculate or know about']";
            string questionButtonXPath = "//button[@type='submit']";

            EnterTextByXPath(questionTextXPath, question);
            Pause();
            ClickByXPath(questionButtonXPath);
            Pause();
        }

        public string GetAnswer()
        {
            string answerDivXPath = "//*[@id=\"root\"]/div/div/main/div[3]/div/section[1]/section[2]/div[2]";
            string answerButtonXPath = "//span[text() = 'Plaintext']";
            string answerTextXPath = "//div[@aria-describedby='tooltip5']/button/span";

            MouseoverByXPath(answerDivXPath);
            Pause();
            ClickByXPath(answerButtonXPath);
            Pause();
            return GetTextByXPath(answerTextXPath);
        }

        private void Pause()
        {
            // for display purposes only
            Thread.Sleep(1);
        }
    }
}
