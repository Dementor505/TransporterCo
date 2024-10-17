using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TransporterCompany.UserButtons;

namespace TransporterCompany.MainDataBase
{
    public class MainButtons
    {
        UserControl[] simpleUser = new UserControl[] { new ExitAccountBtn() };
        UserControl[] directorUser = new UserControl[] { new ExitAccountBtn() };
        UserControl[] workerUser = new UserControl[] { new ExitAccountBtn() };

        public WrapPanel mainWrapPanel;

        public void ClearWrapPanel()
        {
            mainWrapPanel.Children.Clear();
        }
        public void RefreshButtons(int role)
        {
            if (mainWrapPanel != null)
            {
                mainWrapPanel.Children.Clear();
                switch (role)
                {
                    case 1:
                        foreach (var button in simpleUser)
                        {
                            mainWrapPanel.Children.Add(button);
                        }
                        break;
                    case 2:
                        foreach (var button in directorUser)
                        {
                            mainWrapPanel.Children.Add(button);
                        }
                        break;
                    case 3:
                        foreach (var button in workerUser)
                        {
                            mainWrapPanel.Children.Add(button);
                        }
                        break;
                }
            }
        }

    }
}
