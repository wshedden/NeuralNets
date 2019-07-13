using System;

namespace NeuralNets{
    class Network{
        private Layer[] layers;
        public Network(int[] dimensions){
            layers = new Layer[dimensions.Length];
            layers[0] = new Layer(dimensions[0], isInput: true);
            for(int i = 1; i < layers.Length; i++){
                layers[i] = new Layer(dimensions[i], isInput: false);
            }
        }
    }
}