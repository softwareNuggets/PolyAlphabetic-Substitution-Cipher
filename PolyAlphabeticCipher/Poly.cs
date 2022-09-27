using System;


namespace PolyAlphabeticCipher
{
    public class Poly
    {
        public const int KEYROWS = 1024;

        private string[] VocabularySet = new string[KEYROWS];
        public char[,] cipherText = new char[KEYROWS, 256];


        public bool InitPoly()
        {
            //load keys from key class
            bool rv = LoadPolyAlphabetKeys();
            if (rv == true)
            {
                BuildPolyAlphabeticCipher();
                return (true);
            }

            return (false);
        }

        private void BuildPolyAlphabeticCipher()
        {
            int cLength = VocabularySet[0].Length;
            int i = 0;

            //AB231F862C44BB832EC53D45825464438908D3

            for (int r = 0; r < VocabularySet.Length; r++)
            {
                i = 0;
                for (int c = 0; c < cLength; c += 2)
                {
                    // pattern on loop will equal "AB"
                    string pattern = VocabularySet[r][c].ToString() + VocabularySet[r][c + 1].ToString();

                    // cipherText[0,0]= 171 '«'
                    cipherText[r, i] += (char)Convert.ToInt32(pattern, 16);
                    i++;
                }

            }
        }

        private bool LoadPolyAlphabetKeys()
        {
            try
            {
                var asciiKeys = new Key();
                asciiKeys.LoadKey();

                for (int i = 0; i < KEYROWS; i++)
                {
                    VocabularySet[i] = asciiKeys.key[i];
                }

                return (true);
            }
            catch
            {
                return (false);
            }

        }
    }
}
