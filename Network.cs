using System;

namespace GeneticAlgo {
    class Network {
        Layer[] layers;
        private double error;
        public double Error{
            get {return error;}
            set{error = value;}
        }

        
        public Network(int[] dimensions) {
            layers = new Layer[dimensions.Length];
            CreateLayers(dimensions);
        }

        private void CreateLayers(int[] d) {
            layers[0] = new Layer(Type.INPUT, d[0], 0);
            layers[d.Length - 1] = new Layer(Type.OUTPUT, d[d.Length - 1], d[d.Length - 2]);
            for (int i = 1; i < d.Length - 1; i++) {
                layers[i] = new Layer(Type.HIDDEN, d[i], d[i - 1]);
            }
        }

        public void Propagate(double[] inputs) {
            layers[0].SetValues(inputs);
            for (int i = 1; i < layers.Length; i++) {
                Neuron[] neurons = layers[i].Neurons;
                Neuron[] prevNeurons = layers[i - 1].Neurons;
                double[] biases = new double[neurons.Length];
                double[] newValues = new double[neurons.Length];
                double[] values = new double[prevNeurons.Length];
                double[][] weights = new double[neurons.Length][];
                for (int j = 0; j < values.Length; j++) values[j] = prevNeurons[j].value;
                for (int j = 0; j < weights.Length; j++) weights[j] = neurons[j].weights;
                for (int j = 0; j < biases.Length; j++) biases[j] = neurons[j].bias;
                for (int k = 0; k < neurons.Length; k++) {
                    double prod = Program.DotProd(values, weights[k]);
                    double activated = Program.Sigmoid(prod + biases[k]);
                    newValues[k] = activated;
                }
                layers[i].SetValues(newValues);
            }
        }

        public double SqError(double[] expected) {
            double[] outputs = GetOutputs();
            double error = 0;
            for (int i = 0; i < outputs.Length; i++) {
                error += Math.Pow(expected[i] - outputs[i], 2);
            }
            this.error = error;
            return error;
        }

        public double[] GetOutputs() {
            Neuron[] neurons = layers[layers.Length - 1].Neurons;
            double[] outputs = new double[neurons.Length];
            for (int i = 0; i < neurons.Length; i++) outputs[i] = neurons[i].value;
            return outputs;
        }

        private void Reset(){
            for(int i = 0; i < layers.Length; i++){
                layers[i].SetValues(new double[layers[i].Neurons.Length]);
            }
            Error = 0;
        }

        public void Mutate (double rate = 0.5){
            for(int i = 0; i < layers.Length; i++){
                for(int k = 0; k < layers[i].Neurons.Length; k++){
                    layers[i].Neurons[k].Mutate(rate);
                }
            }
        }


        public double[] Forward (double[] inputs, double[] expected){
            Propagate(inputs);
            double[] outputs = GetOutputs();
            SqError(expected);
            return outputs;
        }

        public void PrintValues() {
            for (int i = 0; i < layers.Length; i++) {
                Console.WriteLine($"Error = {error}");
                Console.WriteLine($"Layers [{i}]:");
                layers[i].PrintValues();
            }
        }

    }

}