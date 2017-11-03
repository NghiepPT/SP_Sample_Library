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
//using SPSwrapApi;
using SPSwrapApi.Common;
using System.Net;
namespace SP_Sample_Library
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

        private void cmbSites_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cmbSites_Drop(object sender, DragEventArgs e)
        {

        }

        private void cmbSites_DropDownOpened(object sender, EventArgs e)
        {

            NetworkCredential credentials = new NetworkCredential(txtUserName.Text, password.Password);
            SPContext context = new SPContext(credentials, null, new Uri(txtServer.Text.ToString()));
            Web web = new Web(context, new Uri(txtServer.Text.ToString()));
            List<Site> sites = new List<Site>();
            sites = web.GetSites();
           cmbSites.Items.Clear();
            foreach (var site in sites)
            {
                cmbSites.Items.Add(site.Title);
            }
        }

        private void cmbLibrary_DropDownOpened(object sender, EventArgs e)
        {
            NetworkCredential nc = new NetworkCredential(txtUserName.Text, password.Password);
            SPContext context = new SPContext(nc, null, new Uri(txtServer.Text.ToString()));
            string sSiteUrl = txtServer.Text.ToString().TrimEnd('/') + "/" + cmbSites.Text.ToString();
            SPSwrapApi.Common.List list = new SPSwrapApi.Common.List(context, sSiteUrl);
            IEnumerable<SPSwrapApi.INode> nodes = list.Load();
            cmbLibrary.Items.Clear();
            foreach(var node in nodes)
            {
                if (node.NodeType == SPSwrapApi.NodeType.Library)
                {
                    cmbLibrary.Items.Add(node.Title);
                }
            }

        }
    }
}
