using System;
using System.IO;
using System.Collections.Generic;

class AoC_puzzle7 {
    
    // OK, a harder challenge this time. How to collect the data?
    // I believe a tree-structure is in order. But we have to create
    // our own classes for this.

    // First we make an interface for the nodes of our tree - there are two types of nodes,
    // files and directories/folders. They both have a name, and both need a way to measure
    // data size.

    /// <summary>
    /// Interface describing the nodes of our file-system, which can be either a 
    /// file or a folder.
    /// </summary>
    public interface FileSystemNode {
        public string Name {get;}
        public int GetDataSize();
    }

    // Now we implement the data file class:

    /// <summary>
    /// A class for the files in our tree-structured file-and-folder-system.
    /// </summary>
    public class File : FileSystemNode {
        private string name;
        private int dataSize;
        public string Name {
            get {return name;}
        }
        public File (string name, int size) {
            this.name = name;
            this.dataSize = size;
        } 
        public int GetDataSize() {
            return dataSize;
        }
    }
    // Next we implement the more tricky Folder-class.
    // Beyond the interface we need a way to add FileSystemNodes to the
    // folder. ALSO we need a way to navigate back and forth from 
    // one folder either further down, or back. So we need a parent-folder
    // property, but also we need a way to choose which sub-folder we want to go to.

    /// <summary>
    /// A class for the folders in our tree-strucutured file-and-folder-system.
    /// </summary>
    public class Folder : FileSystemNode {
        private string name;
        private List<FileSystemNode> contents;
        public Folder? Parent {get; set;}
        public string Name {
            get {return name;}
        }
        public Folder(string name) {
            this.name = name;
            this.contents = new List<FileSystemNode>();
        }
        // A second constructor, to create a folder with a given parent.
        public Folder(string name, Folder parent) {
            this.name = name;
            this.contents = new List<FileSystemNode>();
            this.Parent = parent;
        }
        public void AddToFolder(FileSystemNode fileNode) {
            contents.Add(fileNode);
        }
        public FileSystemNode GetFileSystemNode(string name) {
            return contents.Find(x => x.Name == name);
        }
        public List<FileSystemNode> GetContents() {
            return contents;
        }
        public int GetDataSize() {
            int dataSize = 0;
            foreach (FileSystemNode node in contents) {
                dataSize += node.GetDataSize();
            }
            return dataSize;
        }
    }

    /// <summary>
    /// Recursive method for finding the sum of data of any folders with less than a
    /// specified threshold of datasize, in a FileSystemNode. Note that
    /// subfolders are counted both in their own right, but also in any parent folders.
    /// </summary>
    private static int SumOfFolderDataBelowThreshold(FileSystemNode node, int threshold) {
        int output = 0;
        if (node is Folder folder) {
            int dataSize = folder.GetDataSize();
            // Console.WriteLine($"Folder {folder.Name} of datasize {folder.GetDataSize()} detected");
            if (dataSize <= threshold) {
                output += dataSize;
            }
            List<FileSystemNode> contents = folder.GetContents();
            foreach (FileSystemNode subNode in contents) {
                output += SumOfFolderDataBelowThreshold(subNode,threshold);
            }
        }
        return output;
    }




    /// <summary>
    /// Method that reads the input .txt file into a 
    /// tree-like folder structure.
    /// </summary>
    /// <param name="str"> Name of input file</param>
    /// <returns>A folder</returns>
    public static Folder ReadFileOutputFolder(string str) {
        
        Folder fileSystem = new Folder("/");
        Folder currentFolder = fileSystem;
        try {
            // Create an instance of StreamReader to read from a file.
            // The using statement also closes the StreamReader.
            using (StreamReader sr = new StreamReader(str))
            {
                string line;
                // Read and display lines from the file until the end of
                // the file is reached.
                while ((line = sr.ReadLine()) != null) {
                    if (line[0] == '$') {
                        switch (line.Substring(2)) {
                            case "cd /":
                                currentFolder = fileSystem;
                                break;
                            case "cd ..":
                                if (currentFolder.Parent != null) {
                                    currentFolder = currentFolder.Parent;
                                }
                                break;
                            case "ls":
                                // Do nothing, is taken care of in the line[0] != '$' parts.
                                break;
                            default:
                                currentFolder = (Folder)currentFolder.GetFileSystemNode(line.Substring(5));
                                break;
                        }
                    }
                    else if (line.Substring(0,3) == "dir") {
                        currentFolder.AddToFolder(new Folder(line.Substring(4),currentFolder));
                        // Console.WriteLine($"Added folder {line.Substring(4)} to folder {currentFolder.Name}!");
                    } 
                    else {
                        string[] splitFile = line.Split(' ');
                        currentFolder.AddToFolder(new File(splitFile[1],Int32.Parse(splitFile[0])));
                        // Console.WriteLine($"Added file {currentFolder.GetFileSystemNode(splitFile[1]).Name} with data size {splitFile[0]} to folder {currentFolder.Name}!");
                        // Console.WriteLine($"Current datasize of folder {currentFolder.Name}: {currentFolder.GetDataSize()}.");
                    }
                }
            }
            
        }
        catch (Exception e)
        {
            // Let the user know what went wrong.
            Console.WriteLine("The file could not be read into folder-structure:");
            Console.WriteLine(e.Message);
        }
        return fileSystem;;
    }


