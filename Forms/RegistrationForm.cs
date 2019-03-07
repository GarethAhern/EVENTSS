using EventsAndInvitesLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventsAndInvitesUI
{
    public partial class RegistrationForm : Form
    {
        public RegistrationForm()
        {
            InitializeComponent();
        }

        private void previewRegistrationEmailButton_Click(object sender, EventArgs e)
        {

            //TODO - put tests into make sure i should send email

            //EmailLogic.SendEmail();
          
        }
    }
}
