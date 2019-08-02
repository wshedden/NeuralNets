using System;

namespace Neural_Networks {
    public class Network {
        private Layer[] layers;
        public double error;

        public Network (int[] dimensions) {
            int numOfLayers = dimensions.Length;
            layers = new Layer[numOfLayers];
            layers[0] = new Layer (dimensions[0]);
            for (int i = 1; i < numOfLayers; i++) {
                layers[i] = new Layer (dimensions[i], layers[i - 1].neurons.Length);
            }
        }

        public void PrintNeuronValues () {
            for (int i = 0; i < layers.Length; i++) {
                Console.WriteLine ($"Layer {i}: ");
                layers[i].PrintNeuronValues ();
                Console.WriteLine ();
            }
        }

        public void SetWeights (double[][][] w) {
            for (int layer = 1; layer < layers.Length; layer++) {
                layers[layer].SetWeights (w[layer - 1]);
            }
        }
        public void SetBiases (double[][] b) {
            for (int layer = 1; layer < layers.Length; layer++) {
                layers[layer].SetBiases (b[layer - 1]);
            }
        }

        public void PrintWeights () {
            double[][][] weights = GetWeights ();
            for (int i = 0; i < weights.Length; i++) {
                for (int j = 0; j < weights[i].Length; j++) {
                    for (int k = 0; k < weights[i][j].Length; k++) {
                        Console.Write (weights[i][j][k]);
                    }
                    Console.WriteLine ();
                }
                Console.WriteLine ("\n\n");
            }
        }

        public void Propagate () {
            for (int i = 1; i < layers.Length; i++) {
                Layer prevLayer = layers[i - 1];
                for (int j = 0; j < layers[i].neurons.Length; j++) {
                    double[] multiplied = new double[prevLayer.neurons.Length];
                    for (int k = 0; k < prevLayer.neurons.Length; k++) {
                        multiplied[k] = prevLayer.neurons[k].GetValue () * layers[i].neurons[j].weights[k];
                    }
                    double summed = Program.DoubleSum (multiplied) + layers[i].neurons[j].bias;
                    double activated = Program.Sigmoid (summed);
                    layers[i].neurons[j].SetValue (activated);
                }
            }
        }

        public void SetInputs (double[] inputs) {
            for (int i = 0; i < inputs.Length; i++) {
                layers[0].neurons[i].SetValue (inputs[i]);
            }

        }
        public double[][][] GetWeights () {
            double[][][] l = new double[layers.Length - 1][][];

            for (int layer = 1; layer < layers.Length; layer++) { //Starts at 1 to avoid input layer, which has no weights.
                l[layer - 1] = new double[layers[layer].neurons.Length][];
                for (int neuron = 0; neuron < layers[layer].neurons.Length; neuron++) {

                    l[layer - 1][neuron] = layers[layer].neurons[neuron].weights;
                }
            }
            return l;
        }

        public double[][] GetBiases () {
            double[][] l = new double[layers.Length - 1][];

            for (int layer = 1; layer < layers.Length; layer++) { //Starts at 1 to avoid input layer, which has no biases.
                l[layer - 1] = new double[layers[layer].neurons.Length];
                for (int neuron = 0; neuron < layers[layer].neurons.Length; neuron++) {

                    l[layer - 1][neuron] = layers[layer].neurons[neuron].bias;
                }
            }
            return l;
        }

        public double[] GetOutputs () {
            Layer outputLayer = layers[layers.Length - 1];
            double[] outputs = new double[outputLayer.neurons.Length];
            for (int i = 0; i < outputLayer.neurons.Length; i++) {
                outputs[i] = outputLayer.neurons[i].GetValue ();
            }
            return outputs;
        }

        public double[] QuickPropagate (double[] inputs) {
            SetInputs (inputs);
            Propagate ();
            return GetOutputs ();
        }

        public void Mutate (double mutationRate = 0.01) {
            for (int i = 1; i < layers.Length; i++) {
                layers[i].Mutate (mutationRate);
            }
        }

    }

    public class Layer {
        public Neuron[] neurons;

        public Layer (int numOfNeurons) {
            neurons = new Neuron[numOfNeurons];
            for (int i = 0; i < numOfNeurons; i++) {
                neurons[i] = new Neuron ();
            }
        }

        public Layer (int numOfNeurons, int weightNum) {

            neurons = new Neuron[numOfNeurons];
            for (int i = 0; i < numOfNeurons; i++) {
                neurons[i] = new Neuron ();
                neurons[i].SetWeightNum (weightNum);
            }
            InitializeLayer ();

        }

        private void InitializeLayer () {
            for (int i = 0; i < neurons.Length; i++) {
                neurons[i].InitializeValues ();
            }
        }

        public void SetWeights (double[][] w) {
            for (int neuron = 0; neuron < neurons.Length; neuron++) {
                neurons[neuron].SetWeights (w[neuron]);
            }
        }

        public void SetBiases (double[] b) {
            for (int neuron = 0; neuron < neurons.Length; neuron++) {
                neurons[neuron].bias = b[neuron];
            }
        }

        public void PrintNeuronValues () {
            for (int i = 0; i < neurons.Length; i++) {
                Console.Write ($"Neuron {i}: ");
                neurons[i].PrintNeuronValues ();
            }
        }

        public void Mutate (double mutationRate) {
            foreach (Neuron n in neurons) {
                n.Mutate (mutationRate);
            }
        }

    }

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