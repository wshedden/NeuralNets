
using System;
using System.Collections.Generic;


namespace Neural_Networks { //---------------------------------- THE GENETIC ALGORITHM DOESN'T WORK PROPERLY - TURN BACK HERE --------------------------------------\\
    class Program {
        static void Main(string[] args) {

            int[] dimensions = new int[] { 2, 4, 2 };

            string dir = Environment.CurrentDirectory;
            string dataFileName = "data.txt";
            string fullDir = dir + "\\" + dataFileName;

            string[] lines = System.IO.File.ReadAllLines(fullDir);


            double[][] inputs = new double[lines.Length / 2][];
            double[][] expected = new double[lines.Length / 2][];
            foreach (int k in new int[] { 0, 1 }) {
                for (int i = k; i < lines.Length; i += 2) {
                    string[] lineArr = lines[i].Split(' ');
                    double[] nums = new double[lineArr.Length];
                    for (int j = 0; j < nums.Length; j++) {
                        bool successful = Double.TryParse(lineArr[j], out nums[j]);
                        if (!successful) Console.WriteLine("$[DATA CONVERSION ERROR]; i={i}, j={j}");
                    }
                    if (k == 0)
                        inputs[i / 2] = nums;
                    else
                        expected[i / 2] = nums;

                }
            }


            Population population = new Population(10000, dimensions);

            population.PrintWeights(0);

            double[] testData = new double[] { 5 };

            double[] outputs = population.QuickPropagate(testData);
            string numInput;

        
            population.Train(inputs, expected, epochs: 10000000, interval: 163840);
            Console.WriteLine();

            do {
                double[] nums = new double[2 /*input layer size, ICBA making it a variable*/];
                Console.Write("> ");
                numInput = Console.ReadLine();
                string[] numList = numInput.Split(' ');

                for (int i = 0; i < nums.Length; i++) {
                    try {
                        Double.TryParse(numList[i], out nums[i]);
                    } catch (Exception _) {
                        Console.WriteLine("[ERROR: Wrong number of arguments]");
                    }
                }

                outputs = population.QuickPropagate(nums);
                for(int i = 0; i < outputs.Length; i++) {
                    Console.Write(outputs[i] + " ");
                }
                Console.WriteLine();

            } while (numInput != "q");




            Console.ReadKey();


        }

        public static double DoubleSum(double[] arr) {
            double total = 0;
            for (int i = 0; i < arr.Length; i++) {
                total += arr[i];
            }
            return total;
        }

        public static double Sigmoid(double n) {
            return 1d / (1d + (double)Math.Exp(-n));
        }

        public static void PrintArray(double[] arr) {
            foreach (double i in arr) {
                Console.WriteLine(i);
            }
        }

        public static void PrintArray(int[] arr) {
            foreach (int i in arr) {
                Console.WriteLine(i);
            }
        }



    }


    

    
}