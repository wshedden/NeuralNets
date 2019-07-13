using System;

namespace NeuralNets{
    class Layer{
        private Neuron[] neurons;
        bool isInput;
        public Layer(int numOfNeurons, bool isInput = false, int weightNum = 0){
            neurons = new Neuron[numOfNeurons];
            this.isInput = isInput;
            for(int i = 0; i < numOfNeurons; i++){
                neurons[i] = new Neuron(weightNum);
            }
            if(!isInput) Array.ForEach(neurons, neuron => neuron.RandomizeFields());
        }

        public void PrintNeuronValues(){
            if(!isInput) Array.ForEach(neurons, neuron => neuron.PrintNeuronValues());
        }

        public Neuron[] Neurons{
            get{
                return neurons;
            }
            set{
                if(value.Length == neurons.Length) neurons = value;
            }
        }
    }
}