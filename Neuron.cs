using System;

namespace GeneticAlgo {
    class Neuron {
        public double[] weights;
        public double bias;
        public double value;

        public Neuron(int n) {
            weights = new double[n];
            Initialise();
        }

        private void Initialise(double minW = -1, double maxW = 1, double minB = -5, double maxB = 5) {
            bias = Program.random.NextDouble() * (maxB - minB) + minB;
            for (int i = 0; i < weights.Length; i++) {
                weights[i] = Program.random.NextDouble() * (maxW - minW) + minW;
            }
        }

        public void Mutate (double rate){
            if(Program.random.NextDouble() < rate) Initialise();
        }

        public void PrintValues() {
            Console.WriteLine($"\t\tBias = {bias}");
            Console.WriteLine($"\t\t{weights.Length} Weights: ");
            for (int i = 0; i < weights.Length; i++) {
                Console.WriteLine($"\t\t\tWeights [{i}] = {weights[i]}");
            }
            Console.WriteLine($"\t\tValue = {value}");
        }
    }
}
    