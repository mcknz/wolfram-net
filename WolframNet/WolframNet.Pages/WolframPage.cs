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
            string questionTextXPath = "//*[@id='__next']/div/div[1]/div/div/div[1]/section/form/div/div/input";
            string questionButtonXPath = "//button[@type='submit']";

            EnterTextByXPath(questionTextXPath, question);
            Pause();
            ClickByXPath(questionButtonXPath);
            Pause();
        }

        public string GetAnswer()
        {
            string rootXPath = "//*[@id='__next']/div/div[1]/main/div[2]/div/div[2]/section/";
            string answerDivXPath = $"{rootXPath}section[2]";
            string answerButtonXPath = $"{rootXPath}section[2]/ul/li[4]/button/span";
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
