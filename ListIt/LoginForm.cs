﻿using System;
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

        private async void button1_Click(object sender, EventArgs e)
        {
            //log in
            //check if user exists
            if ((!(username.Text.Equals("")) && (!password.Text.Equals(""))))
            {
                if (await ListDB.checkUserExists(username.Text))
                {
                    if (await ListDB.logIn(username.Text, ListDB.hashIt(password.Text)))
                    {
                        //username and password match
                        MainForm.label1.Text = "Logged in as ' " + username.Text + " '";
                        MainForm.button7.Hide();
                        MainForm.button8.Show();
                        MainForm.LoggedIn = true;
                        Close();
                    }
                    else
                        MessageBox.Show("Wrong password!");
                    //password does not match
                }
                else
                    MessageBox.Show("That username does not exist.");
            }
            else
                MessageBox.Show("The username or password field is empty.");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            NewUserForm createAccountForm = new NewUserForm();
            createAccountForm.ShowDialog();
        }
    }
}