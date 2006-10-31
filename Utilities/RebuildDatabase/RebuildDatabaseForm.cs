using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using ClearCanvas.Controls.WinForms;

namespace ClearCanvas.Utilities.RebuildDatabase
{
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using System.IO;
	using Crownwood.DotNetMagic.Forms;

    public partial class RebuildDatabaseForm : DotNetMagicForm
    {
        delegate void UpdateProgressBarDelegate(ImageInsertCompletingEventArgs args);
        delegate void EndRebuildDelegate(DatabaseRebuildCompletedEventArgs args);

        public RebuildDatabaseForm()
        {
            InitializeComponent();
            Binding bindingImageStoragePath = new Binding("Text", Properties.Settings.Default, "ImageStoragePath", false, DataSourceUpdateMode.OnPropertyChanged);
            Binding bindingRecursiveSearch = new Binding("Checked", Properties.Settings.Default, "RecursiveSearch", false, DataSourceUpdateMode.OnPropertyChanged);
            _statusGroupBox.Text = "Idle";
            _imageFolderText.DataBindings.Add(bindingImageStoragePath);
            _findFilesRecursivelyCheckbox.DataBindings.Add(bindingRecursiveSearch);
            this.CancelButton = _exitButton;
            _stopButton.Enabled = false;
        }

        private void BrowseClick(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.Description = "Choose the folder where images are stored";
            folderDialog.RootFolder = Environment.SpecialFolder.MyComputer;
          
            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                _imageFolderText.Text = folderDialog.SelectedPath;
            }
        }

        private void RebuildDatabaseFormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void ExitButtonClick(object sender, EventArgs e)
        {
            if (null != _rebuilder && _rebuilder.IsRebuilding)
                _rebuilder.StopRebuild();
            Close();
        }

        private void StartButtonClick(object sender, EventArgs e)
        {
            _statusGroupBox.Text = "Initializing...";
            this.Refresh();
            using (new CursorManager(this, Cursors.WaitCursor))
            {
                _rebuilder = new DatabaseRebuilder(_imageFolderText.Text, _findFilesRecursivelyCheckbox.Checked);
                _rebuilder.ImageInsertCompletingEvent += delegate(object source, ImageInsertCompletingEventArgs args)
                    {
                        this.BeginInvoke((MethodInvoker) delegate() { UpdateProgressBar(args); });
                    };
                _rebuilder.DatabaseRebuildCompletedEvent += DatabaseRebuildCompletedEventHandler;
            }

            // set up progress bar
            _progressBar.Visible = true;
            _progressBar.Minimum = 1;
            _progressBar.Maximum = _rebuilder.NumberOfFiles;
            _progressBar.Value = 1;
            _progressBar.Step = 1;

            _browseForFolderButton.Enabled = false;
            _imageFolderText.Enabled = false;
            _startButton.Enabled = false;
            _findFilesRecursivelyCheckbox.Enabled = false;
            _stopButton.Enabled = true;
            _exitButton.Enabled = false;
            _startTime = DateTime.Now;

            _statusGroupBox.Text = "Rebuilding...";
            _rebuilder.StartRebuild();
        }

        public void ImageInsertCompletingEventHandler(Object source, ImageInsertCompletingEventArgs args)
        {
            
        }

        public void DatabaseRebuildCompletedEventHandler(Object source, DatabaseRebuildCompletedEventArgs args)
        {
            BeginInvoke(new EndRebuildDelegate(EndRebuild), new object[] { args });
        }

        private void StopButtonClick(object sender, EventArgs e)
        {
            _rebuilder.StopRebuild();
        }

        private void UpdateProgressBar(ImageInsertCompletingEventArgs args)
        {
            _progressBar.PerformStep();
            _statusTextLabel.Text = args.FileName.Substring(args.FileName.LastIndexOf('\\') + 1);
        }

        private void EndRebuild(DatabaseRebuildCompletedEventArgs args)
        {
            if (!args.RebulidWasAborted)
            {
                _stopTime = DateTime.Now;
                TimeSpan duration = _stopTime - _startTime;
                _statusGroupBox.Text = "Processed " + _rebuilder.NumberOfFiles.ToString() + " files in " + duration.ToString();
            }
            else
            {
                _statusGroupBox.Text = "Rebuild was aborted";
            }

            _browseForFolderButton.Enabled = true;
            _imageFolderText.Enabled = true;
            _findFilesRecursivelyCheckbox.Enabled = true;
            _startButton.Enabled = true;
            _stopButton.Enabled = false;
            _exitButton.Enabled = true;

        }

        private DateTime _startTime;
        private DateTime _stopTime;
        private DatabaseRebuilder _rebuilder = null;
    }
}