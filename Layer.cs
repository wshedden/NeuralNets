using System;

namespace GeneticAlgo {
    class Layer {
        private Neuron[] neurons;
        Type type;
        public Layer(Type type, int n, int prevN) {
            neurons = new Neuron[n];
            for (int i = 0; i < n; i++) {
                neurons[i] = new Neuron(prevN);
            }
            this.type = type;
        }
        public void SetValues(double[] val) {
            for (int i = 0; i < neurons.Length; i++) {
                neurons[i].value = val[i];
            }
        }

        public void PrintValues() {
            for (int i = 0; i < neurons.Length; i++) {
                Console.WriteLine($"\tNeurons [{i}]:");
                neurons[i].PrintValues();
            }
        }


        public Neuron[] Neurons {
            get { return neurons; }
        }

    }
    
}