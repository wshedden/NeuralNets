using System;
namespace Neural_Networks {
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


    }
}