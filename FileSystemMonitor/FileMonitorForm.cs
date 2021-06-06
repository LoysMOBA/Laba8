using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using IMT;
using System.Xml;
using Newtonsoft.Json;
using serilize = FileSystemMonitor.FormJson.serilize;
using System.Xml.Serialization;
using System.Xml.Linq;

namespace FileSystemMonitor
{
    public partial class FileMonitorForm : Form
    {

        public FileMonitorForm()
        {
            InitializeComponent();
        }


        private void FileMonitorForm_Load(object sender, EventArgs e)
        {
            //Задаем объект, используемый для маршалинга вызовов обработчика
            //событий, инициированных в результате изменения каталога.
            fileSystemWatcher1.SynchronizingObject = this;
            //Событие возникающее при изменении файла или каталога в заданном пути.
            fileSystemWatcher1.Changed += new FileSystemEventHandler(LogFileSystemChanges);
            //Событие возникающее при создании файла или каталога в заданном пути.
            fileSystemWatcher1.Created += new FileSystemEventHandler(LogFileSystemChanges);
            //Событие возникающее при удалении файла или каталога в заданном пути.
            fileSystemWatcher1.Deleted += new FileSystemEventHandler(LogFileSystemChanges);
            //Событие возникающее при переименовании файла или каталога в заданном пути.
            fileSystemWatcher1.Renamed += new RenamedEventHandler(LogFileSystemRenaming);

            fileSystemWatcher2.SynchronizingObject = this;
            //Событие возникающее при изменении файла или каталога в заданном пути.
            fileSystemWatcher2.Changed += new FileSystemEventHandler(LogFileSystemChanges);
            //Событие возникающее при создании файла или каталога в заданном пути.
            fileSystemWatcher2.Created += new FileSystemEventHandler(LogFileSystemChanges);
            //Событие возникающее при удалении файла или каталога в заданном пути.
            fileSystemWatcher2.Deleted += new FileSystemEventHandler(LogFileSystemChanges);
            //Событие возникающее при переименовании файла или каталога в заданном пути.
            fileSystemWatcher2.Renamed += new RenamedEventHandler(LogFileSystemRenaming);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Создание класса для вывода окна выбора директории
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();

            if (result == DialogResult.OK)
            {
                textBox1.Text = fbd.SelectedPath;
            }
        }

        void LogFileSystemRenaming(object sender, RenamedEventArgs e)
        {
            string log = string.Format("{0:G} | {1} | Переименован файл {2}", DateTime.Now, e.FullPath, e.OldName);
            //Добавляем новую запись события.
            listBox1.Items.Add(log);
            //Выбираем последнюю добавленную запись.
            listBox1.SelectedIndex = listBox1.Items.Count - 1;

            XDocument xdoc = XDocument.Load(@"C:\Users\ggggg\Downloads\FileSystemMonitor\FileSystemMonitor\FileSystemMonitor\XMLFile1.xml");
            XElement root = xdoc.Element("root");
            root.Add(new XElement("Log", log));
            xdoc.Save(@"C:\Users\ggggg\Downloads\FileSystemMonitor\FileSystemMonitor\FileSystemMonitor\XMLFile1.xml");
         

            List<serilize> _data = new List<serilize>();
            _data.Add(new serilize("Логи", log));
            string json = JsonConvert.SerializeObject(_data.ToArray());
            File.AppendAllText(@"C:\Users\ggggg\Downloads\FileSystemMonitor\FileSystemMonitor\FileSystemMonitor\json1.json", json);
        }

        void LogFileSystemChanges(object sender, FileSystemEventArgs e)
        {
            string log = string.Format("{0:G} | {1} | {2}", DateTime.Now, e.FullPath, e.ChangeType);
            //Добавляем новую запись события.
            listBox1.Items.Add(log);
            //Выбираем последнюю добавленную запись.
            listBox1.SelectedIndex = listBox1.Items.Count - 1;
            
            XDocument xdoc = XDocument.Load(@"C:\Users\ggggg\Downloads\FileSystemMonitor\FileSystemMonitor\FileSystemMonitor\XMLFile1.xml");
            XElement root = xdoc.Element("root");
            root.Add(new XElement("Log", log));
            xdoc.Save(@"C:\Users\ggggg\Downloads\FileSystemMonitor\FileSystemMonitor\FileSystemMonitor\XMLFile1.xml");

            List<serilize> _data = new List<serilize>();
            _data.Add(new serilize("Логи", log)); 

            string json = JsonConvert.SerializeObject(_data.ToArray());

            File.AppendAllText(@"C:\Users\ggggg\Downloads\FileSystemMonitor\FileSystemMonitor\FileSystemMonitor\json1.json", json+"\n");

        }

