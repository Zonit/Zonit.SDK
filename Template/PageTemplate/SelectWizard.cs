using Microsoft.VisualStudio.TemplateWizard;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Zonit.Template.PageTemplate;

public class SelectWizard : IWizard
{
    public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
    {
        // Tworzymy okno wyboru scenariusza
        var scenarioChoice = new List<string> { "IAreaWeb", "IAreaManager", "IAreaManagement", "IAreaDiagnostics" };
        var result = MessageBox.Show("Wybierz interfejs:", "Wyb�r Interfejsu", MessageBoxButtons.YesNo);

        if (result == DialogResult.Yes)
        {
            replacementsDictionary.Add("Scenario", scenarioChoice[0]);  // Domy�lny wyb�r
        }
        else
        {
            // Zaimplementuj logik� wyboru u�ytkownika
        }
    }

    public void RunFinished()
    {
        // Obs�uguje akcje po zako�czeniu generowania pliku
    }

    public bool ShouldAddProjectItem(string filePath)
    {
        return true;
    }
}