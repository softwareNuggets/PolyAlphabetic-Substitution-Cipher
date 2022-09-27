using System;
using System.Text;
using System.Windows;


namespace PolyAlphabeticCipher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            TBSource.Text = "Can you imagine, polyalphabetic substitution ciphers\n" +
                            "were invented in 1467, by Leon Battista Alberti.";

        }


        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            TBOutput.Text = "";
            TBSource.Text = "";
            XorTextBox.Text = "";
        }

        private void BtnGenerateKey_Click(object sender, RoutedEventArgs e)
        {
            VocabularyKeyGenerator vkg = new VocabularyKeyGenerator();
            vkg.GenerateSet(1024);
        }

        private void BtnEncrypt_Click(object sender, RoutedEventArgs e)
        {

            var key = XorTextBox.Text.ToString();
            string source = TBSource.Text.ToString().Trim();

            string MessageWithXor = XorMessage(source, key);

            var t = new Translate();

            t.InitPoly();

            string s = t.Encrypt(MessageWithXor);
            TBOutput.Text = s;
        }

        private void BtnDecrypt_Click(object sender, RoutedEventArgs e)
        {

            var key = XorTextBox.Text.ToString();
            string output = TBOutput.Text.ToString().Trim();


            var t = new Translate();
            t.InitPoly();

            string original = t.Decrypt(output);

            string MessageWithXor = XorMessage(original, key);

            TBSource.Text = MessageWithXor;
        }

        private string XorMessage(string message, string key)
        {
            if (key == null) return message;
            if (key.Length == 0) return message;
            StringBuilder sb = new StringBuilder();



            for (int i = 0; i < message.Length; i++)
            {
                int ch = message[i];
                ch ^= (int)key[i % key.Length];

                sb.Append((char)ch);
            }

            return (sb.ToString());


        }
    }
}
