using System;
using System.Windows.Forms;

namespace PowerShellLauncherPortal
{
    public partial class Form1 : Form
    {
        private AuthenticationHandler _authenticationHandler;
        private NinjaRmmApiHandler _ninjaRmmApiHandler;
        private PowerShellScriptHandler _powerShellScriptHandler;
        private SharePointHandler _sharePointHandler;
        private ListBox _scriptListBox;

        private Button _authenticateButton;
        private ComboBox _computerDropdown;
        private Button _launchScriptsButton;

        public Form1()
        {
            InitializeComponent();
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            _authenticationHandler = new AuthenticationHandler();
            _ninjaRmmApiHandler = new NinjaRmmApiHandler();
            _powerShellScriptHandler = new PowerShellScriptHandler();
            _sharePointHandler = new SharePointHandler();

            _scriptListBox = new ListBox
            {
                Location = new System.Drawing.Point(300, 50),
                Size = new System.Drawing.Size(200, 200)
            };
            Controls.Add(_scriptListBox);

            _authenticateButton = new Button
            {
                Text = "Authenticate",
                Location = new System.Drawing.Point(50, 50),
                Size = new System.Drawing.Size(100, 30)
            };
            _authenticateButton.Click += AuthenticateButton_Click;
            Controls.Add(_authenticateButton);

            _computerDropdown = new ComboBox
            {
                Location = new System.Drawing.Point(50, 100),
                Size = new System.Drawing.Size(200, 30)
            };
            Controls.Add(_computerDropdown);

            _launchScriptsButton = new Button
            {
                Text = "Launch Scripts",
                Location = new System.Drawing.Point(50, 150),
                Size = new System.Drawing.Size(100, 30)
            };
            _launchScriptsButton.Click += LaunchScriptsButton_Click;
            Controls.Add(_launchScriptsButton);
        }

        private async void AuthenticateButton_Click(object sender, EventArgs e)
        {
            bool isAuthenticated = await _authenticationHandler.AuthenticateAsync();
            if (isAuthenticated)
            {
                var computerList = await _ninjaRmmApiHandler.GetComputerListAsync();
                _computerDropdown.Items.AddRange(computerList.ToArray());

                // Fetch the list of PowerShell scripts from SharePoint
                var scriptList = await _sharePointHandler.GetScriptListAsync(_authenticationHandler.AccessToken);
                _scriptListBox.Items.AddRange(scriptList.ToArray());
            }
            else
            {
                MessageBox.Show("Authentication failed. Please try again.");
            }
        }

        private void LaunchScriptsButton_Click(object sender, EventArgs e)
        {
            if (_computerDropdown.SelectedItem != null)
            {
                string computerName = _computerDropdown.SelectedItem.ToString();
                _powerShellScriptHandler.ExecuteScriptOnComputer(computerName);
            }
            else
            {
                MessageBox.Show("Please select a computer from the dropdown.");
            }
        }
    }
}
