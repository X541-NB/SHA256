using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;

namespace SHA256Project
{
    class CLITool
    {
        public string ErrorMessage = "";
        public string SelectedOption = "";
        static string pathFile = "";
        static string pathDirectory = "";
        static string pathDirectoryToSave = "";

        public bool CheckArgvCount(int argvLength)
        {
            if (argvLength != 0 && argvLength <= 4)
            {
                return true;
            }
            else
            {
                ShowFormat();
                return false;
            }
        }

        public bool FindOptions(string[] argv)
        {
            if (!CheckForSaveHashCodeFile(argv))
            {
                return false;

            }
                if (argv[0] == "-d")
                {
                    if (Directory.Exists(argv[1]))
                    {
                        pathDirectory = argv[1];
                        SelectedOption = "-d";
                        return true;
                    }
                    else
                    {
                        ErrorMessage = "Directory Is not Exists\n";
                        return false;
                    }
                }
                else if (argv[0] == "-f")
                {
                    if (File.Exists(argv[1]))
                    {
                        pathFile = argv[1];
                        SelectedOption = "-f";
                        return true;
                    }
                    else
                    {
                        ErrorMessage = "File Is not Exists\n";
                        return false;
                    }

                }
                else if (argv[0] == "-c")
                {
                    // Check the cound of argument at all, for this option, the count of arguments
                    // must be 3(it contain the option and two paths or hash codes), if it's not, print error the format
                    if (argv.Length != 3)
                    {
                        ErrorMessage = "You just used wrong format\n";
                        return false;
                    }

                    // if it returns an index of \ or / , it means user entered a path
                    if (argv[1].IndexOf('\\') >= 0 || argv[1].IndexOf('/') >= 0 || File.Exists(argv[1]))
                    {
                        if (!File.Exists(argv[1]))
                        {
                            ErrorMessage = "File Is not Exists in firstPath\n";
                            return false;
                        }
                        if (argv[2].IndexOf('\\') >= 0 || argv[2].IndexOf('/') >= 0)
                        {
                            if (!File.Exists(argv[2]))
                            {
                                ErrorMessage = "File Is not Exists in SecondPath\n";
                                return false;
                            }
                        }

                        // Check the file name in both paths
                        if (Path.GetFileName(argv[1]) != "GeneratedHash.xml")
                        {
                            ErrorMessage = "File name Is not GeneratedHash.xml in FirstPath\n";
                            return false;
                        }

                        if (Path.GetFileName(argv[2]) != "GeneratedHash.xml")
                        {
                            ErrorMessage = "File name Is not GeneratedHash.xml in SecondPath\n";
                            return false;
                        }


                        if (!CompareTwoGeneratedHashFiles(argv[1], argv[2]))
                            return false;


                       
                        return true;

                    }
                    else
                    {
                        // If user entered a hash code in first argument but enterd a path in second argument, got this error.
                        if (argv[2].IndexOf('/') >= 0 || argv[2].IndexOf('\\') >= 0)
                        {
                            ErrorMessage = "You used a Hash Code in first argument but in second argument you used a path!\nWhy? Please use a hash code in secound argument\n";
                            return false;

                        }



                        Console.WriteLine(CompareHashCodes(argv[1].ToLower(),argv[2].ToLower()));

                        return true;
                    }

                }
                else
                {
                    ErrorMessage = "You didn't use the correct Options\n";
                    return false;
                }
            

        }

        public bool CheckForSaveHashCodeFile(string[] argv)
        {
            // Check Path for saving file
            if (argv.Length == 4)     // At all we have 4 arguments + the -s for saving
            {
                if (argv[2] == "-s")    // arguments start from 0, Do you remember..?!
                {
                    if (Directory.Exists(argv[3]))
                    {
                        pathDirectoryToSave = argv[3];
                        return true;
                    }
                    else
                    {
                        ErrorMessage = "Directory For Saving File Is not Exists\n";
                        return false;
                    }
                }
                else
                {
                    ErrorMessage = "Didn't use the correct option\n";

                    return false;
                }

            }

            return true;

        }

