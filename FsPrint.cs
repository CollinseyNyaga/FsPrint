using System;
using System.Collections.Generic;
using System.IO;

class FsPrint
{
    static string CurrentDirectory = "/";           // default directory of start is root directory in the root partition
    
    public static void Main()
    {
        Console.WriteLine("EnterPath (default = / ) :");
        
        string overrideDir = Console.ReadLine();
        
        if(Directory.Exists(overrideDir))
        {
            CurrentDirectory = overrideDir;
            SearchFileSystem(CurrentDirectory);
        }
        else
        {
            Console.WriteLine("Directory path provided doesnt exist. Sorry");
        }
        
    }
    
    
    static void SearchFileSystem(string currentDirectory)
    {
        // first , begin by getting the files / folders in the current root directory .
        // root directory only means the directory that we ought to start searching from .
        
        Fof f = new Fof(path: currentDirectory , gen: 1 , type: 1);
    }

}


// Fof stands for file or folder 
// type is if the entity is a file or folder 
// file = 0
// folder = 1

public class Fof
{
    static string Path;
    int Type;
    string Name;
    public int Generation;                              // generation is the position of the current file or folder , from the topmost parent in the hierachy
    
    // constructor : 
    public Fof(string path, int gen , int type = 1 )
    {
        Path = path;
        Type = type;
        Generation = gen;
        
        CreateName();
        PrintSelf();
        
        //  if its a directory , find children directories:
        if(type == 1)
        {
            FindChildren();
        }
    }
    
    
    void FindChildren()
    {
        // iterate to children directories.
        IEnumerable<string> folders;
        IEnumerable<string> files;
        
        try
        {
            folders = Directory.EnumerateDirectories(Path);
            files = Directory.EnumerateFiles(Path);
            
            int currentGen = Generation + 1;

            foreach(string foldername in folders)
            {
                Fof folder = new Fof(path: foldername , type: 1 , gen: currentGen);
            }
            
            foreach (string filename in files)
            {
                Fof file = new Fof(path: filename ,gen: currentGen , type: 0);
            }
            
        }
        catch(Exception e)
        {
            
        }
        
    }
    
    void CreateName()
    {
        // takes the path of the file and creates a name out of it :
        // moves from the back to forward characters in the Path
        string nameReversed = "";
        
        for(int i = Path.Length - 1 ; i >= 0; i--)
        {
            if(Path[i] == '/')
            {
                break;
            }
            
            nameReversed = nameReversed + Path[i];
        }

        // reverse the name : 
        int k = nameReversed.Length - 1;
        for(int j = 0; j < nameReversed.Length ; j++)
        {
           Name = Name + nameReversed[k];
           k = k - 1;
        }
                        
        // Console.WriteLine(Name);
    }
    
    void PrintSelf()
    {
        // prints / renders the current file or folder in the correct way , depending on the level
        
        string prefix = "";
        string suffix = "";
        int i = 0;
        
        while(i <= Generation)
        {
            prefix = prefix + "\t";
            i = i + 1;
        }
        
        // if directory , set suffix to down arrow
        if(Type == 1)
        {
           suffix = "▼"; 
        }
        
        Console.WriteLine($"{prefix} ├─» {Name} {suffix}");
    }
    
    
}
