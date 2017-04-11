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

        private void button1_Click(object sender, EventArgs e)
        {
            if ((!(username.Text.Equals("")) && (!password.Text.Equals(""))))
            {
                //if user does not exist already
                ListDB.User user = new ListDB.User();
                user.Name = username.Text;
                //get password, hash it, store hash in db
                var data = System.Text.Encoding.UTF8.GetBytes(password.Text);
                data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
                user.pwdHash = "";
                foreach (byte b in data)
                {
                    user.pwdHash += b.ToString("X2");
                }
                MessageBox.Show(user.Name + "\n" + user.pwdHash);
            }
            else
                MessageBox.Show("The username or password field is empty.");
        }
    }  
}
