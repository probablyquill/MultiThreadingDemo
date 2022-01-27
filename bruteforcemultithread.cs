using System;
using System.Threading;

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
        String[] outputContainer;
        public MultiThreaded() {
            this.uppercase = false;
            this.useNumbers = false;
            this.useSpecialChars = false;
            this.length = 2;
            this.threads = 2;
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
            int sectionSize, threadNumber;

            if (this.uppercase == true) characters += characters.ToUpper();
            if (this.useNumbers == true) characters += numbers;
            if (this.useSpecialChars == true) characters += specialChars;

            sectionSize = characters.Length / this.threads;
            threadNumber = 0;
            if (this.threads > characters.Length) this.threads = characters.Length;
            Thread[] threadContainer = new Thread[this.threads + 1];
            this.outputContainer = new String[this.threads + 1];

            for (int i = 0; i < characters.Length; i+=sectionSize) {
                int nextStep = i + sectionSize;
                int a = i;
                String character2 = characters;
                int backupNumber = threadNumber;
                int newLength = this.length;

                if (nextStep > characters.Length) nextStep = characters.Length;

                Console.WriteLine("Starting thread with parameters: " + i + ", " + nextStep + ", " + threadNumber);
                threadContainer[threadNumber] = new Thread(() => generate(newLength, 0, "", a, nextStep, character2, backupNumber));
                threadContainer[threadNumber].Start();
                threadNumber += 1;
            }
            for (int i = 0; i < this.threads; i++) {
                threadContainer[i].Join();
                output += outputContainer[i];
            }

            return output;
        }

        void generate(int length, int depth, String previous, int start, int end, String characters, int threadNum) {
            String saved = previous;
            if (depth < length - 1) {
                for (int i = start; i < end; i++) {
                    previous += characters[i];
                    generate(length, depth + 1, previous, 0, characters.Length, characters, threadNum);
                    previous = saved;
                }
            } else {
                for (int i = 0; i < characters.Length; i++) {
                    this.outputContainer[threadNum] += previous + characters[i] + "\n";
                }
            }
        }
    }
}