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
        UserControl[] simpleUser = new UserControl[] { new EmptyField2(), new ExitAccountBtn(), new ProfileBtn(),
            new OrderBtn(),
        new EmptyField()};

        UserControl[] directorUser = new UserControl[] {new EmptyField2(), new ExitAccountBtn(), new ProfileBtn(),
            new WorkerBtn(), new MaterialsBtn(), new ComponentsBtn(), new PlanBtn(), new IconsBtn(),new OrderBtn(),
        new EmptyField()};

        UserControl[] managerUser = new UserControl[] { new EmptyField2(), new ExitAccountBtn(), new ProfileBtn(),
            new MaterialsBtn(), new ComponentsBtn(), new OrderBtn(),
        new EmptyField()};

        UserControl[] constructorUser = new UserControl[] { new EmptyField2(), new ExitAccountBtn(), new ProfileBtn(),
            new MaterialsBtn(), new ComponentsBtn(), new OrderBtn(),
        new EmptyField()};

        UserControl[] masterUser = new UserControl[] { new EmptyField2(), new ExitAccountBtn(), new ProfileBtn(),
            new MaterialsBtn(), new ComponentsBtn(), new OrderBtn(),
        new EmptyField()};

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
                        foreach (var button in managerUser)
                        {
                            mainWrapPanel.Children.Add(button);
                        }
                        break;
                    case 4:
                        foreach (var button in constructorUser)
                        {
                            mainWrapPanel.Children.Add(button);
                        }
                        break;
                    case 5:
                        foreach (var button in masterUser)
                        {
                            mainWrapPanel.Children.Add(button);
                        }
                        break;
                }
            }
        }

    }
}
