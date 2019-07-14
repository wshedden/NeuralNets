using System;
using System.Collections.Generic;

namespace NeuralNets {
    public class Dataset {

        public int inputSize;
        public int expectedSize;
        public int numOfDataPoints;
        private List<double[]> inputs;
        private List<double[]> expected;


        public Dataset (int inputSize, int expectedSize) {
            this.inputSize = inputSize;
            this.expectedSize = expectedSize;
            inputs = new List<double[]>();
            expected = new List<double[]>();

        }

        public void AddDataPair (double[] inputList, double[] expectedList) {
            this.inputs.Add (inputList);
            this.expected.Add (expectedList);
        }

        public double[] GetInput (int index) {
            return inputs[index];
        }

        public double[] GetExpected (int index) {
            return expected[index];
        }

        public void RetrieveFromFile (string fileName, string path = ".\\") {
            if (path == ".\\")
                path = Environment.CurrentDirectory;
            string fullPath = path + "\\" + fileName;

            string[] lines = System.IO.File.ReadAllLines (fullPath);

            for (int i = 0; i < lines.Length; i++) {
                string[] sList = lines[i].Split (' ');
                double[] numList = new double[inputSize + expectedSize];
                double[] inputList = new double[inputSize];
                double[] expectedList = new double[expectedSize];
                for (int k = 0; k < sList.Length; k++) {
                    bool successful = Double.TryParse (sList[k], out numList[k]);
                    if (!successful)
                        Console.WriteLine ("[ERROR]: File parse failed");
                }
                
                for (int k = 0; k < inputSize; k++) {
                    inputList[k] = numList[k];
                }
                for (int k = 0; k < expectedSize; k++) {
                    expectedList[k] = numList[k + inputSize];
                }
                AddDataPair(inputList, expectedList);
            }
            numOfDataPoints = inputs.ToArray().Length;

        }

        public void PrintData () {
            double[][] tInputs = inputs.ToArray ();
            double[][] tExpected = expected.ToArray ();

            for (int i = 0; i < tInputs.Length; i++) {
                for (int k = 0; k < tInputs[i].Length; k++) {
                    Console.Write ($"[{i}][{k}]: {tInputs[i][k]} {tExpected[i][k]}\n");
                }
                Console.WriteLine ("\n");
            }
            Console.WriteLine ();
        }

        public List<double[]> Inputs {
            get {
                return inputs;
            }
        }
        public List<double[]> Expected {
            get {
                return expected;
            }
        }

    }
}