using System;

namespace NeuralNets{
    class Network{
        private Layer[] layers;
        public Network(int[] dimensions){
            layers = new Layer[dimensions.Length];
            layers[0] = new Layer(dimensions[0], isInput: true);
            for(int i = 1; i < layers.Length; i++){
                layers[i] = new Layer(dimensions[i], isInput: false, weightNum: layers[i-1].Neurons.Length);
            }
        }

        public void Propagate(){
            for(int i = 0; i < layers.Length; i++){
                double[] prevLayerValues = new double[layers[i-1].Neurons.Length];
                Array.ForEach(NetMath.Range(layers[i-1].Neurons.Length), j => prevLayerValues[j] = layers[i-1].Neurons[j].Val);

                for(int j = 0; j < layers[i].Neurons.Length; j++){
                    double dotSum = NetMath.DotProduct(prevLayerValues, layers[i].Neurons[j].Weights);
                    double activated = NetMath.Sigmoid(dotSum + layers[i].Neurons[j].Bias);
                    layers[i].Neurons[j].Val = activated;
                }
                
                
            }
        }

        public void PrintNeuronValues(){
            Array.ForEach(layers, layer => layer.PrintNeuronValues());
        }

        
    }
}