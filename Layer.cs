using System;

namespace NeuralNets{
    class Layer{
        Neuron[] neurons;
        public Layer(int numOfNeurons, bool isInput = false){
            neurons = new Neuron[numOfNeurons];
            for(int i = 0; i < numOfNeurons; i++){
                neurons[i] = new Neuron();
            }
        }
    }
}