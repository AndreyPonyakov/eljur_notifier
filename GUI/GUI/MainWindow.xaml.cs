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

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            DataSet ds = new DataSet();
            MsDbRequester MsDbRequester = new MsDbRequester();

            var AllPupils = MsDbRequester.getListAllPupils();
            //MessageBox.Show(AllPupils.Count.ToString());

            DataTable table = new DataTable();
            CreateTable(table, AllPupils);

            PupulsDataGrid.ItemsSource = table.DefaultView;

        }

        public void CreateTable(DataTable table, List<Pupil> allPupils)
        {
            table.Columns.Add("ИД ЭлЖур", typeof(object));
            table.Columns.Add("Ученик", typeof(object));
            table.Columns.Add("Класс", typeof(object));
            table.Columns.Add("СкудИд", typeof(object));
            table.Columns.Add("Уведомлять", typeof(object));

            foreach (Pupil p in allPupils)
            {
                DataRow row = table.NewRow();
                row["ИД ЭлЖур"] = p.EljurAccountId;
                row["Ученик"] = p.FullFIO;
                row["Класс"] = p.Clas;
                row["СкудИд"] = p.PupilIdOld;
                row["Уведомлять"] = p.NotifyEnable;       
                table.Rows.Add(row);
            }
        }





    }
}