        public void ShowFormat()
        {
            Console.WriteLine("usage: programName [options] [pathToGetFile\\pathToGetDirectory]\n");
            Console.WriteLine("Getting help:");
            Console.WriteLine("\t-d\tDirectory\t---Choose the full Directory(All files will be found by the program automatically)");
            Console.WriteLine("\t-f\tFile\t\t---Choose one file");
            Console.WriteLine("\t-c\tCompare\t\t---Choose two (\"paths\" or \"Hash Codes\") to compare");
            Console.WriteLine("\t-s\tSave\t\t---Choose a path to save the generated Hash Code(s)\n\t    \t\t\t   It's Optional(If not entered, it prints them on screen)");


            Console.WriteLine("Usage\n");
            Console.WriteLine("\tprogramName -d \"PathToDirectory\\\" -s \"PathToDirectory\\\"");
            Console.WriteLine("\tprogramName -f \"PathToDirectory\\file.abc\" -s \"PathToDirectory\\\"");
            Console.WriteLine("\tprogramName -c \"5f82577456dd4637fc4f79350cc8a1db4b8505a9d5b22c73a75aef18e34efeb7\" \"5f82577456dd4637fc4f79350cc8a1db4b8505a9d5b22c73a75aef18e34efeb7\"");
            Console.WriteLine("\tprogramName -c \"PathToDirectory\\GeneratedHash.xml\" \"PathToDirectory\\GeneratedHash.xml\"");
            


            Console.WriteLine("\n\n\n\nTake a look, Something to see...\n\thttps:\\\\github.com\\X541-NB \t\t\"Just For NB!\"");
        }

