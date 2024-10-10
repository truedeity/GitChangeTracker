using System;
using System.IO;
using System.Linq;
using LibGit2Sharp;

class Program
{
    static void Main(string[] args)
    {
        string repoPath;

        // If no argument is provided, check the current directory
        if (args.Length == 0)
        {
            repoPath = Environment.CurrentDirectory;
            Console.WriteLine($"No repository path provided. Checking current directory: {repoPath}");

            // Check if the current directory is a Git repository
            if (!Repository.IsValid(repoPath))
            {
                Console.WriteLine("The current directory is not a Git repository.");
                return;
            }
        }
        else
        {
            repoPath = args[0];
        }

        // Ensure the repoPath is valid
        if (!Directory.Exists(repoPath))
        {
            Console.WriteLine("Please provide a valid Git repository path.");
            return;
        }

        // Destination directory where changes will be copied
        string destPath = @"C:\mygitchanges";

        // Create a timestamped directory for the changes
        string repoName = new DirectoryInfo(repoPath).Name;
        string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        string changesDir = Path.Combine(destPath, $"{repoName}_{timestamp}");

        try
        {
            Directory.CreateDirectory(changesDir);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating directory: {ex.Message}");
            return;
        }

        try
        {
            // Use LibGit2Sharp to detect staged and unstaged changes
            using (var repo = new Repository(repoPath))
            {
                var status = repo.RetrieveStatus();

                var changedFiles = status
                    .Where(entry => entry.State == FileStatus.ModifiedInWorkdir || entry.State == FileStatus.ModifiedInIndex || entry.State == FileStatus.NewInIndex || entry.State == FileStatus.DeletedFromWorkdir)
                    .Select(entry => entry.FilePath)
                    .ToList();

                if (!changedFiles.Any())
                {
                    Console.WriteLine("No changed files found.");
                    return;
                }

                // Copy changed files to destination while preserving folder structure
                foreach (var file in changedFiles)
                {
                    string sourceFilePath = Path.Combine(repoPath, file);
                    string destFilePath = Path.Combine(changesDir, file);

                    // Ensure the file exists before trying to copy
                    if (File.Exists(sourceFilePath))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(destFilePath)); // Ensure directory structure is preserved
                        File.Copy(sourceFilePath, destFilePath, true); // Copy file

                        Console.WriteLine($"Copied {file} to {destFilePath}");
                    }
                    else
                    {
                        Console.WriteLine($"File {file} was deleted, skipping copy.");
                    }
                }

                Console.WriteLine($"Changes copied to {changesDir}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
