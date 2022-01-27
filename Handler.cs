using System;

namespace BruteForceExample
{
    class Handler
    {
        static void Main(string[] args)
        {
            String response;
            int charNum;
            Boolean upperIncluded = false;
            Boolean numbersIncluded = false;
            Boolean specialIncluded = false;
            int threads = 0;
             String outputText = "";

            Console.WriteLine("Threaded:\n\ta.) Yes\n\tb.) No");
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
                //Bad input
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

            if (threads > 0) {
                MultiThreaded runner = new MultiThreaded(upperIncluded, numbersIncluded, specialIncluded, charNum, threads);
                outputText = runner.run();
            } else {
                SingleThreaded runner = new SingleThreaded(upperIncluded, numbersIncluded, specialIncluded, charNum);
                outputText = runner.run();
            }   

            watch.Stop();
            Console.WriteLine(outputText);
            Console.WriteLine("The generation took " + watch.ElapsedMilliseconds + "ms to complete.");
        }
    }
}