        public void GenerateSHA256(string option)
        {
            if (option == "-f")     // Check for files
            {
                if (pathDirectoryToSave == "")
                {
                    Console.WriteLine("The generated hash:\n");
                    Console.WriteLine("\t{0}   -   {1}", SHA256Algorithm.ComputeSha256Hash(pathFile), Path.GetFileName(pathFile));
                    return;
                }
                else
                {
                    SaveHashCodeFile(SHA256Algorithm.ComputeSha256Hash(pathFile));
                    Console.WriteLine("\n\nHash File saved successfully in {0}", pathDirectoryToSave + "\\GeneratedHash.xml");
                    return;
                }
            }
            else if (option == "-d")        // Check for directory
            {
                try
                {
                    if (pathDirectoryToSave == "")      // Check that user used -s for saving
                    {                                   // if it was null it means used didn't use -s
                        // Get all files in all directories
                        string[] files = Directory.GetFiles(pathDirectory, "*", SearchOption.AllDirectories);
                        foreach (string file in files)
                        {
                            //  Check the directoris for existence
                            if (File.Exists(file))
                            {
                                Console.WriteLine("\t{0}   -   {1}", SHA256Algorithm.ComputeSha256Hash(file), file);
                            }
                        }
                    }
                    else
                    {
                        XElement FilesElements = new XElement("Files");
                         string[] files = Directory.GetFiles(pathDirectory, "*", SearchOption.AllDirectories);
                        // Let's say you want to add 10 items
                        foreach (string file in files)
	                    {
                            
                            if(File.Exists(file))
                            {
                                string generatedFileHash = SHA256Algorithm.ComputeSha256Hash(file);
		                     FilesElements.Add(
                                    new XElement("File",
                                        new XElement("FileName", Path.GetFileName(file)),
                                        new XElement("HashCode", generatedFileHash),
                                        new XElement("GeneratedFrom", file)));
                            }
	                    }
                           


                        XDocument doc = new XDocument(FilesElements);
                        string savingPath = pathDirectoryToSave + "\\GeneratedHash.xml";
                        doc.Save(savingPath);
                        Console.WriteLine("\n\nHash File saved successfully in {0}", pathDirectoryToSave + "\\GeneratedHash.xml");

                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine("Somethig happend during generating HASH CODE\nError: {0}",ex.ToString());
                    
                }
                



            }


        }

        public void SaveHashCodeFile(string generatedHash)
        {
            if (SelectedOption == "-f" && pathDirectoryToSave != "")     // File path checking
            {                                                            // if pathDirectoryToSave was exists, we understand user used -s for saving
                XDocument doc = new XDocument(
                    new XElement("Files",
                        new XElement("File",
                            new XElement("FileName", Path.GetFileName(pathFile)),
                            new XElement("HashCode", generatedHash),
                            new XElement("GeneratedFrom", pathFile))));

                string savingPath = pathDirectoryToSave + "\\GeneratedHash.xml";
                doc.Save(savingPath);


            }

        }

        public string CompareHashCodes(string firstArgument, string secondArgument)
        {
            if (firstArgument == secondArgument)
                return "Both are correct!";
            else
                return "They're not correct...!";
        }

        public bool CompareTwoGeneratedHashFiles(string path1, string path2)
        {
            try
            {
                // checks the count of elemet file
                // If they were the same it continues
                // Load both
                XDocument doc1 = XDocument.Load(path1);
                XDocument doc2 = XDocument.Load(path2);
                if (doc1.Descendants("File").Count() != doc2.Descendants("File").Count())
                {
                    ErrorMessage = "The count of elements File are not the same in both files\n       One of them has " + doc1.Descendants("File").Count() + " elements and other one has " + doc2.Descendants("File").Count() + "! ";
                    return false;
                }

                // Get all File elements from both documents
                var files1 = doc1.Descendants("File").ToList();
                var files2 = doc2.Descendants("File").ToList();

                // If some files were not the same
                // They will be added to this array. At the end, it prints all the errors.
                string[] FilesThatAreNotSame = new string[doc1.Descendants("File").Count()];

                int indexFilesNotSame = -1;
                bool firstInitTheIndexNotSame = false;

                for (int i = 0; i < doc1.Descendants("File").Count(); i++)
                {
                    string fileName1 = (string)files1[i].Element("FileName");
                    string HashCode1 = (string)files1[i].Element("HashCode");

                    string fileName2 = (string)files2[i].Element("FileName");
                    string HashCode2 = (string)files2[i].Element("HashCode");

                    // Compare both FileNames AND Hashcodes
                    if (fileName1 == fileName2 && HashCode1 == HashCode2)
                    {
                        Console.WriteLine("Element {0} is: OK", i + 1);
                    }
                    else
                    {
                        Console.WriteLine("Element {0} is: Incorrect!", i + 1);

                        // I wrote this because of init the indexFilesNotSame
                        // I Should know there is incorrect or not
                        // it init the indexFilesNotSame for one time
                        // if I didn't wrote this it init the indexFilesNotSame everytime
                        if (firstInitTheIndexNotSame == false)
                        {
                            indexFilesNotSame = 0;
                            firstInitTheIndexNotSame = true;
                        }

                        string path1NotSame = (string)files1[i].Element("GeneratedFrom");
                        string path2NotSame = (string)files2[i].Element("GeneratedFrom");

                        FilesThatAreNotSame[indexFilesNotSame] = "FileName 1: " + fileName1 + "\tFileName 2: " + fileName2 + "\nHashCode 1: " + HashCode1 + "\tHashCode 2: " + HashCode2 + "\nFile1 GeneratedFrom: " + path1NotSame + "\tFile2 GeneratedFrom: " + path2NotSame;
                        indexFilesNotSame++;
                    }

                }

                // it's because of FilesThatAreNotSame
                // why? because I need to check how many incorrect are there
                // this will tell me (The index of FilesThatAreNotSame)
                // if it was more that -1 (num >= 0)
                // I understand it has at least one incorrect, OK!
                if (indexFilesNotSame > -1)
                {
                    Console.WriteLine("\n\nWe got some files that were Incorrect");
                    Console.WriteLine("{0} Files:\n", indexFilesNotSame);

                    for (int i = 0; i < indexFilesNotSame; i++)
                    {
                        Console.WriteLine(FilesThatAreNotSame[i]);
                    }

                    Console.WriteLine("\n\nOne tip: Don't change the file name or hash code in GeneratedHash.xml before generating hash codes. I mean copy your files from source to where you want (your destination), then generate hash codes ,compare them, and after all these, change file name");

                }
                else
                {
                    Console.WriteLine("All elements were Correct!");
                }


                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nWe got an error: \n{0}",ex.ToString());

                ErrorMessage = "\tOne tip: Don't change the GeneratedHash.xml, if you change it, everything will be awful";

                return false;
            }
            

        }
    }
}
