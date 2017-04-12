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
    public partial class NewUserForm : Form
    {
        public NewUserForm()
        {
            InitializeComponent();

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            ListDB DB = new ListDB();

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if ((!(username.Text.Equals("")) && (!password.Text.Equals(""))))
            {
                //if user does not exist already
                if (await ListDB.checkUserExists(username.Text) == false)
                {
                    //hash password
                    var pwdHash = ListDB.hashIt(password.Text);
                    //create user
                    ListDB.newUser(username.Text, pwdHash);
                    //return to login form
                    Close();
                }
                else
                    MessageBox.Show("That username has already been taken. Please try another.");
            }
            else
                MessageBox.Show("The username or password field is empty.");
        }
    }  
}
