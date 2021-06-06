using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Newtonsoft.Json;
using System.IO;
using IMT;

namespace FileSystemMonitor
{
    public partial class FormXML : Form
    {
        private string configFile = $"C:\\Users\\ggggg\\OneDrive\\Рабочий стол\\Работа";
        private void LoadS()
        {
            XmlDocument doc = new XmlDocument();
            string FilePath = @"C:\Users\ggggg\Downloads\IMT-20210530T211611Z-001\IMT\XMLFile1.xml";
            doc.LoadXml(System.IO.File.ReadAllText(FilePath));

            foreach (XmlNode node in doc.DocumentElement)
            {
                string way = node.Attributes[0].Value;
                string status = node.Attributes[0].Value;
                listBox1.Items.Add(new serilize(way, status));
            }
        }


        public FormXML()
        {
            InitializeComponent();
            LoadS();
        }
    }
}

