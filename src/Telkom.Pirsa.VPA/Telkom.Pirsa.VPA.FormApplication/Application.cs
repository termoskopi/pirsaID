using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telkom.Pirsa.VPA.FormApplication.Controls;


namespace Telkom.Pirsa.VPA.FormApplication
{
    public partial class Application : Form
    {
        private UserControl activeControl = null;

        public Application()
        {
            InitializeComponent();
        }

        #region Helper
        private void Refresh(int row, int col, int rowspan, int colspan)
        {
            mainLayout.Controls.Clear();
            mainLayout.Controls.Add(activeControl, col, row);
            mainLayout.SetRowSpan(activeControl, rowspan);
            mainLayout.SetColumnSpan(activeControl, colspan);
            activeControl.Dock = DockStyle.Fill;
            mainLayout.Refresh();

        }
        #endregion

        private void LoadVideoClickAction(object sender, EventArgs e)
        {

        }

        private void Application_Load(object sender, EventArgs e)
        {

        }

        private void GeneralSettingClickAction(object sender, EventArgs e)
        {
            activeControl = new GeneralSettingUI();
            
            this.Refresh(1, 3, 1, 3);
        }
    }
}
