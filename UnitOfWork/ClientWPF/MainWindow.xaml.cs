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
using UnitOfWork.Model;
using UnitOfWork.Infrastructure;
using UnitOfWork.Repository;
using StructureMap;

namespace ClientWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            BootStraper.ConfigureStructureMap();

            this.puniBoxove();

        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {


            //IUnitOfWork uof = new UnitOfWork.Infrastructure.UnitOfWork();
            //IRacunRepository repo = new RacunRepository(uof);
            //RacunService RacunService = new RacunService(repo, uof);

            RacunService RacunService = new RacunService(ObjectFactory.GetInstance<IRacunRepository>(), ObjectFactory.GetInstance<IUnitOfWork>());

            int id1 =Convert.ToInt16(combo1.SelectedItem.ToString().Split('-')[0].ToString());
            int id2 = Convert.ToInt16(combo2.SelectedItem.ToString().Split('-')[0].ToString());
            int novac = Int16.Parse(txtBox.Text);
            Racun Racun1 = RacunService.getRacun(id1);
            Racun Racun2 = RacunService.getRacun(id2);
            
            RacunService.Transfer(Racun1, Racun2, novac);

            MessageBox.Show("Transakcija je uspešno obavljena.", "Porukica", MessageBoxButton.OKCancel, MessageBoxImage.Information);
            puniBoxove();

            //combo1.Items.Refresh();

            

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }


        private void puniBoxove() {

            RacunService RacunService = new RacunService(ObjectFactory.GetInstance<IRacunRepository>(), ObjectFactory.GetInstance<IUnitOfWork>());

            IEnumerable<Racun> racuni = RacunService.GetRacuni();

            List<string> lista=  new List<string>();

            foreach (Racun item in racuni)
            {
               lista.Add( item.Id + "-" + item.StanjeRacuna );
                 
            }
            combo1.ItemsSource = lista;
            combo2.ItemsSource = lista;
        }

    }

}
