using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Marss.TasksGenerator
{
    public partial class AddTasksProgressForm : Form
    {
        public AddTasksProgressForm()
        {
            InitializeComponent();
        }

       
        public void ExecuteAction(Form parentForm, Action longRunningAction)
        {
            StartPosition = FormStartPosition.CenterParent;
            _longRunningAction = longRunningAction;

            ShowDialog(parentForm);
            if (_lastError != null)
                throw new Exception(_lastError);
        }

        #region private

        BackgroundWorker _bgWorker = new BackgroundWorker();
        private Action _longRunningAction;
        private string _lastError;

        private void AddTasksProgressForm_Shown(object sender, EventArgs e)
        {
            _bgWorker = new BackgroundWorker();
            _bgWorker.DoWork += _bgWorker_DoWork;
            _bgWorker.RunWorkerCompleted += _bgWorker_RunWorkerCompleted;
            _bgWorker.RunWorkerAsync();
        }

        void _bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            if (e.Error != null)
            {
                _lastError = e.Error.Message;
            }
            Close();
                       
        }

        void _bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            _longRunningAction();
        }
        
        #endregion
    }
}
