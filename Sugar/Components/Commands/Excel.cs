using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Sugar.Helpers;

namespace Sugar.Components.Commands
{
    class Excel : ICommand
    {
        static public void Init(ICommandManager commandManager)
        {
            commandManager.AddCommandHandler(new Excel());
        }

        public string Name
        {
            get { return "Excel"; }
        }

        public string[] ParamList
        {
            get { return new string[] { "File Name", "Password" }; } 
        }

        public string Help
        {
            get { return "<h3>Excel</h3><p>Creates an Excel file with the contents of the clipboard. Has two params.</p><dl><dt>File Name <span class='label label-default'>optional</span></dt><dd>File name to use when creating Excel file. If file name is not provide, a temp file name will be created.</dd><dt>Password <span class='label label-default'>optional</span></dt>  <dd>Encrypt file using this password.</dd></dl>"; }
        }

        public bool Execute(string[] args)
        {
            string text = Clipboard.GetText();
            string password = "";
            string fileName = Path.GetTempFileName();
            if (args.Length > 1 && !string.IsNullOrWhiteSpace(args[1]))
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                fileName = path + "\\" + args[1].Trim();
            }

            if (args.Length > 2 && !string.IsNullOrWhiteSpace(args[2]))
            {
                password = args[2].Trim();
            }

            fileName = Path.ChangeExtension(fileName, ".xlsx");

            if (!string.IsNullOrWhiteSpace(text))
            {
                BuildExcel(text, fileName, password);
            }
            return true; // hide command window
        }


        public void BuildExcel(string input, string fileName, string password)
        {
            List<object[]> Data = new List<object[]>();
            List<string> lines = input.SplitLines();
            int rowCount = 0;
            int colCount = 0;

            foreach (string line in lines)
            {
                List<string> fields = line.SplitTabs();
                colCount = fields.Count();
                object[] row = new object[colCount];
                for (int i = 0; i < colCount; i++)
                {
                    row[i] = fields[i];
                }
                Data.Add(row);
                rowCount++;
            }



            using (ExcelPackage pck = new ExcelPackage())
            {
                //Create the worksheet
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Sheet1");

                if( !string.IsNullOrWhiteSpace( password ) )
                {
                    pck.Encryption.Password = password;
                    pck.Encryption.Algorithm = EncryptionAlgorithm.AES192;
                    pck.Workbook.Protection.LockStructure = true;
                }

                //Load the datatable into the sheet
                if (rowCount > 0)
                {
                    ws.Cells["A1"].LoadFromArrays(Data);

                    //Format the header for columns
                    using (ExcelRange rng = ws.Cells[1, 1, 1, colCount])
                    {
                        rng.Style.Font.Bold = true;
                        rng.Style.Fill.PatternType = ExcelFillStyle.Solid;                      //Set Pattern for the background to Solid
                        rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189));  //Set color to dark blue
                        rng.Style.Font.Color.SetColor(Color.White);
                        for (int i = 1; i <= rng.Count(); i++)
                        {
                            ws.Column(i).AutoFit();
                        }
                    }

                }
                

                if (SaveData(fileName, pck.GetAsByteArray()))
                {
                    System.Diagnostics.Process.Start(fileName);
                }
            }
        }

        protected bool SaveData(string FileName, byte[] Data)
        {
            bool retVal = false;
            BinaryWriter Writer = null;

            try
            {
                // Create a new stream to write to the file
                Writer = new BinaryWriter(File.OpenWrite(FileName));

                // Writer raw data                
                Writer.Write(Data);
                Writer.Flush();
                Writer.Close();
                retVal = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return retVal;
        }
    }
}
