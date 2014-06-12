using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Windows.Forms;

namespace FolderRead
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbdDialog = new FolderBrowserDialog();

            fbdDialog.ShowDialog();

            string[] files = Directory.GetFiles(fbdDialog.SelectedPath);

            string folderPath = fbdDialog.SelectedPath + "\\FileNameFolder";

            if (!Directory.Exists(fbdDialog.SelectedPath + "\\FileNameFolder"))
            {
                Directory.CreateDirectory(folderPath);
            }

            foreach (var item in files)
            {
                string[] fileName = item.Split('\\');
                string[] file = fileName[2].Split('.');
                File.AppendAllText(folderPath + "\\files.txt", file[0] + Environment.NewLine);    
            }

            System.Windows.MessageBox.Show("Done!");
        }

        private void btnCreateXml_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofdFileDialog = new OpenFileDialog();

            if (ofdFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                string[] fileNames = File.ReadAllLines(ofdFileDialog.FileName);

                string folderPath = @"C:\EstructurasSiteMap";

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string xmlStructure = "";

                if (chkHaveUrls.IsChecked.Value)
                {
                    foreach (var item in fileNames)
                    {
                        xmlStructure = "<url><loc>" + item + "</loc><lastmod>2014-06-12T10:00:00+00:00</lastmod><changefreq>daily</changefreq><priority>0.50</priority></url>";

                        File.AppendAllText(folderPath + "\\xmlSiteMapUrl.txt", xmlStructure + Environment.NewLine);
                    }
                }
                else
                {
                    foreach (var item in fileNames)
                    {
                        xmlStructure = "<url><loc>http://www.segurosbroker.com/seguros/vehiculos/" + item + "/</loc><lastmod>2014-06-12T10:00:00+00:00</lastmod><changefreq>daily</changefreq><priority>0.50</priority></url>";

                        File.AppendAllText(folderPath + "\\xmlSiteMap.txt", xmlStructure + Environment.NewLine);
                    }
                }

                System.Windows.MessageBox.Show("Done!");
            }
        }
    }
}
