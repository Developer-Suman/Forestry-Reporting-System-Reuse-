using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reuse.Bll.Helpher
{
    public class UniCodeHelpher
    {

        public string ConvertNepaliToEnglishAndViceVersa(string value)
        {
            char[] valueCharArray= value.ToCharArray();
            char convertedCharacter;
            string? convertedValue = null;


            foreach (char character in valueCharArray)
            {
                switch(character)
                {
                    case '१':
                        convertedCharacter = '1';
                        break;
                    case '२':
                        convertedCharacter = '2';
                        break;
                    case '३':
                        convertedCharacter = '3';
                        break;
                    case '४':
                        convertedCharacter = '4';
                        break;
                    case '५':
                        convertedCharacter = '5';
                        break;
                    case '६':
                        convertedCharacter = '6';
                        break;
                    case '७':
                        convertedCharacter = '7';
                        break;
                    case '८':
                        convertedCharacter = '8';
                        break;
                    case '९':
                        convertedCharacter = '9';
                        break;
                    case '०':
                        convertedCharacter = '0';
                        break;
                    default:
                        convertedCharacter = character;
                        break;
                }

                convertedValue = convertedValue + convertedCharacter;

            }
            return convertedValue;
        }


        public string? ConvertEnglishToNepali(string value)
        {
            char[] valueCharArray = value.ToCharArray();
            char convertedCharacter;
            string? convertedValue = null;

            foreach(char character in valueCharArray)
            {
                switch(character)
                {
                    case '1':
                        convertedCharacter = '१';
                        break;
                    case '2':
                        convertedCharacter = '२';
                        break;
                    case '3':
                        convertedCharacter = '३';
                        break;
                    case '4':
                        convertedCharacter = '४';
                        break;
                    case '5':
                        convertedCharacter = '५';
                        break;
                    case '6':
                        convertedCharacter = '६';
                        break;
                    case '7':
                        convertedCharacter = '७';
                        break;
                    case '8':
                        convertedCharacter = '८';
                        break;
                    case '9':
                        convertedCharacter = '९';
                        break;
                    case '0':
                        convertedCharacter = '०';
                        break;
                    default:
                        convertedCharacter = character;
                        break;
                }

                convertedValue = convertedValue + convertedCharacter;
            }

            return convertedValue;
        }
    }
}
