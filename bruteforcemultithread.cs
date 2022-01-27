using System;
using System.Threading;

namespace BruteForceExample
{
    class MultiThreaded
    {
        //Initialize Global Variables
        //Importantly these can be accessed by ANY THREAD at ANY TIME
        //Only applicaple for outputContainer in this specific program.
        Boolean uppercase = false;
        Boolean useNumbers = false;
        Boolean useSpecialChars = false;
        String output = "";
        int threads = 0;
        int length = 0;
        String[] outputContainer;

        //Empty constuctor, unused.
        public MultiThreaded() {
            this.uppercase = false;
            this.useNumbers = false;
            this.useSpecialChars = false;
            this.length = 2;
            this.threads = 2;
        }

        //Main / most usable constructor.
        public MultiThreaded(Boolean uppercase, Boolean numbers, Boolean specialChars, int length, int threads) {
            this.uppercase = uppercase;
            this.useNumbers = numbers;
            this.useSpecialChars = specialChars;
            this.length = length;
            this.threads = threads;
        }
        public String run() {
            //Stored possibly letters, numbers, and special characters.
            String characters = "abcdefghijklmnopqrstuvwxyz";
            String numbers = "1234567890";
            String specialChars = "!@#$%^&*()<>_+-=";
            int sectionSize, threadNumber;
            
            //Create the String variable based on the input given to the constructor.
            if (this.uppercase == true) characters += characters.ToUpper();
            if (this.useNumbers == true) characters += numbers;
            if (this.useSpecialChars == true) characters += specialChars;

            //Check to make sure that there are not more threads being requested than there are characters.
            if (this.threads > characters.Length) this.threads = characters.Length;
            
            //Get the size of subsections based on the number of desired threads.
            //Due to the way this works out, asking for 8 threads may
            //get you 9, for example, but I don't care.
            sectionSize = characters.Length / this.threads;
            threadNumber = 0;

            //Create the containers for the threads and the string array which will contain the output.
            Thread[] threadContainer = new Thread[this.threads + 10];
            this.outputContainer = new String[this.threads + 10];

            for (int i = 0; i < characters.Length; i+=sectionSize) {
                //Make the variables thread safe by redefining every variable which will be
                //passed to the function so that a variable is not accessed multiple times at once.
                int nextStep = i + sectionSize;
                int a = i;
                String character2 = characters;
                int backupNumber = threadNumber;
                int newLength = this.length;

                //Make sure that the iteration does not overshoot the maximum length of the character string.
                if (nextStep > characters.Length) nextStep = characters.Length;

                //Create the thread and store it in threadConatiner.
                //Console.WriteLine("Starting thread " + threadNumber + " with parameters: " + i + ", " + nextStep + ".");
                //I don't understand lambda expressions but this works to pass a function with arguments to a thread.
                threadContainer[threadNumber] = new Thread(() => generate(newLength, 0, "", a, nextStep, character2, backupNumber));
                threadContainer[threadNumber].Start();
                threadNumber += 1;
            }
            //Wait for every thread to finish. Regardless of what order they finish in, calling
            //it like this results in the main program not continuing until all threads are finished.
            //After each thread is finished, the data it saved to the outputContainer is added
            //to the output string.
            for (int i = 0; i < this.threads; i++) {
                threadContainer[i].Join();
                output += outputContainer[i];
            }

            return output;
        }

        void generate(int length, int depth, String previous, int start, int end, String characters, int threadNum) {
            //Length: How many characters long is the generated string supposed to be?
            //Depth: How many layers of recursion deep is the function?
            //Previous: The characters up to the current loop, i.e. "aa"
            //Start: Where to start in the characters string
            //End: Where to end in the characters string
            //Characters: the string containing all of the characers to use in generation.

            //Checks to see if the program is at the correct depth to start outputting information.
            
            if (depth < length - 1) {//If it isn't deep enough
                //Saved saves the initial previous variable, so that it can be reset while iterating through
                //the characters 
                String saved = previous;
                for (int i = start; i < end; i++) {
                    previous += characters[i];
                    generate(length, depth + 1, previous, 0, characters.Length, characters, threadNum);
                    previous = saved;
                }
            } else {
                for (int i = 0; i < characters.Length; i++) {
                    //This is probably not good practice, however in practice as each element in the String array
                    //outputContainer is only accessed by a single thread, and main does not touch it until after 
                    //a respective thread is finished, this is fine.
                    this.outputContainer[threadNum] += previous + characters[i] + "\n";
                }
            }
        }
    }
}