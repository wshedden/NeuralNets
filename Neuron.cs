using System;

namespace Neural_Networks {
    public class Neuron {
        public double[] weights;
        public double bias;
        public int weightNum;
        private double value;

        public Neuron () { }

        public void SetWeightNum (int weightNum) { //MUST SetWeightNum before calling InitializeValues
            this.weightNum = weightNum;
            weights = new double[weightNum];
        }

        public double GetValue () {
            return value;
        }

        public void SetWeights (double[] w) {
            weights = new double[w.Length];
            for (int weight = 0; weight < weights.Length; weight++) {
                weights[weight] = w[weight];
            }
        }

        public void SetValue (double v) {
            value = v;
        }

        public void InitializeValues () {
            Random random = new Random ();
            bias = (random.NextDouble () * 2) - 1;
            for (int i = 0; i < weightNum; i++) {
                weights[i] = (random.NextDouble () * 2) - 1;
            }
        }

        public void PrintNeuronValues () {
            Console.WriteLine ($"V={value}, B={bias}, W={weights != null && weights.Length != 0}");
        }

        public void Mutate (double mutationRate) {
            Random rand = new Random ();

            if (rand.NextDouble () < mutationRate)
                bias = (rand.NextDouble () * 2) - 1;
            for (int i = 0; i < weights.Length; i++) {
                if (rand.Next () < mutationRate)
                    weights[i] = (rand.NextDouble () * 2) - 1;
            }
        }
    }

}