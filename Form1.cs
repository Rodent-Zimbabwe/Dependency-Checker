using System;
using System.Collections.Generic;

using System.Windows.Forms;

namespace Dependency_Checker
{
    public partial class Form1 : Form
    {
        private Dictionary<string, List<string>>
             installedApplications;

        public Form1()
        {
            InitializeComponent();
            installedApplications = new Dictionary<string, List<string>>();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string appName = textBox1.Text.Trim();
            string dependenciesText = textBox2.Text.Trim();
            if (string.IsNullOrEmpty(appName))
            {
                MessageBox.Show("please neter an app name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            List<string> dependencies = new List<string>();
            if (!string.IsNullOrEmpty(dependenciesText))
            {
                dependencies.AddRange(dependenciesText.Split(','));
            }
            installedApplications.Add(appName, dependencies);
            UpdateApplicationsListBox();
            ClearInputFields();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DependencyChecker dependencyChecker = new DependencyChecker(installedApplications);
            label2.Text = dependencyChecker.CheckDependencies();
        }

        private void UpdateApplicationsListBox()
        {
            listBox1.Items.Clear();
            foreach (var app in installedApplications.Keys)
            {
               listBox1.Items.Add(app);
            }

        }
        private void ClearInputFields()
        {
            textBox1.Clear();
           ///label2.Clear();
        }
    }
    public class DependencyChecker
    {
        private Dictionary<string, List<string>> installedApplications;

        public DependencyChecker(Dictionary<string, List<string>> applications)
        {
            installedApplications = applications;
        }

        public string CheckDependencies()
        {
            string result = "Checking Dependencies...\n\n";

            foreach (var app in installedApplications)
            {
                result += $"Application: {app.Key}\n";

                if (app.Value.Count == 0)
                {
                    result += "No dependencies found.\n\n";
                }
                else
                {
                    result += "Dependencies:\n";

                    foreach (var dependency in app.Value)
                    {
                        result += $" - {dependency}\n";
                    }

                    result += "\n";
                }
            }

            return result;
        }
    }
}

