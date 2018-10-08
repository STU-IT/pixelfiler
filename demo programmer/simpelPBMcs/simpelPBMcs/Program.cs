

/*

En demo fra Søren Magnusson smag@tec.dk

Implementerer http://netpbm.sourceforge.net/doc/pbm.html#plainpbm

*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace simpelPBMcs
{
    class Program
    {
        enum Filetype
        {
            bitmap = 1,
            graymap = 2,
            pixelmap = 3
        };

        static void Main(string[] args)
        {
            string filename = @"..\..\demobilleder\" + "feep.pbm";
            foreach (string line in File.ReadLines(filename))
            {
                Console.WriteLine("-- {0}", line);
            }

            //FileStream fs = File.OpenRead(filename);

            Filetype fileType  = 0;

            UInt32 width = 0;
            UInt32 height = 0;
            
            StreamReader sr = new StreamReader(File.OpenRead(filename));
            string linje1 = sr.ReadLine();

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

                Console.WriteLine("Type {0}", fileType);
                Console.WriteLine("Width {0}", width);
                Console.WriteLine("Height {0}", height);

                /**************************************************
                 * Indlæser pixels
                 * ************************************************/

                Byte[] bits = new Byte[width * height];
                UInt32 ptr = 0;

                while ( (line = sr.ReadLine()) != null)
                {
                    if (line[0] != '#')
                    {
                        foreach(char tegn in line)
                        {
                            if (tegn != ' ')
                            {
                                bits[ptr++] = Convert.ToByte(tegn - 48);
                            }
                        }
                    }
                }

                uint i = 0;
                foreach (var b in bits)
                {
                    if (b == 1)
                        Console.Write("X");
                    else
                        Console.Write(" ");

                    if (++i % width == 0)
                    {
                        Console.WriteLine();
                    }
                }

                        //while ((line = sr.ReadLine()) != null)
                        //{
                        //    if (line[0] != '#') // ignorer linier der starter med #
                        //    {

                        //    }
                        //}

            } // end if 'P'
            
        }
    }
}
