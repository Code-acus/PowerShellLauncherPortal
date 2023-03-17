using System.Management.Automation;
using System.Management.Automation.Runspaces;

namespace PowerShellLauncherPortal
{
    public class PowerShellScriptHandler
    {
        public async Task ExecuteScriptAsync(string targetComputer, string scriptPath)
        {
            using (var runspace = RunspaceFactory.CreateRunspace())
            {
                runspace.Open();
                using (PowerShell powerShell = PowerShell.Create())
                {
                    powerShell.Runspace = runspace;

                    powerShell.AddCommand("Invoke-Command")
                        .AddParameter("ComputerName", targetComputer)
                        .AddParameter("FilePath", scriptPath);

                    var results = await Task.Factory.FromAsync<PSDataCollection<PSObject>>(
                        powerShell.BeginInvoke(), powerShell.EndInvoke);
                    
                    // Handle the results if necessary
                    
                    runspace.Close();
                    
                    // Handle any errors
                    if (powerShell.Streams.Error.Count > 0)
                    {
                        // Handle errors
                    }
                    
                    // Handle any warnings
                    if (powerShell.Streams.Warning.Count > 0)
                    {
                        // Handle warnings
                    }
                    
                    // Handle any verbose messages
                    if (powerShell.Streams.Verbose.Count > 0)
                    {
                        // Handle verbose messages
                    }
                    
                    // Handle any debug messages
                    if (powerShell.Streams.Debug.Count > 0)
                    {
                        // Handle debug messages
                    }
                    
                    // Handle any information messages
                    if (powerShell.Streams.Information.Count > 0)
                    {
                        // Handle information messages
                    }
                    
                    // Handle any progress records
                    if (powerShell.Streams.Progress.Count > 0)
                    {
                        // Handle progress records
                    }
                    
                    // Handle any information messages
                    if (powerShell.Streams.Information.Count > 0)
                    {
                        // Handle information messages
                    }
                    
                }
            }
        }
    }
}

// Path: PowerShellLauncherPortal\PowerShellScriptHandler.cs
// Compare this snippet from PowerShellLauncherPortal\Program.cs:
// namespace PowerShellLauncherPortal;
//   class Program
//   {
//       [STAThread]
//       static void Main()
//       {
//           ApplicationConfiguration.Initialize();
//           Application.Run(new Form1());
//       }
//   }
// Compare this snippet from PowerShellLauncherPortal\Form1.cs:
// namespace PowerShellLauncherPortal
// {
//     public partial class Form1 : Form
//     {
//         private readonly AuthenticationHandler _authenticationHandler;
//         private readonly NinjaRmmApiHandler _ninjaRmmApiHandler;
//         private readonly PowerShellScriptHandler _powerShellScriptHandler;
//
//         public Form1()
//         {
//             _authenticationHandler = new AuthenticationHandler();
//             _ninjaRmmApiHandler = new NinjaRmmApiHandler();
//             _powerShellScriptHandler = new PowerShellScriptHandler();
//             InitializeComponent();
//         }
//
//         private async void Form1_Load(object sender, EventArgs e)
//         {
//             var authenticated = await _authenticationHandler.AuthenticateAsync();
//
//             if (authenticated)
//             {
//                 List<string> computerList = await _ninjaRmmApiHandler.GetComputerListAsync();
//                 // Show computer list and allow selection for PowerShell script execution
//
//                 // Example of launching a PowerShell script on a selected machine
//                 string selectedComputer = computerList[0]; // Assuming the first computer is selected
//                 string scriptPath = @"C:\path\to\your\powershell\script.ps1";
//                 await _powerShellScriptHandler.ExecuteScriptAsync(selectedComputer, scriptPath);
//             }
//             else
//             {
//                 MessageBox.Show("Authentication failed. Please check your credentials.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                 Close();
//             }
//         }
//     }
// }
