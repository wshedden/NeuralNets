﻿using System;

namespace GeneticAlgo {

    enum Type {
        INPUT, HIDDEN, OUTPUT
    }

    class Program {
        public static Random random = new Random();

        public static double DotProd (double[] a, double[] b) {
            double total = 0;
            for(int i = 0; i < a.Length; i++) {
                total += a[i] * b[i];
             }
            return total;
        }

        public static double Sigmoid (double x) {
            return 1d / (1d + Math.Exp(-x));
        }

        static void Main(string[] args) {
            int[] dimensions = new int[] { 2, 10, 2 };
            double[][] inputs = {new double[] { 2, 3 }};
            double[][] expected = {new double[] {0.5, 0.5}};
            Population population = new Population(10, dimensions);

            population.NaturalSelection(inputs, expected, epochs: 10000).PrintValues();
            Console.ReadKey();
        }
    }
}