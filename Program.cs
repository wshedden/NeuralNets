using System;
using System.Collections.Generic;

namespace Neural_Networks { //---------------------------------- THE GENETIC ALGORITHM DOESN'T WORK PROPERLY - TURN BACK HERE --------------------------------------\\
    class Program {
        static void Main (string[] args) {

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

        static double[] SortArray (double[] arr) {
            List<double> l = new List<double> (arr);
            return SortList (l).ToArray ();
        }

        static List<double> SortList (List<double> l) {
            if (l.Count == 1) return l;
            List<double> left = SortList (l.GetRange (0, l.Count / 2));
            List<double> right = SortList (l.GetRange (l.Count / 2, l.Count-(l.Count / 2)));
            return Merge (left, right);

        }

        static List<double> Merge (List<double> a, List<double> b) {
            List<double> c = new List<double> ();
            int i = 0;
            int k = 0;
            while (i < a.Count && k < b.Count) {
                if (a[i] < b[k]) {
                    c.Add (a[i]);
                    i++;
                } else {
                    c.Add (b[k]);
                    k++;
                }
            }
            if (i == a.Count) {
                c.AddRange (b.GetRange (k, b.Count - k));
            }
            if (k == b.Count) {
                c.AddRange (a.GetRange (i, a.Count - i));
            }

            return c;

        }

        public static double DoubleSum (double[] arr) {
            double total = 0;
            for (int i = 0; i < arr.Length; i++) {
                total += arr[i];
            }
            return total;
        }

        public static double Sigmoid (double n) {
            return 1d / (1d + (double) Math.Exp (-n));
        }

        public static void PrintArray (double[] arr) {
            foreach (double i in arr) {
                Console.WriteLine (i);
            }
        }

        public static void PrintArray (int[] arr) {
            foreach (int i in arr) {
                Console.WriteLine (i);
            }
        }

    }

}