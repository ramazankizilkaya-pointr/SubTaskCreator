using OpenQA.Selenium.Chrome;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SubTaskCreator
{
    public partial class Form1 : Form
    {
        private List<Control> requiredInputControls = new List<Control>();
        private string userName;
        private string userPassword;

        public Form1()
        {
            InitializeComponent();

            qaListBox.Items.Add("Nurullah Karaköse");
            qaListBox.Items.Add("Tülay Palta");
            qaListBox.Items.Add("Furkan Tan");
            qaListBox.Items.Add("Berk Eşgüner");
            qaListBox.Items.Add("Erol Isildak");
            qaListBox.Items.Add("Ramazan Kızılkaya");
            
            qaListBox.SelectionMode = SelectionMode.MultiExtended;
            
            requiredInputControls.Add(ticketId);
            requiredInputControls.Add(projectFolderPath);
            requiredInputControls.Add(releaseVersion);

            foreach (Control control in requiredInputControls)
            {
                control.TextChanged += InputFields_TextChanged;
            }
            for (int i = 0; i < qaListBox.Items.Count; i++)
            {
                qaListBox.SetSelected(i, true);
            }
            button1.Enabled = false;
            CheckConfigs();
        }


        private async void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to proceed?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                return;
            }

            button1.Text = "Started working.";
            button1.Enabled = false;
            var jiraTicketId = ticketId.Text.Trim();
            var releaseVersionText = releaseVersion.Text.Trim();
            var projectPath = projectFolderPath.Text;
            var featureFileNamList = new List<string>();
            var featureFolderPath = Path.Combine(projectPath, "qa-scenarios");
            var fileList = Directory.GetFiles(featureFolderPath, "*.feature");
            foreach (var file in fileList)
            {
                featureFileNamList.Add(Path.GetFileNameWithoutExtension(file).ToUpper());
            }
            var testerList = new List<string>();
            var selectedTesters = qaListBox.SelectedItems;
            var selectedTesters2 = qaListBox.SelectedItems;
            foreach (var item in selectedTesters)
            {
                testerList.Add(item.ToString());
            }
            var options = new ChromeOptions();
            options.AddArguments("start-maximized");
            options.AddArguments("headless");
;            var driver = new ChromeDriver(options);
            var bot = new JiraBot(driver, logTextBox);
            await Task.Run(()=> bot.LoginAndOpenTaskPage(jiraTicketId, userName, userPassword));
            await Task.Run(()=> bot.CreateSubTask(featureFileNamList, testerList, releaseVersionText));
        }

        private void InputFields_TextChanged(object sender, EventArgs e)
        {
            bool allInputsFilled = requiredInputControls.All(control => !string.IsNullOrWhiteSpace(control.Text));
            button1.Enabled = allInputsFilled;
        }

        public void CheckConfigs()
        {
            var parentDir = Directory.GetCurrentDirectory();
            var configFilePath = Path.Combine(parentDir, "configs.txt");

            if (!File.Exists(configFilePath)){
                using (StreamWriter writer = new StreamWriter(configFilePath))
                {
                    writer.WriteLine("Jira username: ");
                    writer.WriteLine("Jira password: ");
                }
                MessageBox.Show("Please enter your jira credentials to the config file, save and try again");
                Process.Start("notepad.exe", configFilePath);
                System.Environment.Exit(0);

            }
            else
            {
                List<string> lines = File.ReadLines(configFilePath).ToList();
                for (int i = 0; i < lines.Count(); i++){
                    if (lines[i].Contains("Jira username:"))
                    {
                        userName = lines[i].Replace("Jira username:", "").Trim();
                    }
                    if (lines[i].Contains("Jira password:"))
                    {
                        userPassword = lines[i].Replace("Jira password:", "").Trim();
                    }
                }
                if(userName.Length == 0 | userPassword.Length == 0)
                {
                    MessageBox.Show("Plese enter your jira credentials and save the file");
                    Process.Start("notepad.exe", configFilePath);
                    System.Environment.Exit(0);
                }
            }
            //var lines = File.ReadAllLines(parentDir);
        }
    }
}
