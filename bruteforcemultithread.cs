using System;

namespace BruteForceExample
{
    class MultiThreaded
    {
        Boolean uppercase = false;
        Boolean useNumbers = false;
        Boolean useSpecialChars = false;
        String output = "";
        int threads = 0;
        int length = 0;
        public MultiThreaded() {
            this.uppercase = false;
            this.useNumbers = false;
            this.useSpecialChars = false;
            this.length = 2;
            int threads = 2;
        }
        public MultiThreaded(Boolean uppercase, Boolean numbers, Boolean specialChars, int length, int threads) {
            this.uppercase = uppercase;
            this.useNumbers = numbers;
            this.useSpecialChars = specialChars;
            this.length = length;
            this.threads = threads;
        }
        public String run()
        {
            String characters = "abcdefghijklmnopqrstuvwxyz";
            String numbers = "1234567890";
            String specialChars = "!@#$%^&*()<>_+-=";
            int sectionSize;

            if (this.uppercase == true) characters += characters.ToUpper();
            if (this.useNumbers == true) characters += numbers;
            if (this.useSpecialChars == true) characters += specialChars;

            sectionSize = characters.Length / this.threads;

            for (int i = 0; i < characters.Length; i+=sectionSize) {
                if (i < characters.Length) i = characters.Length;
            }

            generate(this.length, 0, "", 0, characters.Length, characters);
            return this.output;
        }

        void generate(int length, int depth, String previous, int start, int end, String characters) {
            String saved = previous;

            if (depth < length - 1) {
                for (int i = 0; i < characters.Length; i++) {
                    if (depth == 0) Console.WriteLine(characters[i]);
                    previous += characters[i];
                    generate(length, depth + 1, previous, start, end, characters);
                    previous = saved;
                } 
            } else {
                for (int i = 0; i < characters.Length; i++) {
                    this.output += previous + characters[i] + "\n";
                }
            }
        }
    }
}