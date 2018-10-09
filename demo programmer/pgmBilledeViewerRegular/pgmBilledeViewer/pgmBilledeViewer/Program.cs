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
        public static Bitmap indlæsBillede(string filepath)
        {
            //string filename = @"..\..\demobilleder\" + "feep_p2_plain.pgm";
            foreach (string line in File.ReadLines(filepath))
            {
                Console.WriteLine("-- {0}", line);
            }

            //FileStream fs = File.OpenRead(filename);

            Filetype fileType = 0;

            UInt32 width = 0;
            UInt32 height = 0;
            UInt32 maxval = 0;

            int filePointer = 0;

            StreamReader sr = new StreamReader(File.OpenRead(filepath));
            string linje1 = sr.ReadLine();
            filePointer += linje1.Length + 1;


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
                else if (linje1[1] == '2' || linje1[1] == '5')
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
                    filePointer += line.Length + 1;
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
                        filePointer += line.Length + 1;
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

                //sr.BaseStream.

                BinaryReader br = new BinaryReader(sr.BaseStream);
                br.BaseStream.Position = filePointer;

                byte tal;
                while (br.BaseStream.Length > br.BaseStream.Position)
                {
                    tal = br.ReadByte();
                    bits[ptr++] = Convert.ToByte(tal);                   
                }

                bitmappet = new Bitmap((int)width, (int)height);
                int x = 0, y = 0;

                uint i = 0;
                foreach (var b in bits)
                {
                    //if (b >= 1)
                    //    Console.Write("X");
                    //else
                    //    Console.Write(" ");

                    byte brightPixel = (byte)(b * (255 / maxval));

                    System.Drawing.Color c = Color.FromArgb(brightPixel, brightPixel, brightPixel);
                    bitmappet.SetPixel(x, y, c);
                    
                    x++;
                    if (++i % width == 0)
                    {
                        //Console.WriteLine();
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

            

            //indlæsBillede();

        }
    }
}
