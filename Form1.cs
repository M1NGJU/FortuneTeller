using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FortuneTeller
{
    public partial class Form1: Form
    {

        List<string> results;

        public Form1()
        {
            InitializeComponent();
            LoadResults();
        }

        private void LoadResults()
        {
            try
            {
                string filename = "results.csv";
                results = File.ReadAllLines(filename).ToList();
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show($"파일이 없어요");
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show($"권한이 없어요");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"알 수 없는 오류가 발생했어요.");
            }
            
        }

        private void tbBirthday_TextChanged(object sender, EventArgs e)
        {

        }

        private void 내역불러오기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormHistory form = Application.OpenForms["FormHistory"] as FormHistory;
            if(form != null)
            {
                form.Activate();
            }
            else
            {
                form = new FormHistory(this);
                form.Show();
            }
                
        }

        private string GetFortun()
        {
            Random random = new Random();
            int index = random.Next(0, results.Count);
            return results[index];
        }

        private void 끝내기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void 포츈텔러정보ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new FormAbout();
            form.ShowDialog();
        }

        private void btnShowResult_Click(object sender, EventArgs e)
        {
            string birthday = tbBirthday.Text;
            string birthtime = tbBirthtime.Text;
            string result = GetFortun();
            string saju = result.Split('|')[0];
            string message = result.Split('|')[1];
            tbResult.Text = birthday +" "+ birthtime + Environment.NewLine+ result
                + saju + Environment.NewLine
                + message;

            SaveHistory($"{birthday}{birthtime}|{result}");
        }

        private void SaveHistory(string history)
        {
            try
            {
                string filename = "history.csv";
                File.AppendAllText(filename, history + Environment.NewLine);
            }
            catch(UnauthorizedAccessException ex)
            {
                MessageBox.Show($"알 수 없는 오류가 발생했습니다.");
            }
            catch(Exception ex)
            {
                MessageBox.Show($"알 수 없는 오류가 발생했습니다.");
            }
        }

        internal void LoadHistory(string history)
        {
            string birthday = history.Split('|')[0].Split(' ')[0];
            tbBirthday.Text = birthday;
            string birthtime = history.Split('|')[0].Split(' ')[0];
            tbBirthtime.Text = birthtime;
            string saju = history.Split('|')[1];
            tbResult.Text = birthday + " " + birthtime + Environment.NewLine
                + saju + Environment.NewLine;
        }
    }
}
