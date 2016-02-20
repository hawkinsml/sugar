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
        static public void Init()
        {
            CommandManager.AddCommandHandler(new MakeUpper());
        }

        public string Name
        {
            get { return "Commas"; }
        }

        public void Execute(string[] args)
        {
            string text = Clipboard.GetText();
            if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
            {
                text = args[0];
            }

            if (!string.IsNullOrWhiteSpace(text))
            {
                BuildExcel(text);
            }
        }


        public void BuildExcel(string input)
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
                string fileName = Path.GetTempFileName();
                fileName = Path.ChangeExtension(fileName, ".xlsx");

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
