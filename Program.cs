
using System;

namespace Neural_Networks { //---------------------------------- THE GENETIC ALGORITHM DOESN'T WORK PROPERLY - TURN BACK HERE --------------------------------------\\
    class Program {
        static void Main(string[] args) {

            int[] dimensions = new int[] { 2, 4, 2 };

            string dir = Environment.CurrentDirectory;
            string dataFileName = "data.txt";
            string fullDir = dir + "\\" + dataFileName;

            string[] lines = System.IO.File.ReadAllLines(fullDir);


            double[][] inputs = new double[lines.Length / 2][];
            double[][] expected = new double[lines.Length / 2][];
            foreach (int k in new int[] { 0, 1 }) {
                for (int i = k; i < lines.Length; i += 2) {
                    string[] lineArr = lines[i].Split(' ');
                    double[] nums = new double[lineArr.Length];
                    for (int j = 0; j < nums.Length; j++) {
                        bool successful = Double.TryParse(lineArr[j], out nums[j]);
                        if (!successful) Console.WriteLine("$[DATA CONVERSION ERROR]; i={i}, j={j}");
                    }
                    if (k == 0)
                        inputs[i / 2] = nums;
                    else
                        expected[i / 2] = nums;

                }
            }


            Population population = new Population(10000, dimensions);

            population.PrintWeights(0);

            double[] testData = new double[] { 5 };

            double[] outputs = population.QuickPropagate(testData);
            string numInput;

        
            population.Train(inputs, expected, epochs: 10000000, interval: 163840);
            Console.WriteLine();

            do {
                double[] nums = new double[2 /*input layer size, ICBA making it a variable*/];
                Console.Write("> ");
                numInput = Console.ReadLine();
                string[] numList = numInput.Split(' ');

                for (int i = 0; i < nums.Length; i++) {
                    try {
                        Double.TryParse(numList[i], out nums[i]);
                    } catch (Exception _) {
                        Console.WriteLine("[ERROR: Wrong number of arguments]");
                    }
                }

                outputs = population.QuickPropagate(nums);
                for(int i = 0; i < outputs.Length; i++) {
                    Console.Write(outputs[i] + " ");
                }
                Console.WriteLine();

            } while (numInput != "q");




            Console.ReadKey();


        }

        public static double DoubleSum(double[] arr) {
            double total = 0;
            for (int i = 0; i < arr.Length; i++) {
                total += arr[i];
            }
            return total;
        }

        public static double Sigmoid(double n) {
            return 1d / (1d + (double)Math.Exp(-n));
        }

        public static void PrintArray(double[] arr) {
            foreach (double i in arr) {
                Console.WriteLine(i);
            }
        }

        public static void PrintArray(int[] arr) {
            foreach (int i in arr) {
                Console.WriteLine(i);
            }
        }



    }


    public class Network {
        private Layer[] layers;
        public double error;

        public Network(int[] dimensions) {
            int numOfLayers = dimensions.Length;
            layers = new Layer[numOfLayers];
            layers[0] = new Layer(dimensions[0]);
            for (int i = 1; i < numOfLayers; i++) {
                layers[i] = new Layer(dimensions[i], layers[i - 1].neurons.Length);
            }
        }

        public void PrintNeuronValues() {
            for (int i = 0; i < layers.Length; i++) {
                Console.WriteLine($"Layer {i}: ");
                layers[i].PrintNeuronValues();
                Console.WriteLine();
            }
        }

        public void SetWeights(double[][][] w) {
            for (int layer = 1; layer < layers.Length; layer++) {
                layers[layer].SetWeights(w[layer - 1]);
            }
        }
        public void SetBiases(double[][] b) {
            for (int layer = 1; layer < layers.Length; layer++) {
                layers[layer].SetBiases(b[layer - 1]);
            }
        }


