namespace PowerShellLauncherPortal
{
    public partial class Form1 : Form
    {
        private readonly AuthenticationHandler _authenticationHandler;
        private readonly NinjaRmmApiHandler _ninjaRmmApiHandler;
        private readonly PowerShellScriptHandler _powerShellScriptHandler;

        public Form1()
        {
            _authenticationHandler = new AuthenticationHandler();
            _ninjaRmmApiHandler = new NinjaRmmApiHandler();
            _powerShellScriptHandler = new PowerShellScriptHandler();
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            var authenticated = await _authenticationHandler.AuthenticateAsync();

            if (authenticated)
            {
                List<string> computerList = await _ninjaRmmApiHandler.GetComputerListAsync();
                // Show computer list and allow selection for PowerShell script execution

                // Example of launching a PowerShell script on a selected machine
                string selectedComputer = computerList[0]; // Assuming the first computer is selected
                string scriptPath = @"C:\path\to\your\powershell\script.ps1";
                await _powerShellScriptHandler.ExecuteScriptAsync(selectedComputer, scriptPath);
            }
            else
            {
                MessageBox.Show("Authentication failed. Please check your credentials.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }
    }
}