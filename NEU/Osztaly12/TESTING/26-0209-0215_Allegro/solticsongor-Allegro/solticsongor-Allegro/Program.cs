using System.Diagnostics;
using System.Text;

namespace solticsongor_Allegro;

public static class Program
{
    private const string ReportFile = "Tesztjegyzokonyv.md";

    public static void Main()
    {
        Console.WriteLine("========================================");
        Console.WriteLine("   Allegro Selenium Test Runner");
        Console.WriteLine("========================================");
        Console.WriteLine("Select test mode:");
        Console.WriteLine("1. Legal (C# - Manual CAPTCHA Solving | 3 Clash Royale Matches - apx. 30 Insta Reel)");
        Console.WriteLine("2. Illegal (Python - Stealth Bypass) | 1 Clash Royale Match - apx. 5 Insta Reel");
        Console.Write("\nYour choice (1/2): ");

        var choice = Console.ReadLine();
        string output;

        if (choice == "2")
        {
            if (CheckPythonEnvironment())
            {
                output = RunPythonStealth();
            }
            else
            {
                Console.WriteLine("\nPython environment not found. Switching to Legal mode...");
                output = RunCSharpTests("0");
            }
        }
        else
        {
            output = RunCSharpTests("0");
        }

        UpdateTestReport(choice == "2" ? "Illegal (Python)" : "Legal (C#)", output);

        if (!Console.IsInputRedirected)
        {
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }

    private static bool CheckPythonEnvironment()
    {
        try
        {
            using var process = Process.Start(new ProcessStartInfo
            {
                FileName = "python",
                Arguments = "--version",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            });
            process?.WaitForExit();
            return process?.ExitCode == 0;
        }
        catch
        {
            return false;
        }
    }

    private static bool IsPackageInstalled(string packageName)
    {
        try
        {
            using var process = Process.Start(new ProcessStartInfo
            {
                FileName = "python",
                Arguments = $"-c \"import {packageName.Replace("-", "_")}\"",
                UseShellExecute = false,
                CreateNoWindow = true
            });
            process?.WaitForExit();
            return process?.ExitCode == 0;
        }
        catch
        {
            return false;
        }
    }

    private static string RunPythonStealth()
    {
        var installedNow = false;
        var keepPackage = true;

        const string scriptName = "stealth_bypass.py";
        var scriptPath = File.Exists(scriptName) ? scriptName : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, scriptName);

        if (!File.Exists(scriptPath))
        {
            Console.WriteLine($"\nError: {scriptName} not found.");
            return "Error: Script not found.";
        }

        if (!IsPackageInstalled("undetected-chromedriver") || !IsPackageInstalled("requests") || !IsPackageInstalled("selenium-stealth"))
        {
            Console.Write("\nRequired Python packages are missing. Install them? (y/n): ");
            if (Console.ReadLine()?.ToLower() != "y") return "Installation declined.";

            Console.Write("Keep packages after run? (y/n): ");
            keepPackage = Console.ReadLine()?.ToLower() == "y";

            if (!IsPackageInstalled("undetected-chromedriver")) RunCommand("pip", "install undetected-chromedriver");
            if (!IsPackageInstalled("requests")) RunCommand("pip", "install requests");
            if (!IsPackageInstalled("selenium-stealth")) RunCommand("pip", "install selenium-stealth");
            installedNow = true;
        }

        Console.WriteLine($"\nStarting: python {scriptName}...");
        string output = RunCommandWithCapture("python", $"\"{scriptPath}\"");

        if (installedNow && !keepPackage)
        {
            RunCommand("pip", "uninstall -y undetected-chromedriver requests selenium-stealth");
        }
        return output;
    }

    private static void RunCommand(string fileName, string arguments)
    {
        try
        {
            using var process = Process.Start(new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = arguments,
                UseShellExecute = false,
                CreateNoWindow = false
            });
            process?.WaitForExit();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private static string RunCommandWithCapture(string fileName, string arguments)
    {
        var sb = new StringBuilder();
        try
        {
            using var process = Process.Start(new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = arguments,
                UseShellExecute = false,
                CreateNoWindow = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            });

            process!.OutputDataReceived += (s, e) => { if (e.Data != null) { Console.WriteLine(e.Data); sb.AppendLine(e.Data); } };
            process!.ErrorDataReceived += (s, e) => { if (e.Data != null) { Console.WriteLine(e.Data); sb.AppendLine(e.Data); } };

            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();
        }
        catch (Exception ex)
        {
            sb.AppendLine($"Execution Error: {ex.Message}");
        }
        return sb.ToString();
    }

    private static string RunCSharpTests(string mode)
    {
        Console.WriteLine("\nStarting tests in Legal (C#) mode...");
        return RunCommandWithCapture("dotnet", $"test --logger \"console;verbosity=normal\" -- environment ALLEGRO_TEST_MODE={mode}");
    }

    private static void UpdateTestReport(string mode, string log)
    {
        var reportPath = Path.Combine(Directory.GetCurrentDirectory(), ReportFile);
        var reportContent = new StringBuilder();

        if (!File.Exists(reportPath))
        {
            reportContent.AppendLine("# Allegro Automated Test History");
            reportContent.AppendLine("| Timestamp | Mode | Status |");
            reportContent.AppendLine("| :--- | :--- | :--- |");
        }
        else
        {
            reportContent.Append(File.ReadAllText(reportPath).TrimEnd());
            reportContent.AppendLine();
        }

        string status = (log.ToLower().Contains("failed") || log.ToLower().Contains("error") || log.ToLower().Contains("exception")) ? "❌ FAILED" : "✅ PASSED";
        reportContent.AppendLine($"| {DateTime.Now:yyyy-MM-dd HH:mm:ss} | {mode} | {status} |");

        File.WriteAllText(reportPath, reportContent.ToString());
        Console.WriteLine($"\nTest record added to: {ReportFile}");
    }
}