        public void PrintWeights() {
            double[][][] weights = GetWeights();
            for (int i = 0; i < weights.Length; i++) {
                for (int j = 0; j < weights[i].Length; j++) {
                    for (int k = 0; k < weights[i][j].Length; k++) {
                        Console.Write(weights[i][j][k]);
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("\n\n");
            }
        }

        public void Propagate() {
            for (int i = 1; i < layers.Length; i++) {
                Layer prevLayer = layers[i - 1];
                for (int j = 0; j < layers[i].neurons.Length; j++) {
                    double[] multiplied = new double[prevLayer.neurons.Length];
                    for (int k = 0; k < prevLayer.neurons.Length; k++) {
                        multiplied[k] = prevLayer.neurons[k].GetValue() * layers[i].neurons[j].weights[k];
                    }
                    double summed = Program.DoubleSum(multiplied) + layers[i].neurons[j].bias;
                    double activated = Program.Sigmoid(summed);
                    layers[i].neurons[j].SetValue(activated);
                }
            }
        }

        public void SetInputs(double[] inputs) {
            for (int i = 0; i < inputs.Length; i++) {
                layers[0].neurons[i].SetValue(inputs[i]);
            }

        }
        public double[][][] GetWeights() {
            double[][][] l = new double[layers.Length - 1][][];

            for (int layer = 1; layer < layers.Length; layer++) { //Starts at 1 to avoid input layer, which has no weights.
                l[layer - 1] = new double[layers[layer].neurons.Length][];
                for (int neuron = 0; neuron < layers[layer].neurons.Length; neuron++) {


                    l[layer - 1][neuron] = layers[layer].neurons[neuron].weights;
                }
            }
            return l;
        }

        public double[][] GetBiases() {
            double[][] l = new double[layers.Length - 1][];

            for (int layer = 1; layer < layers.Length; layer++) { //Starts at 1 to avoid input layer, which has no biases.
                l[layer - 1] = new double[layers[layer].neurons.Length];
                for (int neuron = 0; neuron < layers[layer].neurons.Length; neuron++) {

                    l[layer - 1][neuron] = layers[layer].neurons[neuron].bias;
                }
            }
            return l;
        }

        public double[] GetOutputs() {
            Layer outputLayer = layers[layers.Length - 1];
            double[] outputs = new double[outputLayer.neurons.Length];
            for (int i = 0; i < outputLayer.neurons.Length; i++) {
                outputs[i] = outputLayer.neurons[i].GetValue();
            }
            return outputs;
        }

        public double[] QuickPropagate(double[] inputs) {
            SetInputs(inputs);
            Propagate();
            return GetOutputs();
        }

        public void Mutate(double mutationRate = 0.01) {
            for (int i = 1; i < layers.Length; i++) {
                layers[i].Mutate(mutationRate);
            }
        }


    }




    public class Layer {
        public Neuron[] neurons;

        public Layer(int numOfNeurons) {
            neurons = new Neuron[numOfNeurons];
            for (int i = 0; i < numOfNeurons; i++) {
                neurons[i] = new Neuron();
            }
        }

        public Layer(int numOfNeurons, int weightNum) {

            neurons = new Neuron[numOfNeurons];
            for (int i = 0; i < numOfNeurons; i++) {
                neurons[i] = new Neuron();
                neurons[i].SetWeightNum(weightNum);
            }
            InitializeLayer();

        }


        private void InitializeLayer() {
            for (int i = 0; i < neurons.Length; i++) {
                neurons[i].InitializeValues();
            }
        }

        public void SetWeights(double[][] w) {
            for (int neuron = 0; neuron < neurons.Length; neuron++) {
                neurons[neuron].SetWeights(w[neuron]);
            }
        }

        public void SetBiases(double[] b) {
            for (int neuron = 0; neuron < neurons.Length; neuron++) {
                neurons[neuron].bias = b[neuron];
            }
        }



        public void PrintNeuronValues() {
            for (int i = 0; i < neurons.Length; i++) {
                Console.Write($"Neuron {i}: ");
                neurons[i].PrintNeuronValues();
            }
        }

        public void Mutate(double mutationRate) {
            foreach (Neuron n in neurons) {
                n.Mutate(mutationRate);
            }
        }


    }

    public class Neuron {
        public double[] weights;
        public double bias;
        public int weightNum;
        private double value;

        public Neuron() { }

        public void SetWeightNum(int weightNum) { //MUST SetWeightNum before calling InitializeValues
            this.weightNum = weightNum;
            weights = new double[weightNum];
        }

        public double GetValue() {
            return value;
        }

        public void SetWeights(double[] w) {
            weights = new double[w.Length];
            for (int weight = 0; weight < weights.Length; weight++) {
                weights[weight] = w[weight];
            }
        }

        public void SetValue(double v) {
            value = v;
        }

        public void InitializeValues() {
            Random random = new Random();
            bias = (random.NextDouble() * 2) - 1;
            for (int i = 0; i < weightNum; i++) {
                weights[i] = (random.NextDouble() * 2)-1;
            }
        }


        public void PrintNeuronValues() {
            Console.WriteLine($"V={value}, B={bias}, W={weights != null && weights.Length != 0}");
        }

        public void Mutate(double mutationRate) {
            Random rand = new Random();

            if (rand.NextDouble() < mutationRate)
                bias = (rand.NextDouble() * 2) - 1;
            for (int i = 0; i < weights.Length; i++) {
                if (rand.Next() < mutationRate)
                    weights[i] = (rand.NextDouble() * 2) - 1;
            }
        }
    }

    public class Population {
        int popSize;
        private Network[] networks;
        double[] errors;

        public Population(int popSize, int[] dimensions) {
            this.popSize = popSize;
            networks = new Network[popSize];
            for (int i = 0; i < popSize; i++) {
                networks[i] = new Network(dimensions);
            }
        }

        private void MutateHalf() {
            const double mutationRate = 0.5;
            for (int network = popSize / 2; network < popSize; network++) {
                networks[network].Mutate(mutationRate);
            }
        }




        private void SortNetworks() {
            //Bubble sort based on network error
            bool sorted = false;
            while (!sorted) {
                sorted = true;
                for (int i = 0; i < popSize - 1; i++) {

                    if (networks[i].error > networks[i + 1].error) {

                        Network temp = networks[i];
                        networks[i] = networks[i + 1];
                        networks[i + 1] = temp;
                        sorted = false;
                    }
                }
            }
        }



        private double GetSquaredDifference(double[] outputs, double[] expected) {
            double total = 0;
            foreach (double i in outputs) {
                foreach (double e in expected) {
                    total += Math.Pow(i - e, 2);
                }
            }
            return total;
        }

        public void PrintErrors() {
            SortNetworks();
            for (int network = 0; network < popSize; network++) {
                Console.WriteLine(networks[network].error);
            }
        }

        public void PrintBestError() {
            SortNetworks();
            Console.WriteLine($"Best error: {networks[0].error}");
        }

        private void Clone() {
            for (int network = 0; network < popSize / 2; network++) {
                Network n1 = networks[network];
                Network n2 = networks[network * 2];
                n2 = n1;

                n2.SetWeights(n1.GetWeights());
                n2.SetBiases(n1.GetBiases());
                networks[network] = n1;
                networks[network * 2] = n2;
            }
        }

        public Network GetBestNetwork() {
            SortNetworks();
            return networks[0];
        }


        public void PrintWeights(int n) {
            SortNetworks();
            networks[n].PrintWeights();
        }
        public void PrintNetworkValues(int n) {
            SortNetworks();
            networks[n].PrintNeuronValues();
        }

        public double[] QuickPropagate(double[] inputs) {
            SortNetworks();
            return networks[0].QuickPropagate(inputs);
        }

        public void Train(double[][] inputs, double[][] expected, int epochs = 1000, int interval = 64) {
            for (int epoch = 0; epoch < epochs; epoch += interval) {
                if (epoch % interval == 0)
                    Console.Write($"\rEpoch {epoch}/{epochs} = {100*epoch/epochs}%     ");

                if (popSize % 2 != 0) {
                    Console.WriteLine("ERROR: Population size must be even for the genetic algorithm to work.");
                    return;
                }

                for (int network = 0; network < networks.Length; network++) {
                    double[] errors = new double[expected.Length];
                    for (int test = 0; test < expected.Length; test++) {
                        //Set network errors
                        double[] output = networks[network].QuickPropagate(inputs[test]);
                        double error = GetSquaredDifference(output, expected[test]);
                        errors[test] = error;

                    }

                    networks[network].error = Program.DoubleSum(errors);

                }
                SortNetworks();
                Clone();
                MutateHalf();



            }

        }
    }
}