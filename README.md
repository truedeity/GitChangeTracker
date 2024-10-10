
# Git Change Tracker

## Overview

Git Change Tracker is a C# console application that accepts a path to a Git repository and detects files with changes (both staged and unstaged). It then copies these files, while preserving the folder structure, to a local directory (`C:\mygitchanges\reponame`).

## Features

- Detects files with changes (staged and unstaged).
- Preserves the folder structure while copying the files.
- Copies the modified, newly added, or renamed files from the repository to a destination folder.

## Prerequisites

- .NET SDK (6.0 or higher)
- A valid Git repository
- Git installed on your system (if using Git commands directly)

## Installation

### Step 1: Clone the Repository

```bash
git clone https://github.com/yourusername/GitChangeTracker.git
```

### Step 2: Navigate to the Project Directory

```bash
cd GitChangeTracker
```

### Step 3: Install Dependencies

This project uses the `LibGit2Sharp` library. To install the dependencies, run:

```bash
dotnet add package LibGit2Sharp
```

## Usage

### Step 1: Build the Application

To build the application, run:

```bash
dotnet build
```

### Step 2: Run the Application

To run the application, use the following command, replacing the argument with the path to your Git repository:

```bash
dotnet run -- "C:\path\to\your\git\repository"
```

The application will copy all files with changes (staged and unstaged) to `C:\mygitchanges\reponame`, preserving the folder structure of the repository.

## Example

```bash
dotnet run -- "C:\Projects\MyGitRepo"
```

After running this, all files with changes in the `MyGitRepo` repository will be copied to:

```
C:\mygitchanges\MyGitRepo
```

## Project Structure

```plaintext
|-- GitChangeTracker
    |-- Program.cs    // Main C# file containing the logic for detecting Git changes and copying files
    |-- GitChangeTracker.csproj   // Project configuration file
```

## Dependencies

- [LibGit2Sharp](https://github.com/libgit2/libgit2sharp): Used for interacting with Git repositories in C#.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Contributing

Feel free to fork this project, submit issues, or make pull requests. All contributions are welcome!
