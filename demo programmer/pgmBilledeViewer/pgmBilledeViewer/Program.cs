using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Drawing;

namespace pgmBilledeViewer
{
    static class Program
    {
        enum Filetype
        {
            bitmap = 1,
            graymap = 2,
            pixelmap = 3
        };
        public static Bitmap indlæsBillede()
        {
            string filename = @"..\..\demobilleder\" + "feep_p2_plain.pgm";
            foreach (string line in File.ReadLines(filename))
            {
                Console.WriteLine("-- {0}", line);
            }

            //FileStream fs = File.OpenRead(filename);

            Filetype fileType = 0;

            UInt32 width = 0;
            UInt32 height = 0;
            UInt32 maxval = 0;

            StreamReader sr = new StreamReader(File.OpenRead(filename));
            string linje1 = sr.ReadLine();

            Bitmap bitmappet = new Bitmap(1, 1);

            /***************************************************************
             * Indlæser header
             * *************************************************************/

            if (linje1[0] == 'P')
            {
                if (linje1[1] == '1')
                {
                    fileType = Filetype.bitmap;
                }
                else if (linje1[1] == '2')
                {
                    fileType = Filetype.graymap;
                }
                else if (linje1[1] == '3')
                {
                    fileType = Filetype.pixelmap;
                }
            }
            if (fileType != 0)
            {
                string line;

                while ((width == 0 || height == 0) && (line = sr.ReadLine()) != null)
                {
                    if (line[0] != '#')
                    {
                        int whiteSpaceLoc = line.Trim().IndexOfAny(new char[] { ' ', "\t"[0], "\n"[0] });
                        if (line.Length > 0 && whiteSpaceLoc > 0)
                        {
                            width = Convert.ToUInt32(line.Trim().Substring(0, whiteSpaceLoc));
                            height = Convert.ToUInt32(line.Trim().Substring(whiteSpaceLoc));
                        }
                        else if (width == 0)
                        {
                            width = Convert.ToUInt32(line);
                        }
                        else
                        {
                            height = Convert.ToUInt32(line);
                        }
                    }
                }

                if (fileType == Filetype.graymap)
                {
                    line = "";

                    while (maxval == 0 && (line = sr.ReadLine()) != null)
                    {
                        if (line[0] != '#')
                        {
                            maxval = Convert.ToUInt32(line);
                        }
                    }
                }

                Console.WriteLine("Type {0}", fileType);
                Console.WriteLine("Width {0}", width);
                Console.WriteLine("Height {0}", height);
                Console.WriteLine("MaxVal {0}", maxval);


                /**************************************************
                 * Indlæser pixels
                 * ************************************************/

                Byte[] bits = new Byte[width * height];
                UInt32 ptr = 0;

                while ((line = sr.ReadLine()) != null)
                {
                    if (line[0] != '#')
                    {
                        //foreach(char tegn in line)

                        string[] tallene = line.Replace("  ", " ").Split(' ');
                        foreach (string tal in tallene)
                        {
                            //if (tegn != ' ')
                            //{
                            bits[ptr++] = Convert.ToByte(tal);
                            //}
                        }
                    }
                }

                bitmappet = new Bitmap((int)width, (int)height);
                int x = 0, y = 0;

                uint i = 0;
                foreach (var b in bits)
                {
                    if (b >= 1)
                        Console.Write("X");
                    else
                        Console.Write(" ");

                    System.Drawing.Color c = Color.FromArgb(b, b, b);
                    bitmappet.SetPixel(x, y, c);



                    x++;
                    if (++i % width == 0)
                    {
                        Console.WriteLine();
                        x = 0;
                        y++;
                    }

                }
                

            } // end if 'P'
            return bitmappet;
        }
            

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            

            indlæsBillede();

        }
    }
}
