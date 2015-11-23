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
using System.Windows.Threading;

namespace WpfAlyTalo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Lights Keittio = new Lights();
        public Lights OloHuone = new Lights();
        public Sauna OmaSauna = new Sauna();
        public Thermostat TalonLampo = new Thermostat();
        public char AsteMerkki;
        public DispatcherTimer SaunaTimer = new System.Windows.Threading.DispatcherTimer(); //Saunan lämpötilan kasvatus, kun sauna on päällä


        public MainWindow()
        {
            InitializeComponent();
            Keittio.Dimmer = "0";
            Keittio.Switched = false;

            OloHuone.Dimmer = "0";
            OloHuone.Switched = false;
            txtValotilaOlohuone.Text = "Off";

            OmaSauna.AsetaSauna(0);
            txtSaunatila.Text = "";

            AsteMerkki = Convert.ToChar(176); //http://stackoverflow.com/questions/1536131/celsius-symbol-in-richtextbox
            txtTalonLampotila.Text = "21" + AsteMerkki; 
            txtUusiLampotila.Text = "";

            lblLampotila.Content = "";
            //Timerin asetukset
            SaunaTimer.Tick += SaunanLampo_Tick;
            SaunaTimer.Interval = new TimeSpan(0, 0, 1);

        }

        private void SaunanLampo_Tick(object sender, EventArgs e)
        {
            OmaSauna.SaunaTemperature = OmaSauna.SaunaTemperature + 0.32;
            lblLampotila.Content = OmaSauna.SaunaTemperature.ToString() + AsteMerkki;
        }

        private void bOlohuonePois_Click(object sender, RoutedEventArgs e)
        {
            OloHuone.Switched = false;
            OloHuone.Dimmer = "Off";
            txtValotilaOlohuone.Text = OloHuone.Dimmer;

        }

        private void bOlohuoneHimmea_Click(object sender, RoutedEventArgs e)
        {
            OloHuone.Switched = true;
            OloHuone.Dimmer = "33%";
            txtValotilaOlohuone.Text = OloHuone.Dimmer;
        }

        private void bOlohuonePuolivalot_Click(object sender, RoutedEventArgs e)
        {
            OloHuone.Switched = true;
            OloHuone.Dimmer = "66%";
            txtValotilaOlohuone.Text = OloHuone.Dimmer;
        }

        private void bOlohuoneKirkas_Click(object sender, RoutedEventArgs e)
        {
            OloHuone.Switched = true;
            OloHuone.Dimmer = "100%";
            txtValotilaOlohuone.Text = OloHuone.Dimmer;
           
        }

        private void bKeittioPois_Click(object sender, RoutedEventArgs e)
        {
            Keittio.SwitchOff();
            txtValotilaKeittio.Text = Keittio.Dimmer;
        }

        private void bKeittioHimmea_Click(object sender, RoutedEventArgs e)
        {
            Keittio.SwitchOn(0);
            txtValotilaKeittio.Text = Keittio.Dimmer + "%";
        }

        private void bKeittioPuolivalot_Click(object sender, RoutedEventArgs e)
        {
            Keittio.SwitchOn(66);
            txtValotilaKeittio.Text = Keittio.Dimmer + "%";
        }

        private void bOlohuoneKirkas_Copy_Click(object sender, RoutedEventArgs e)
        {
            Keittio.SwitchOn(100);
            txtValotilaKeittio.Text = Keittio.Dimmer + "%";
        }

        private void bSaunanTila_Click(object sender, RoutedEventArgs e)
        {
            if (OmaSauna.Switched)
            {
                OmaSauna.AsetaSauna(0);
                txtSaunatila.Text = "";
                SaunaTimer.Stop();
                lblLampotila.Content = "";
            }
            else
            {
                OmaSauna.AsetaSauna(1);
                txtSaunatila.Text = "SAUNA PÄÄLLÄ";
                SaunaTimer.Start();
            }
        
        }

        private void bAsetaLampotila_Click(object sender, RoutedEventArgs e)
        {
            int UusiLampotila;
            try
            {
                UusiLampotila = Int32.Parse(txtUusiLampotila.Text);
                TalonLampo.LampotilanAsetus(UusiLampotila);
                txtTalonLampotila.Text = TalonLampo.Temperature.ToString() + AsteMerkki;
                txtUusiLampotila.Text = "";
            }
            catch
            {
                MessageBox.Show("Lämpötilan asettaminen epäonnistui! Vinkki: käytä numeroarvoa ;) ");
            }
        }
    }
}
