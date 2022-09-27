using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;

namespace PolyAlphabeticCipher
{
    public class VocabularyKeyGenerator
    {
        public void GenerateSet(int numberOfKeys)
        {
            Random random = new Random(DateTime.Now.Millisecond);
            List<string> PolyKey = new List<string>();

            for (int keys = 0; keys < numberOfKeys; keys++)
            {
                List<int> inputSetOfNumbersFrom_0_to_255 = new List<int>();
                List<int> outputRandomNumbers = new List<int>();
                int index = 0;

                for (int i = 0; i < 256; i++)
                {
                    inputSetOfNumbersFrom_0_to_255.Add(i); // numbers are in order 0 to 255
                }

                while (inputSetOfNumbersFrom_0_to_255.Count > 0)
                {
                    //get a random number, let's use it as an INDEX postion
                    index = random.Next(0, inputSetOfNumbersFrom_0_to_255.Count);

                    //from that index position, get the number assigned 
                    int currNum = inputSetOfNumbersFrom_0_to_255[index];

                    //add currNum to the output array
                    outputRandomNumbers.Add(currNum);

                    //using that index position, remove the index position from the list
                    inputSetOfNumbersFrom_0_to_255.RemoveAt(index);
                }

                var key = BuildRandomVocabularKey(outputRandomNumbers);

                PolyKey.Add(key);
            }

            SaveKeys(PolyKey);
        }

        private void SaveKeys(List<string> polyKey)
        {
            if (polyKey == null)    return;
            if (polyKey.Count == 0) return;  


            if (Directory.Exists("C:\\Temp") == false)
            {
                Directory.CreateDirectory("C:\\Temp");
            }

            using StreamWriter file = new(@"c:\Temp\keys.cs");
            {
                for (int i = 0; i < polyKey.Count; i++)
                {
                    file.WriteLine(polyKey[i]);
                }
            }

            MessageBox.Show("A file was created in C:\\Temp\\Keys.cs", "Keys Created");
        }

        private string BuildRandomVocabularKey(List<int> outputRandomNumbers)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("key.Add(\"");

            for (int i = 0; i < outputRandomNumbers.Count; i++)
            {
                sb.AppendFormat("{0:X2}", (int)outputRandomNumbers[i]);
            }
            sb.Append("\");");

            return sb.ToString();
        }
        
    }
}
