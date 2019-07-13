using System;

namespace NeuralNets {
    class Neuron {
        private double[] weights;
        private double bias;
        private double val;
        public Neuron (int weightNum = 0) {
            weights = new double[weightNum];
        }

        public void RandomizeFields
            (double minWeights = -5, double maxWeights = 5, double minBias = -2, double maxBias = 2) {
                Random random = new Random ();
                for (int i = 0; i < weights.Length; i++) {
                    weights[i] = random.NextDouble () * (maxWeights - minWeights) + minWeights;
                }
                bias = random.NextDouble () * (maxBias - minBias) + minBias;
            }

        public void PrintNeuronValues () {
            Console.WriteLine ($"Bias = {bias}\nWeights: ");
            Array.ForEach(weights, weight => Console.WriteLine("\t"+weight));
            Console.WriteLine();
        }

        public double[] Weights {
            get {
                return weights;
            }
            set {
                if (value.Length == weights.Length) weights = value;
            }
        }

        public double Bias {
            get {
                return bias;
            }
            set {
                bias = value;
            }
        }

        public double Val{
            get{
                return val;
            }
            set{
                val = value;
            }
        }

    }
}