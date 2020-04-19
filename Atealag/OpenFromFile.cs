using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atealag
{
    /*  Author: Danny Lillard of Aetalag dev team
     *  Date: 4/19/2020
     *  Description: This class runs when a user opens a file, filling in all fields with values from the file.
     */
    class OpenFromFile
    {
        public OpenFromFile(string fileName)
        {
            List<string> text = new List<string>();
            using (StreamReader reader = new StreamReader(fileName))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    text.Add(line);
                }
            }
            CharacterSheet newCharSheet = new CharacterSheet(fileName);
            newCharSheet.Show();
            Sheet loadSheet = new Sheet(fileName);
            //20
            loadSheet.userCharVals = new CharVals(text[0], text[1], text[2], text[3], text[4], text[5], text[6], text[7],
                                                  text[8], text[9], text[10], text[11], text[12], text[13], text[14], text[15],
                                                  text[16], text[17], text[18], text[19], text[20]);
        }
    }
}