        private void MonitoringInput_CheckedChanged(object sender, EventArgs e)
        {
            //Получаем путь к выбранному каталогу.
            string monitoredFolder = textBox1.Text;

            //на существующий каталог на диске.
            bool folderExists = Directory.Exists(monitoredFolder);

            //Проверка существования директории.
            if (folderExists)
            {
                //Задаем путь отслеживаемого каталога.
                fileSystemWatcher1.Path = monitoredFolder;

                //Задаем строку фильтра, используемую для определения файлов,
                //контролируемых в каталоге. 
                //По умолчанию используется "*.*" (отслеживаются все файлы).       
                fileSystemWatcher1.Filter = textBox2.Text;

                //Отслеживаемые изменения.
                NotifyFilters notificationFilters = new NotifyFilters();
                //Атрибуты файла и папки.
                if (checkBox2.Checked) notificationFilters = notificationFilters
                    | NotifyFilters.Attributes;
                //Время создания файла и папки.
                if (checkBox3.Checked) notificationFilters = notificationFilters
                    | NotifyFilters.CreationTime;
                //Имя каталога.
                if (checkBox4.Checked) notificationFilters = notificationFilters
                    | NotifyFilters.DirectoryName;
                //Имя файла
                if (checkBox5.Checked) notificationFilters = notificationFilters
                    | NotifyFilters.FileName;
                //Дата последнего открытия файла или папки.
                if (checkBox6.Checked) notificationFilters = notificationFilters
                    | NotifyFilters.LastAccess;
                //Дата последней записи в файл или папку.
                if (checkBox7.Checked) notificationFilters = notificationFilters
                    | NotifyFilters.LastWrite;
                //Параметры безопасности файла или папки.
                if (checkBox8.Checked) notificationFilters = notificationFilters
                    | NotifyFilters.Security;
                //Размер файла или папки.
                if (checkBox9.Checked) notificationFilters = notificationFilters
                    | NotifyFilters.Size;
                //Задаем тип отслеживаемых изменений.
                fileSystemWatcher1.NotifyFilter = notificationFilters;

                //Задаем значение, определяющее, доступен ли данный компонент.
                fileSystemWatcher1.EnableRaisingEvents = checkBox1.Checked;
            }
            else if (checkBox1.Checked)
            {
                MessageBox.Show(this, "Выбранная директория не существует!", "Мониторинг", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Выключаем мониторинг при ошибке.
                checkBox1.Checked = false;
            }
            string monitoredFolder2 = textBox3.Text;

            bool folderExists2 = Directory.Exists(monitoredFolder2);
            if (folderExists2)
            {
                //Задаем путь отслеживаемого каталога.
                fileSystemWatcher2.Path = monitoredFolder2;

                //Задаем строку фильтра, используемую для определения файлов,
                //контролируемых в каталоге. 
                //По умолчанию используется "*.*" (отслеживаются все файлы).       
                fileSystemWatcher2.Filter = textBox2.Text;


                //Отслеживаемые изменения.
                NotifyFilters notificationFilters2 = new NotifyFilters();
                //Атрибуты файла и папки.
                if (checkBox2.Checked) notificationFilters2 = notificationFilters2
                    | NotifyFilters.Attributes;
                //Время создания файла и папки.
                if (checkBox3.Checked) notificationFilters2 = notificationFilters2
                    | NotifyFilters.CreationTime;
                //Имя каталога.
                if (checkBox4.Checked) notificationFilters2 = notificationFilters2
                    | NotifyFilters.DirectoryName;
                //Имя файла
                if (checkBox5.Checked) notificationFilters2 = notificationFilters2
                    | NotifyFilters.FileName;
                //Дата последнего открытия файла или папки.
                if (checkBox6.Checked) notificationFilters2 = notificationFilters2
                    | NotifyFilters.LastAccess;
                //Дата последней записи в файл или папку.
                if (checkBox7.Checked) notificationFilters2 = notificationFilters2
                    | NotifyFilters.LastWrite;
                //Параметры безопасности файла или папки.
                if (checkBox8.Checked) notificationFilters2 = notificationFilters2
                    | NotifyFilters.Security;
                //Размер файла или папки.
                if (checkBox9.Checked) notificationFilters2 = notificationFilters2
                    | NotifyFilters.Size;
                //Задаем тип отслеживаемых изменений.
                fileSystemWatcher2.NotifyFilter = notificationFilters2;

                //Задаем значение, определяющее, доступен ли данный компонент.
                fileSystemWatcher2.EnableRaisingEvents = checkBox1.Checked;
            }
            else if (checkBox1.Checked)
            {
                MessageBox.Show(this, "Выбранная директория не существует!", "Мониторинг", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Выключаем мониторинг при ошибке.
                checkBox1.Checked = false;
            }
            //После запуске мониторинга, блогируем элементы управления.
            textBox1.Enabled = textBox2.Enabled =
                NotificationsGroup.Enabled = !checkBox1.Checked;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string monitoredFolder = textBox1.Text;
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();

            if (result == DialogResult.OK)
            {
                textBox3.Text = fbd.SelectedPath;
            }
            string ToDir = textBox3.Text;
            SyncDir(monitoredFolder, ToDir);
        }
        static void SyncDir(string FromDir, string ToDir)
        {
            Directory.CreateDirectory(ToDir);

            foreach (string s1 in Directory.GetFiles(ToDir))
            {
                string s2 = FromDir + "\\" + Path.GetFileName(s1);
                if (!File.Exists(s2))
                {
                    File.Delete(s1);
                }
            }

            foreach (string s1 in Directory.GetFiles(FromDir))
            {
                string s2 = ToDir + "\\" + Path.GetFileName(s1);
                if (!File.Exists(s2))
                {
                    File.Copy(s1, s2);
                }
                else
                {
                    FileInfo fi1 = new FileInfo(s1);
                    FileInfo fi2 = new FileInfo(s2);
                    if (fi1.LastWriteTime != fi2.LastWriteTime)
                    {
                        File.Delete(s2);
                        File.Copy(s1, s2);
                    }
                }
            }

            foreach (string s in Directory.GetDirectories(FromDir))
            {
                SyncDir(s, ToDir + "\\" + Path.GetFileName(s));
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormXML newForm = new FormXML();
            newForm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormJson newForm = new FormJson();
            newForm.Show();
        }

        private void ControlGroup_Enter(object sender, EventArgs e)
        {

        }
    }
}
