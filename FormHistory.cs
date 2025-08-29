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
    public partial class FormHistory: Form
    {
        List<string> history;
        Form1 form1;

        public FormHistory(Form1 form)
        {
            this.form1 = form;
            InitializeComponent();
            UPdateHistory();
            
        }

        private void LoadHistory()
        {
            try
            {
                string filename = "history.csv";
                history = File.ReadAllLines(filename).ToList();
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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            UPdateHistory();
        }

        private void UPdateHistory()
        {
            LoadHistory();
            lbHistory.Items.Clear();
            lbHistory.Items.AddRange(history.ToArray());
        }

        private void lbHistory_SelectedIndexChanged(object sender, EventArgs e)
        {
            string message = history[lbHistory.SelectedIndex];
            form1.LoadHistory(message);
        }
    }
}
