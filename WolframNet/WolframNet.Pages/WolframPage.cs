using Microsoft.Extensions.Logging;
using System.Threading;
using WolframNet.Web;

namespace WolframNet.Pages
{
    public class WolframPage : WebPage
    {
        public WolframPage(ILogger<WolframPage> logger) : base("https://www.wolframalpha.com/", logger)
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
            string rootXPath = "//*[@id=\"root\"]/div/div/main/div[3]/div/div[1]/section/";
            string answerDivXPath = $"{rootXPath}section[2]/div";
            string answerButtonXPath = $"{rootXPath}section[2]/ul/li[3]/button/span";
            string answerTextXPath = $"{rootXPath}section[3]/div[1]/div/button/span";

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
