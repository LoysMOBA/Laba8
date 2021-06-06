using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FileSystemMonitor
{
    public partial class FormJson : Form
    {
        private readonly string path = $"C:\\Users\\ggggg\\Downloads\\FileSystemMonitor\\FileSystemMonitor\\FileSystemMonitor\\json1.json";
        public FormJson()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string jsonFromFile;
                using (var reader = new StreamReader(path))
                {
                    jsonFromFile = reader.ReadToEnd();
                }

                richTextBox1.Text = jsonFromFile;

                var customerFromJson = JsonConvert.DeserializeObject<serilize>(jsonFromFile);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public class serilize
        {
            public string Way { get; private set; }
            public string Status { get; private set; }

            public serilize(string way, string status)
            {
                Way = way;
                Status = status;

            }
            public override string ToString()
            {
                return Way;
            }

        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
