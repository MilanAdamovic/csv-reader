using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.IO;


namespace CodeWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        void lFnLoadFileData()
        {
            try
            {
                Microsoft.Win32.OpenFileDialog lObjFileDlge = new Microsoft.Win32.OpenFileDialog();
                lObjFileDlge.Filter = "csv files |*.csv";
                lObjFileDlge.FilterIndex = 1;
                lObjFileDlge.Multiselect = false;
                string fName = "";
                bool? lBlnUserclicked = lObjFileDlge.ShowDialog();
                if (lBlnUserclicked != null || lBlnUserclicked == true)
                {
                    fName = lObjFileDlge.FileName;
                }
                if (System.IO.File.Exists(fName) == true)
                {

                    StreamReader lObjStreamReader = new StreamReader(fName);
                    System.Windows.MessageBox.Show(lObjStreamReader.ToString());
                    lFnGenerateData(lObjStreamReader);
                    lObjStreamReader.Close();
                }
            }
            catch (Exception)
            {
                return;
            }

        }




        void lFnGenerateData(StreamReader aReader)
        {
            try
            {
                bool lBlnIsColumns = true;
                string[] lArrCols = null;
                List<NameList> lstInterview = new List<NameList>();
                dataGrid1.Columns.Clear();
                while (aReader.Peek() > 0)
                {
                    string lStrLine = aReader.ReadLine();
                    if (lStrLine == null)
                        break;
                    if (lStrLine.Trim() == "")
                        continue;
                    string[] lArrStrCells = null;
                    lArrStrCells = lStrLine.Split(',');
                    if (lArrStrCells == null)
                        continue;
                    if (lBlnIsColumns)
                    {
                        lArrCols = lArrStrCells;
                        foreach (string lStrCell in lArrStrCells)
                        {
                            DataGridTextColumn lDGCol = new DataGridTextColumn();
                            lDGCol.Header = lStrCell;
                            lDGCol.Binding = new System.Windows.Data.Binding(lStrCell);
                            dataGrid1.Columns.Add(lDGCol);
                        }
                        lBlnIsColumns = false;
                        continue;
                    }
                    if (lArrCols == null)
                        continue;
                    {

                        NameList interView = new NameList();

                        interView.Gender = lArrStrCells[0];
                        interView.Title = lArrStrCells[1];
                        interView.Occupation = lArrStrCells[2];
                        interView.Company = lArrStrCells[3];
                        interView.GivenName = lArrStrCells[4];
                        interView.MiddleInitial = lArrStrCells[5];
                        interView.Surname = lArrStrCells[6];
                        interView.BloodType = lArrStrCells[7];
                        interView.EmailAddress = lArrStrCells[8];

                        lstInterview.Add(interView);

                    }
                    aReader.Close();
                    dataGrid1.ItemsSource = lstInterview;
                }
            }
            catch (Exception)
            {

                return;
            }
        }

        private void buttonLoad_Click(object sender, RoutedEventArgs e)
        {
            lFnLoadFileData();
        }
    }
}
