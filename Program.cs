using System;

namespace NeuralNets {

    class Program {
        static void Main (string[] args) {

            int[] dimensions = new int[] { 2, 2, 2 };
            Dataset data = new Dataset(dimensions[0], dimensions[dimensions.Length-1]);
            data.RetrieveFromFile("data.txt");
            data.PrintData();
            Network net = new Network (dimensions);
            net.PrintNeuronValues ();
            

            

        }
        public static void PrintArray (double[] arr) {
            for (int i = 0; i < arr.Length; i++) {
                Console.WriteLine ($"[{i}] {arr[i]}");
            }
        }

    }

    class NetMath {
        public static double Sum (double[] arr) {
            double total = 0;
            Array.ForEach (arr, i => total += i);
            return total;
        }
        public static int Sum (int[] arr) {
            int total = 0;
            Array.ForEach (arr, i => total += i);
            return total;
        }

        public static double DotProduct (double[] arr1, double[] arr2) {
            double[] productArr = new double[arr1.Length];
            Array.ForEach (Range (arr1.Length), i => productArr[i] = arr1[i] * arr2[i]);
            return Sum (productArr);

        }
        public static int DotProduct (int[] arr1, int[] arr2) {
            int[] productArr = new int[arr1.Length];
            Array.ForEach (Range (arr1.Length), i => productArr[i] = arr1[i] * arr2[i]);
            return Sum (productArr);

        }

        public static int[] Range (int min, int max) {
            int[] arr = new int[max - min];
            for (int i = 0; i < arr.Length; i++)
                arr[i] = i;
            return arr;
        }
        public static int[] Range (int max) {
            int[] arr = new int[max];
            for (int i = 0; i < arr.Length; i++)
                arr[i] = i;
            return arr;
        }

        public static double Sigmoid (double x, bool derivative = false) {
            if(derivative)
                return Math.Exp(-x)/Math.Pow((1+Math.Exp(-x)), 2);
            return 1d / (1d + (double) Math.Exp (-x));
        }
    }
}