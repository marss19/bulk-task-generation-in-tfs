using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace Marss.TasksGenerator.BLL.TFS
{
    public class TfsConnector
    {
        public TfsDataProvider ConnectToTfsUsingTeamProjectPicker()
        {
            var picker = new TeamProjectPicker(TeamProjectPickerMode.SingleProject, false);
            if (picker.ShowDialog() == DialogResult.OK)
            {
                var tfs = picker.SelectedTeamProjectCollection;
                if (picker.SelectedProjects != null && picker.SelectedProjects.Length > 0)
                {
                    var project = picker.SelectedProjects[0];
                    return new TfsDataProvider(tfs.GetService<WorkItemStore>(), project.Name, picker.SelectedTeamProjectCollection.ConfigurationServer.Name);
                }
            }

            return null;
        }
    }
}