    // OK, now for part 2! We need to find the total size of a folder, that we already have. 
    // But then I guess we need to make a list, and then add to this list every folder that
    // has a datasize big enough to fix our grand problem. Then in the end we can sort this list
    // and then find the best candidate for deletion.

    // OK, better yet is just to traverse and count at the same time, I guess!

    /// <summary>
    /// Recursive method that traverses the input folder. If input is not sufficiently large, 
    /// A generic folder is returned. Otherwise the folder with smallest datasize, but still
    /// larger than the threshold, is returned, as a tuple of its name and its datasize.
    /// </summary>
    /// <param name="folder"> the input folder of type Folder that is traversed</param>
    /// <param name="threshold">the int threshold that input folder datasize must equal or
    /// exceed to be eligible for deletion</param>
    /// <returns>return the tuple of (string name, int datasize) of folder for deletion</returns>
    public static (string, int) FolderForDeletion(Folder folder, int threshold) {
        Folder outputFolder = new Folder("GenericFolder");
        (string,int) output = (outputFolder.Name,Int32.MaxValue);
        if (folder.GetDataSize() >= threshold && folder.GetDataSize() < output.Item2) {
            output = (folder.Name,folder.GetDataSize());
        }
        foreach (FileSystemNode node in folder.GetContents()) {
            if (node is Folder folderNode) {
                if (FolderForDeletion(folderNode,threshold).Item2 < output.Item2) {
                    output = FolderForDeletion(folderNode,threshold);
                }
                
            }
        }
        return output;
    }

    public static void Main() {
        
        // Folder fullFolder = ReadFileOutputFolder("AoC_puzzle7.txt");
        Folder testFolder = ReadFileOutputFolder("testString.txt");
        Console.WriteLine("The sum of data for nodes with data below 100,000 for folder testFolder {0} is: {1}",
            testFolder.Name,SumOfFolderDataBelowThreshold(testFolder,100000));
        // It works! Now we go back to the original folder:
        Folder fullFolder = ReadFileOutputFolder("AoC_puzzle7.txt");
        Console.WriteLine("The sum of data for nodes with data below 100,000 for fullFolder {0} is: {1}",
            fullFolder.Name,SumOfFolderDataBelowThreshold(fullFolder,100000));

        // Part two: 
        // First we need to determine the amount of space that is currently occupied:
        Console.WriteLine("The datasize of fullFolder {0} is: {1}",fullFolder.Name,fullFolder.GetDataSize());
        Console.WriteLine("That means to leave unused data of 30000000 in fullFolder, we need to clear at least: {0} amount of data",
            fullFolder.GetDataSize()-(70000000-30000000));
        // We need to define the minimal amount of data a folder needs to have to be deleted:
        int testThreshold = testFolder.GetDataSize()-(70000000-30000000);
        int fullThreshold = fullFolder.GetDataSize()-(70000000-30000000);
        Console.WriteLine("The folder for deletion in testFolder {0} is: {1}",
            testFolder.Name,FolderForDeletion(testFolder,testThreshold));
        Console.WriteLine("The folder for deletion in fullFolder {0} is: {1}",
            fullFolder.Name,FolderForDeletion(fullFolder,fullThreshold));
    }
}