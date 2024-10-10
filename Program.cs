using LibGit2Sharp;
using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 1)
        {
            Console.WriteLine("Please provide the path to the Git repository.");
            return;
        }

        string repoPath = args[0];
        if (!Repository.IsValid(repoPath))
        {
            Console.WriteLine("The provided path is not a valid Git repository.");
            return;
        }

        string repoName = new DirectoryInfo(repoPath).Name;
        string destinationPath = Path.Combine(@"C:\mygitchanges", repoName);

        if (!Directory.Exists(destinationPath))
        {
            Directory.CreateDirectory(destinationPath);
        }

        using (var repo = new Repository(repoPath))
        {
            var status = repo.RetrieveStatus();
            
            foreach (var entry in status)
            {
                if (entry.State == FileStatus.ModifiedInWorkdir || 
                    entry.State == FileStatus.ModifiedInIndex ||
                    entry.State == FileStatus.NewInWorkdir ||
                    entry.State == FileStatus.NewInIndex ||
                    entry.State == FileStatus.RenamedInIndex)
                {
                    string sourceFile = Path.Combine(repo.Info.WorkingDirectory, entry.FilePath);
                    string destinationFile = Path.Combine(destinationPath, entry.FilePath);

                    Console.WriteLine($"Copying: {sourceFile}");

                    // Create directory structure
                    string directory = Path.GetDirectoryName(destinationFile);
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }

                    // Copy the file
                    File.Copy(sourceFile, destinationFile, true);
                }
            }
        }

        Console.WriteLine("All changes have been copied successfully.");
    }
}
