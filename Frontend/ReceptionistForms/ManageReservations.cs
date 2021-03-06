using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Frontend.ReceptionistForms
{
    public partial class ManageReservations : Form
    {
        public ManageReservations()
        {
            InitializeComponent();
        }

        private void addReservationButton_Click(object sender, EventArgs e)
        {
            AddReservation addReservation = new AddReservation();
            addReservation.Show();
        }

        private void editOrDeleteReservationButton_Click(object sender, EventArgs e)
        {
            EditOrDeleteReservation editOrDeleteReservation = new EditOrDeleteReservation();
            editOrDeleteReservation.Show();
        }

        private void ActiveReservationButton_Click(object sender, EventArgs e)
        {
            ListActiveReservations listActiveReservations = new ListActiveReservations();
            listActiveReservations.Show();
        }
    }
}
