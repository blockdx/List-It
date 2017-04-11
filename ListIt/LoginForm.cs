using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ListIt
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            ListDB db = new ListDB();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //login
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //create new account
            //need to check if account already exists
            //usernames and passwords less than 12 chars
            NewUserForm createAccountForm = new NewUserForm();
            createAccountForm.ShowDialog();
        }
    }  
}
