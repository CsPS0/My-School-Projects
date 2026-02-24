using displayLib;
using System.Text;
using System.Diagnostics;

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;

Console.Clear();

Console.Write("Enter a number (or text containing numbers): ");
string? input = Console.ReadLine();

if (input != null)
{
    string result = DigitalDisplay.GetDisplayFromRawInput(input);
    if (!string.IsNullOrEmpty(result))
    {
        Console.WriteLine(result);
        
        Console.WriteLine("\n--- Running Tests ---");
        try 
        {
            string? slnPath = null;
            DirectoryInfo? currentDir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            
            while (currentDir != null)
            {
                var files = currentDir.GetFiles("*.sln", SearchOption.TopDirectoryOnly);
                if (files.Length > 0)
                {
                    slnPath = files[0].FullName;
                    break;
                }
                currentDir = currentDir.Parent;
            }

            if (slnPath == null)
            {
                currentDir = new DirectoryInfo(Directory.GetCurrentDirectory());
                while (currentDir != null)
                {
                    var files = currentDir.GetFiles("*.sln", SearchOption.TopDirectoryOnly);
                    if (files.Length > 0)
                    {
                        slnPath = files[0].FullName;
                        break;
                    }
                    currentDir = currentDir.Parent;
                }
            }

            if (slnPath != null)
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "dotnet",
                    Arguments = $"test \"{slnPath}\" --no-restore",
                    RedirectStandardOutput = false,
                    UseShellExecute = false,
                    CreateNoWindow = false
                };
                
                using (Process? process = Process.Start(startInfo))
                {
                    process?.WaitForExit();
                }
            }
            else
            {
                Console.WriteLine("Could not find the solution file (.sln) to run tests.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Could not run tests: {ex.Message}");
        }
    }
    else
    {
        Console.WriteLine("No numbers detected.");
    }
}
