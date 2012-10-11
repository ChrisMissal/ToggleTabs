using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.ComponentModel.Design;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft.Win32;

namespace Headspring.ToggleTabs
{
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(GuidList.guidToggleTabsPkgString)]
    public sealed class TabsSpacesTogglerPackage : Package
    {
        private DTE2 _dte;

        public TabsSpacesTogglerPackage()
        {
            Debug.WriteLine("Entering constructor for: {0}", this);
        }

        protected override void Initialize()
        {
            Debug.WriteLine ("Entering Initialize() of: {0}", this);

            base.Initialize();

            _dte = GetGlobalService(typeof (DTE)) as DTE2;

            var mcs = GetService(typeof (IMenuCommandService)) as OleMenuCommandService;

            if (mcs != null)
            {
                var menuCommandId = new CommandID(GuidList.guidToggleTabsCmdSet, (int) PkgCmdIDList.cmdidToggleTabs);
                var menuItem = new MenuCommand(MenuItemCallback, menuCommandId);
                mcs.AddCommand(menuItem);
            }
        }

        private void MenuItemCallback(object sender, EventArgs e)
        {
            const string keyName = @"Software\Headspring\ToggleTabs";
            const string subkeyName = "CurrentState";

            var key = Registry.CurrentUser.OpenSubKey(keyName, true) ?? Registry.CurrentUser.CreateSubKey(keyName);

            if (key == null)
                return;

            var currentState = (int) key.GetValue(subkeyName, 0);
            currentState = currentState == 0 ? 1 : 0;

            _dte.Properties["TextEditor", "AllLanguages"].Item("InsertTabs").Value = currentState;
            key.SetValue(subkeyName, currentState, RegistryValueKind.DWord);
        }
    }
}
