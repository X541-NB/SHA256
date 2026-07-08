using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using System.Xml.Linq;

namespace SHA256Project
{
    class Program
    {
        static void Main(string[] argv)
        {
            try
            {
                // Class CLITools
                CLITool CommandLineTools = new CLITool();

                if (!CommandLineTools.CheckArgvCount(argv.Length))
                {
                    return;     // End the program
                }
                else
                {
                    if (!CommandLineTools.FindOptions(argv))
                    {
                        Console.WriteLine("Got error\nError: {0}", CommandLineTools.ErrorMessage);
                        CommandLineTools.ShowFormat();
                    }
                }

                CommandLineTools.GenerateSHA256(CommandLineTools.SelectedOption);


                // End the program
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An Error occured\nError: {0}\n\n\nPlease take a look of Formation", ex.ToString());
                
            }

        }

        
    }
}
