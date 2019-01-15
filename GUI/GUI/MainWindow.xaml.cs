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
using MsDbLibraryNS.MsDbNS.SetterNS;
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

        //Запись в базу
        private void cellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            var editedCheckbox = e.EditingElement as CheckBox;
           
            if (editedCheckbox != null)
            {
                bool flag = editedCheckbox.IsChecked.Value;

                DataGridRow CurRow = e.Row;
                object data = CurRow.Item;
                DataRowView drv = data as DataRowView;

                //MessageBox.Show(drv[3].ToString());
                //MessageBox.Show(flag.ToString());

                MsDbSetter MsDbSetter = new MsDbSetter();
                MsDbSetter.SetNotifyEnableByPupilIdOld(Convert.ToInt32(drv[3]), flag);
            }
                
        }

        //Запрет редактирования всех полей кроме чекбоксов
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Данные успешно сохранены");
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
