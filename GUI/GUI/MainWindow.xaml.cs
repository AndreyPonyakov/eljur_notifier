using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using MsDbLibraryNS.MsDbNS.RequesterNS;
using MsDbLibraryNS.StaffModel;
using System.Threading;

namespace GUI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }


        //Загрузка содержимого таблицы
        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            List<MyTable> result = new List<MyTable>();
            MsDbRequester MsDbRequester = new MsDbRequester();
            var allPupils = MsDbRequester.getListAllPupils();


            foreach (Pupil p in allPupils)
            {
                MyTable MyTable = new MyTable(p.EljurAccountId, p.FullFIO, p.Clas, p.PupilIdOld, p.NotifyEnable);
                result.Add(MyTable);
            }

            DataTable table = new DataTable();
            table.Columns.Add("ИД ЭлЖур", typeof(int));
            table.Columns.Add("Ученик", typeof(string));
            table.Columns.Add("Класс", typeof(string));
            table.Columns.Add("СкудИд", typeof(int));
            table.Columns.Add("Уведомлять", typeof(bool));

            foreach (MyTable t in result)
            {
                DataRow row = table.NewRow();
                row["ИД ЭлЖур"] = t.IdEljur;
                row["Ученик"] = t.FIO;
                row["Класс"] = t.Class;
                row["СкудИд"] = t.PupilIdOld;
                row["Уведомлять"] = t.NotifyEnable;

                table.Rows.Add(row);
            }

            PupulsDataGrid.ItemsSource = table.DefaultView;

            PupulsDataGrid.BeginningEdit += DataGrid_BeginningEdit;

        }

        private void cellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {

            //MessageBox.Show("ddsjkfgsd");
            //Only handles cases where the cell contains a TextBox
            var editedCheckbox = e.EditingElement as CheckBox;

            if (editedCheckbox != null)
            {
                bool flag = editedCheckbox.IsChecked.Value;

                MessageBox.Show(flag.ToString());
                //MessageBox.Show("Value after edit: " + editedCheckbox.Content.ToString());
                //MessageBox.Show("Value after edit: " + editedCheckbox.ToString());
            }
                
        }


        private void PupulsDataGrid_GotFocus(object sender, RoutedEventArgs e)
        {
            DataGridCell cell = e.OriginalSource as DataGridCell;
            if (cell != null && cell.Column is DataGridCheckBoxColumn)
            {
                PupulsDataGrid.BeginEdit();
                CheckBox chkBox = cell.Content as CheckBox;
                if (chkBox != null)
                {
                    chkBox.IsChecked = !chkBox.IsChecked;
                    MessageBox.Show(chkBox.IsChecked.ToString());
                }
            }
        }




        //private void PupulsDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{

        //    MessageBox.Show(e.RowIndex.ToString() + " and " + e.ColumnIndex.ToString());

        //}


        //void DataGrid_CurrentCellChanged(object sender, DataGridViewCellEventArgs e)
        //{
        //    DataRowView drv = PupulsDataGrid.CurrentCell.Item as DataRowView;
        //    if (drv != null)
        //    {
        //        MessageBox.Show(drv[4].ToString());
        //    }

        //}



        private void PupulsDataGrid_CurrentCellChanged(object sender, EventArgs e)
        {
            DataRowView drv = PupulsDataGrid.CurrentCell.Item as DataRowView;
            if (drv != null)
            {
                //MessageBox.Show(drv[4].ToString());
                //textBox1.Text = drv.Row[0].ToString();
                //textBox2.Text = drv.Row[1].ToString();
            }
        }


        //good
        private static void DataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            //Actual content of the DataGridCell
            FrameworkElement content = e.Column.GetCellContent(e.Row);
            //MessageBox.Show(content.GetType().ToString());
            string type = content.GetType().ToString();

            if (type != "System.Windows.Controls.CheckBox")
            {
                e.Cancel = true;
            }     

        }


        //Получаем данные из таблицы
        private void DataGrid_MouseUp(object sender, SelectionChangedEventArgs e)
        {
            DataRowView drv = PupulsDataGrid.CurrentCell.Item as DataRowView;
            DataTable path = PupulsDataGrid.SelectedItem as DataTable;
            //MessageBox.Show(" ИдЭлЖур: " + path.Rows[0] + "\n ФИО: " + path.Rows[0] + "\n Класс: " + path.Rows[0]
            //    + "\n СкудИД: " + path.Rows[0] + "\n Уведомления: " + path.Rows[0]);
            //MessageBox.Show(drv[4].ToString());
        }

    }

    class MyTable
    {
        public MyTable(int IdEljur, string FIO, string Class, int PupilIdOld, bool NotifyEnable)
        {
            this.IdEljur = IdEljur;
            this.FIO = FIO;
            this.Class = Class;
            this.PupilIdOld = PupilIdOld;
            this.NotifyEnable = NotifyEnable;
        }
        public int IdEljur { get; set; }
        public string FIO { get; set; }
        public string Class { get; set; }
        public int PupilIdOld { get; set; }
        public bool NotifyEnable { get; set; }
    }


}
