using System;

namespace NeuralNets {
    
    class Program {
        static void Main (string[] args) {
            int[] dimensions = new int[]{2, 4, 2};
            Network net = new Network (dimensions);
            net.PrintNeuronValues();

            
            
        }

    
    }

    class NetMath{
        public static double Sum(double[] arr){
            double total = 0;
            Array.ForEach(arr, i => total += i);
            return total;
        }
        public static int Sum(int[] arr){
            int total = 0;
            Array.ForEach(arr, i => total += i);
            return total;
        }

        public static double DotProduct(double[] arr1, double[] arr2){
            double[] productArr = new double[arr1.Length];
            Array.ForEach(Range(arr1.Length), i => productArr[i] = arr1[i] * arr2[i]);
            return Sum(productArr);
            
        }
        public static int DotProduct(int[] arr1, int[] arr2){
            int[] productArr = new int[arr1.Length];
            Array.ForEach(Range(arr1.Length), i => productArr[i] = arr1[i] * arr2[i]);
            return Sum(productArr);
            
        }

        public static int[] Range(int min, int max){
            int[] arr = new int[max-min];
            for(int i = 0; i < arr.Length; i++)
                arr[i] = i;
            return arr;
        }
        public static int[] Range(int max){
            int[] arr = new int[max];
            for(int i = 0; i < arr.Length; i++)
                arr[i] = i;
            return arr;
        }

        public static double Sigmoid(double n){
            return 1d / (1d + (double) Math.Exp(-n));
        }
    }
}