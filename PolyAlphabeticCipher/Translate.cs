using System;
using System.Text;
using System.Windows;

namespace PolyAlphabeticCipher
{
    public class Translate : Poly
    {

        public string Encrypt(string source)
        {
            var text = new StringBuilder();
            int index = 0;

            for (int i = 0; i < source.Length; i++)
            {
                int p_index = i % Poly.KEYROWS;

                char ch = source[i];
                if (ch > 255)
                {
                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine(string.Format("The character: {0} = {1}", source[i], (int)ch));
                    sb.AppendLine();
                    sb.AppendLine("is not a valid ASCII value in the extended chart.");
                    sb.AppendLine();

                    MessageBox.Show(sb.ToString(), "Invalid Character");
                    return string.Empty;

                }

                char t = cipherText[p_index, (int)ch];
                text.AppendFormat("{0:X2}-", (int)t);

                if (index == 24)
                {
                    text.AppendLine();
                    index = 0;
                }
                else
                {
                    index++;
                }
            }

            return (text.ToString());
        }

        public string Decrypt(string hexString)
        {
            char[] sep = { (char)10, (char)13 };    // cr & lf

            var rows = hexString.Trim().Split(sep, StringSplitOptions.RemoveEmptyEntries);
            var sb = new StringBuilder();


            int row_index = 0;
            for (int r = 0; r < rows.Length; r++)
            {
                for (int i = 0; i < rows[r].Length; i += 3)
                {
                    string chInHex = rows[r].Substring(i, 2);


                    int ch = Convert.ToInt32(chInHex, 16);

                    for (int z = 0; z < 256; z++)
                    {
                        if (cipherText[row_index % Poly.KEYROWS, z] == (char)ch)
                        {
                            sb.Append((char)z);
                            break;
                        }
                    }

                    row_index = row_index + 1;
                }
            }

            return (sb.ToString());
        }

    }
}
