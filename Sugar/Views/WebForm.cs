using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;

namespace Sugar.Views
{
    public partial class WebForm : Form
    {
        ChromiumWebBrowser m_chromeBrowser = null;

        public WebForm()
        {
            InitializeComponent();
        }

        public static string GetAppLocation()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        private void WebForm_Load(object sender, EventArgs e)
        {
            var page = new Uri(string.Format("file:///{0}HTMLResources/html/BootstrapExample.html", GetAppLocation()));
            ChromiumWebBrowser myBrowser = new ChromiumWebBrowser(page.ToString());
            this.Controls.Add(myBrowser);
        }
    }
}
