using System;

namespace BruteForceExample
{
    class SingleThreaded
    {
        Boolean uppercase = false;
        Boolean useNumbers = false;
        Boolean useSpecialChars = false;
        String output = "";
        int length = 0;
        public SingleThreaded() {
            this.uppercase = false;
            this.useNumbers = false;
            this.useSpecialChars = false;
            this.length = 2;
        }
        public SingleThreaded(Boolean uppercase, Boolean numbers, Boolean specialChars, int length) {
            this.uppercase = uppercase;
            this.useNumbers = numbers;
            this.useSpecialChars = specialChars;
            this.length = length;
        }
        public String run()
        {
            String characters = "abcdefghijklmnopqrstuvwxyz";
            String numbers = "1234567890";
            String specialChars = "!@#$%^&*()<>_+-=";

            if (this.uppercase == true) characters += characters.ToUpper();
            if (this.useNumbers == true) characters += numbers;
            if (this.useSpecialChars == true) characters += specialChars;

            generate(this.length, 0, "", 0, characters.Length, characters);
            return this.output;
        }

        void generate(int length, int depth, String previous, int start, int end, String characters) {
            String saved = previous;

            if (depth < length - 1) {
                for (int i = 0; i < characters.Length; i++) {
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