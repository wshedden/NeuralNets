using System;

namespace NeuralNets{
    class Network{
        private Layer[] layers;
        public double error;
        public Network(int[] dimensions){
            layers = new Layer[dimensions.Length];
            layers[0] = new Layer(dimensions[0], isInput: true);
            for(int i = 1; i < layers.Length; i++){
                layers[i] = new Layer(dimensions[i], isInput: false, weightNum: layers[i-1].Neurons.Length);
            }
        }

        public void Propagate(){
            for(int i = 1; i < layers.Length; i++){
                double[] prevLayerValues = new double[layers[i-1].Neurons.Length];
                Array.ForEach(NetMath.Range(layers[i-1].Neurons.Length), j => prevLayerValues[j] = layers[i-1].Neurons[j].Val);
                for(int j = 0; j < layers[i].Neurons.Length; j++){
                    double dotSum = NetMath.DotProduct(prevLayerValues, layers[i].Neurons[j].Weights);
                    double activated = NetMath.Sigmoid(dotSum + layers[i].Neurons[j].Bias);
                    layers[i].Neurons[j].Val = activated;
                }
            }
        }

        public void Train(Dataset dataset, double learningRate = 0.1){
            double averageError = GetDatasetError(dataset);

        }

        private double GetDatasetError(Dataset dataset){
            //Calculate average error for entire dataset
            double[] errors = new double[dataset.numOfDataPoints];

            for(int i = 0; i < dataset.numOfDataPoints; i++){

                double[] inputs = dataset.Inputs[i];
                double[] expected = dataset.Expected[i];
                double[] outputs = OutVal(inputs);

                double error = CalculateError(outputs, expected);
                errors[i] = error;
                
            }
            double averageError = NetMath.Sum(errors)/dataset.numOfDataPoints;
            return averageError;
        }

        public void BackPropagate(){

        }

        public void PrintNeuronValues(){
            Array.ForEach(layers, layer => layer.PrintNeuronValues());
        }

        private void SetInputs(double[] inputs){
            Array.ForEach(NetMath.Range(inputs.Length), i => layers[0].Neurons[i].Val = inputs[i]);
        }

        private double[] GetOutputs(){
            Layer lastLayer = layers[layers.Length-1];
            double[] outputs = new double[lastLayer.Neurons.Length];
            Array.ForEach(NetMath.Range(lastLayer.Neurons.Length), i => outputs[i] = lastLayer.Neurons[i].Val);
            return outputs;
        }

        public double[] OutVal (double[] inputs){
            SetInputs(inputs);
            Propagate();
            return GetOutputs();
        }

        public void Reset(){
            double[] emptyInputList = new double[layers[0].Neurons.Length];
            for(int i = 0; i < emptyInputList.Length; i++){
                emptyInputList[i] = 0;
            }
            SetInputs(emptyInputList);
        }

        private double CalculateError(double[] outputs, double[] expected){ //For one line of data
            //Must Propagate() first 
            double[] squaredDifferenceArray = new double[outputs.Length];
            for(int i = 0; i < outputs.Length; i++){
                squaredDifferenceArray[i] = Math.Pow(expected[i] - outputs[i], 2);
            }
            double avgSquaredDifference = NetMath.Sum(squaredDifferenceArray)/outputs.Length;
            return avgSquaredDifference;
        }

        
    }
}