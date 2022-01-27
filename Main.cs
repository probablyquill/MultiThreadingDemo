using System;

namespace BruteForceExample {
    class Handler {
        static void Main(string[] args) {
            String response;
            int charNum;
            Boolean upperIncluded = false;
            Boolean numbersIncluded = false;
            Boolean specialIncluded = false;
            int threads = 0;
            String outputText = "";
            
            Console.WriteLine("Multithreading yields a performance benifit when the generated text is three characters or longer.");
            Console.WriteLine("Generating two characters or fewer results in worse performance the more threads are made.");
            Console.WriteLine("Threaded:\n\ta.) Yes\n\tb.) No");

            //Example from tests (threads, length):
            //1, 3 -> 144ms || 4, 3 -> 36ms || 8, 3 -> 33ms || 26, 3 -> 17msd
            //1, 2 -> 1ms   || 4, 2 -> 2ms  || 8, 2 -> 3ms  || 26, 2 -> 13ms

            response = Console.ReadLine().ToUpper();
            if (response == "A" || response =="Y") {
                Console.WriteLine("How many threads (approximately) should be made?");
                try {
                    threads = int.Parse(Console.ReadLine());
                } catch {
                    //Bad input
                    threads = 0;
                }
            }

            Console.WriteLine("Number of Characters:");
            try {
                charNum = int.Parse(Console.ReadLine());
            } catch {
                //Bad input, assume 2.
                charNum = 2;
            }

            Console.WriteLine("Include Upper Case:\n\ta.) Yes\n\tb.) No");
            response = Console.ReadLine().ToUpper();
            if (response == "A" || response =="Y") {
                upperIncluded = true;
                Console.WriteLine("Including Upper Case");
            } else {
                Console.WriteLine("Not Including Upper Case.");
            }

            Console.WriteLine("Include Numbers:\n\ta.) Yes\n\tb.) No");
            response = Console.ReadLine();
            if (response == "A" || response =="Y") {
                numbersIncluded = true;
                Console.WriteLine("Including Numbers");
            } else {
                Console.WriteLine("Not Including Numbers.");
            }

            Console.WriteLine("Include Special Characters:\n\ta.) Yes\n\tb.) No");
            response = Console.ReadLine();
            if (response == "A" || response =="Y") {
                specialIncluded = true;
                Console.WriteLine("Including Special Characters");
            } else {
                Console.WriteLine("Not Including Special Characters.");
            }

            Console.WriteLine("Running...\n");
            var watch = System.Diagnostics.Stopwatch.StartNew();

            //Creates a new runner object to handle the generation based on how many threads it has been told to use.
            if (threads > 1) {
                MultiThreaded runner = new MultiThreaded(upperIncluded, numbersIncluded, specialIncluded, charNum, threads);
                outputText = runner.run();
            } else {
                SingleThreaded runner = new SingleThreaded(upperIncluded, numbersIncluded, specialIncluded, charNum);
                outputText = runner.run();
            }   

            //Output results
            watch.Stop();
            Console.WriteLine("The generation took " + watch.ElapsedMilliseconds + "ms to complete.");
            //Wait to exit
            Console.ReadLine();
        }
    }
}
