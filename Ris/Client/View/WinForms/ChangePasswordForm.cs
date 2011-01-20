#region License

// Copyright (c) 2011, ClearCanvas Inc.
// All rights reserved.
// http://www.clearcanvas.ca
//
// This software is licensed under the Open Software License v3.0.
// For the complete license, see http://www.clearcanvas.ca/OSLv3.0

#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ClearCanvas.Ris.Client.View.WinForms
{
    public partial class ChangePasswordForm : Form
    {
        public ChangePasswordForm()
        {
            InitializeComponent();
        }

        public string UserName
        {
            get { return _userName.Text; }
            set { _userName.Text = value; }
        }

        public string Password
        {
            get { return _password.Text; }
            set { _password.Text = value; }
        }

        public string NewPassword
        {
            get { return _newPassword.Text; }
        }

        private void _okButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void _cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void _password_TextChanged(object sender, EventArgs e)
        {
            UpdateButtonStates();
        }

        private void _newPassword_TextChanged(object sender, EventArgs e)
        {
            UpdateButtonStates();
        }

        private void _newPasswordConfirm_TextChanged(object sender, EventArgs e)
        {
            UpdateButtonStates();
        }

        private void UpdateButtonStates()
        {
            _errorProvider.SetError(_newPassword, 
                _newPassword.Text == _password.Text ? 
                    "New password must be different from previous" : null);

            _errorProvider.SetError(_newPasswordConfirm,
                _newPassword.Text != _newPasswordConfirm.Text ?
                    "New passwords do not match" : null);

            bool ok = !string.IsNullOrEmpty(_userName.Text) && !string.IsNullOrEmpty(_password.Text) &&
                      !string.IsNullOrEmpty(_newPassword.Text) && !string.IsNullOrEmpty(_newPasswordConfirm.Text) &&
                      _newPassword.Text.Equals(_newPasswordConfirm.Text);

            _okButton.Enabled = ok;
        }

        private void ChangePasswordForm_Load(object sender, EventArgs e)
        {
            // depending on use-case, the old password may already be filled in
            if (string.IsNullOrEmpty(_password.Text))
                _password.Select();
            else
                _newPassword.Select();
        }
    }
}