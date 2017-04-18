using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace DirectoryCreator {

    public partial class Form1 : Form {
        string PATH = "";
        private string[] pathParts;

        public Form1() {
            InitializeComponent();
        }

        private void createButton_Click(object sender, EventArgs e) {
            ProcessStartInfo psi = new ProcessStartInfo();
            string regexedText = Regex.Replace(textBox.Text, "[^a-zA-Z0-9а-яА-Я ]", "");
            string[] words = new string[regexedText.Split(' ').Length];
            words = regexedText.Split(' ');
            string commandLine = pathParts[0] + ":" + " & cd " + pathParts[1];
            for (int i = 0; i < words.Length; i++) {
                commandLine += " & " + "mkdir " + words[i] + " & " + "cd " + words[i];
            }
            psi.FileName = "cmd";
            psi.Arguments = @"/c" + commandLine; // /c - zakrit'. k - ostavit'
            Process.Start(psi);
            label1.Text = "Catalogs has been created";
            textBox.Text = "";
            Console.WriteLine(pathParts[0] + ", " + pathParts[1]);
        }

        private void browseButton_Click(object sender, EventArgs e) {
            DialogResult res = folderBrowserDialog1.ShowDialog();
            if (res == DialogResult.OK) {
                PATH = folderBrowserDialog1.SelectedPath;
            }
            pathTextBox.Text = PATH;
            createButton.Enabled = true;
            pathParts = PATH.Split(':'); //[0] - literal, [1] - put'
        }

        private void textBox_TextChanged(object sender, EventArgs e) {
            if (textBox.Text == "" || PATH == "") {
                createButton.Enabled = false;
            } else {
                createButton.Enabled = true;
            }
        }
    }

}