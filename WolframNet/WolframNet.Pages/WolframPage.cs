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

        public void Ask(String question)
        {
            String questionTextXPath = "//input[@placeholder='Enter what you want to calculate or know about']";
            String questionButtonXPath = "//button[@type='submit']";

            EnterTextByXPath(questionTextXPath, question);
            Pause();
            ClickByXPath(questionButtonXPath);
            Pause();
        }

        public String GetAnswer()
        {
            String answerDivXPath = "//*[@id=\"root\"]/div/div/main/div[3]/div/section[1]/section[2]/div[2]";
            String answerButtonXPath = "//span[text() = 'Plaintext']";
            String answerTextXPath = "//div[@aria-describedby='tooltip5']/button/span";

            MouseoverByXPath(answerDivXPath);
            Pause();
            ClickByXPath(answerButtonXPath);
            Pause();
            return GetTextByXPath(answerTextXPath);
        }

        private void Pause()
        {
            try
            {
                Thread.Sleep(3000);
            }
            catch (Exception)
            {
                //sleep for display purposes, don't need to handle exception.
            }
        }
    }
}
