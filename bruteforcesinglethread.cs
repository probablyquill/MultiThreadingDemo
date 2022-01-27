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
            //Default constructor, sets all to false and length to 2.
            this.uppercase = false;
            this.useNumbers = false;
            this.useSpecialChars = false;
            this.length = 2;
        }
        public SingleThreaded(Boolean uppercase, Boolean numbers, Boolean specialChars, int length) {
            //General constructor
            this.uppercase = uppercase;
            this.useNumbers = numbers;
            this.useSpecialChars = specialChars;
            this.length = length;
        }
        public String run()
        {
            //Stored possibly letters, numbers, and special characters.
            String characters = "abcdefghijklmnopqrstuvwxyz";
            String numbers = "1234567890";
            String specialChars = "!@#$%^&*()<>_+-=";

            //Create the String variable based on the input given to the constructor.
            if (this.uppercase == true) characters += characters.ToUpper();
            if (this.useNumbers == true) characters += numbers;
            if (this.useSpecialChars == true) characters += specialChars;

            //Call first run of the recursive generation function.
            generate(this.length, 0, "", 0, characters.Length, characters);
            //Return the output as a string.
            return this.output;
        }

        void generate(int length, int depth, String previous, int start, int end, String characters) {
            //Length: How many characters long is the generated string supposed to be?
            //Depth: How many layers of recursion deep is the function?
            //Previous: The characters up to the current loop, i.e. "aa"
            //Start: Where to start in the characters string
            //End: Where to end in the characters string
            //Characters: the string containing all of the characers to use in generation.

            //Checks to see if the program is at the correct depth to start outputting information.
            if (depth < length - 1) { //If it isn't deep enough
                //Saved saves the initial previous variable, so that it can be reset while iterating through
                //the characters 
                String saved = previous;

                for (int i = 0; i < characters.Length; i++) {
                    previous += characters[i];
                    generate(length, depth + 1, previous, start, end, characters);
                    previous = saved;
                } 
            } else { //If the program has reached the desired recursion depth
                for (int i = 0; i < characters.Length; i++) {
                    this.output += previous + characters[i] + "\n";
                }
            }
        }
    }
}