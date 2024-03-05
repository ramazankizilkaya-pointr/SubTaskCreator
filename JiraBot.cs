using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V120.WebAuthn;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xunit;
using Keys = OpenQA.Selenium.Keys;

namespace SubTaskCreator
{
    public class JiraBot
    {
        ChromeDriver driver;
        private RichTextBox _logBox;
        public Dictionary<string, List<string>> assignmentList = new Dictionary<string, List<string>>();

        public JiraBot(ChromeDriver driver, RichTextBox logBox)
        {
            this.driver = driver;
            _logBox = logBox;

        }


        public void LoginAndOpenTaskPage(string jiraTicetId, string name, string pwd)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            driver.Navigate().GoToUrl($"https://pointr.atlassian.net/browse/{jiraTicetId}");
            driver.FindElement(By.XPath("//input[@id='username']")).SendKeys(name);
            driver.FindElement(By.XPath("//button[@id='login-submit']")).Click();
            driver.FindElement(By.XPath("//input[@id='password']")).SendKeys(pwd);
            LogMessage("logged into Jira...");
            driver.FindElement(By.XPath("//button[@id='login-submit']")).Click();
            var subTaskBtn = driver.FindElement(By.XPath("//*[text()='Create subtask']"));
            Assert.Equal(subTaskBtn.Displayed, true);
        }

        public void CreateSubTask(List<string> featureList, List<string> qaList, string version)
        {
            featureList.Sort();
            qaList = [.. qaList.OrderBy(x => Guid.NewGuid())];
            foreach (var qa in qaList)
            {
                assignmentList.Add(qa, new List<string>());
            }
            var createSubTaskBtn = driver.FindElement(By.XPath("//button[@aria-label='Create subtask']"));
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            createSubTaskBtn.Click();

            foreach (var feature in featureList)
            {
                var subTaskInput = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//input[@placeholder='What needs to be done?']")));
                subTaskInput.Click();
                subTaskInput.SendKeys($"{feature} ({version})");
                Thread.Sleep(1000);
                var createBtn = driver.FindElement(By.XPath("//button[@data-testid and .//span[text()='Create']]"));
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", createBtn);
                createBtn.Click();
                var createdElement = driver.FindElement(By.XPath($"//div[@data-testid='issue-line-card.card-container' and contains(.,'{feature}')]"));
                Assert.Equal(createdElement.Displayed, true);
                LogMessage($"{feature} subtask created...");
                Thread.Sleep(1000);
                var lastElementStateDropdown = driver.FindElement(By.XPath("//div[@data-testid]//ul/div[last()]//button[@aria-label='Draft - Change status']"));
                lastElementStateDropdown.Click();
                var stateDropdownBacklogOption = driver.FindElement(By.XPath("//div[contains(@id,'select')]//div[contains(., 'Backlog') and contains(., 'Blocked')]//*[text()='Backlog']"));
                stateDropdownBacklogOption.Click();
                LogMessage($"{feature} subtask moved to Backlog");
                Thread.Sleep(500);
                Assert.Equal(driver.FindElements(By.XPath("//div[contains(@id,'select')]//div[contains(., 'Backlog') and contains(., 'Blocked')]//*[text()='Backlog']")).Count, 0);
                Thread.Sleep(500);

                var lastElementsAssigneeDropdown = driver.FindElement(By.XPath("//div[@data-testid]//ul/div[last()]//div[@role='button']/button"));
                lastElementsAssigneeDropdown.Click();
                Thread.Sleep(500);

                var user = GetUser();
                driver.FindElement(By.XPath("//input[contains(@class, 'fabric-user-picker__input')]")).SendKeys(user);
                Thread.Sleep(1000);
                driver.FindElement(By.XPath($"//div[contains(@class,'fabric-user-picker__option') and contains(.,'{user}')]")).Click();
                Thread.Sleep(1000);
                assignmentList.First(x => x.Key == user).Value.Add(feature);
                LogMessage($"{feature} assigned to {user}");
            }
            LogMessage($"Operation success... {featureList.Count} subtasks created...");
            foreach (var assignment in assignmentList)
            {
                LogMessage($"{assignment.Key}) Tasks count: {assignment.Value.Count}, Tasks: {string.Join(", ", assignment.Value)}");
            }
            MessageBox.Show("Done");
        }

        public void LogMessage(string message)
        {
            if (_logBox.InvokeRequired)
            {
                _logBox.Invoke(new MethodInvoker(delegate {
                    _logBox.AppendText(message + Environment.NewLine);
                }));
            }
            else
            {
                _logBox.AppendText(message + Environment.NewLine);
            }

        }


        public string GetUser()
        {
            assignmentList = assignmentList.OrderBy(kvp => kvp.Value.Count)
                                                 .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            return assignmentList.First().Key;
        }

    }

}
