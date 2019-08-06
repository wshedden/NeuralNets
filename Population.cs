using System;
using System.Collections.Generic;

namespace Neural_Networks {
    public class Population {
        int popSize;
        private Network[] networks;

        public Population (int popSize, int[] dimensions) {
            this.popSize = popSize;
            networks = new Network[popSize];
            for (int i = 0; i < popSize; i++) {
                networks[i] = new Network (dimensions);
            }
        }

        private void MutateHalf () {
            const double mutationRate = 0.5;
            for (int network = popSize / 2; network < popSize; network++) {
                networks[network].Mutate (mutationRate);
            }
        }

        public List<Network> SortList(List<Network> l){
            //Merge sort
            if(l.Count == 1) return l;
            List<Network> left = SortList(l.GetRange(0, l.Count/2));
            List<Network> right = SortList(l.GetRange(l.Count/2, l.Count-(l.Count/2)));
            return Merge(left, right);
        }

        public List<Network> Merge(List<Network> a, List<Network> b){
            List<Network> c = new List<Network>();
            int i = 0;
            int k = 0;
            while(i < a.Count && k < b.Count){
                if(a[i].error < b[k].error){
                    c.Add(a[i]);
                    i++;
                } else {
                    c.Add(b[k]);
                    k++;
                }

            }
            if(i == a.Count) c.AddRange (b.GetRange (k, b.Count - k));
            if(k == b.Count) c.AddRange (a.GetRange (i, a.Count - i));
            return c;
        }

        public void SortNetworks() {
            //Initiates merge sort
            List<Network> l = new List<Network>(networks);
            networks = SortList(l).ToArray();
        }

        private double GetSquaredDifference (double[] outputs, double[] expected) {
            double total = 0;
            foreach (double i in outputs) {
                foreach (double e in expected) {
                    total += Math.Pow (i - e, 2);
                }
            }
            return total;
        }

        public void PrintErrors () {
            SortNetworks ();
            for (int network = 0; network < popSize; network++) {
                Console.WriteLine (networks[network].error);
            }
        }

        public void PrintBestError () {
            SortNetworks ();
            Console.WriteLine ($"Best error: {networks[0].error}");
        }

        private void Clone () {
            for (int network = 0; network < popSize / 2; network++) {
                Network n1 = networks[network];
                Network n2 = networks[network * 2];
                n2 = n1;

                n2.SetWeights (n1.GetWeights ());
                n2.SetBiases (n1.GetBiases ());
                networks[network] = n1;
                networks[network * 2] = n2;
            }
        }

        public Network GetBestNetwork () {
            SortNetworks ();
            return networks[0];
        }

        public void PrintWeights (int n) {
            SortNetworks ();
            networks[n].PrintWeights ();
        }
        public void PrintNetworkValues (int n) {
            SortNetworks ();
            networks[n].PrintNeuronValues ();
        }

        public double[] QuickPropagate (double[] inputs) {
            SortNetworks ();
            return networks[0].QuickPropagate (inputs);
        }

        public void Train (double[][] inputs, double[][] expected, int epochs = 1000, int interval = 64) {
            for (int epoch = 0; epoch < epochs; epoch += interval) {
                if (epoch % interval == 0)
                    Console.Write ($"\rEpoch {epoch}/{epochs} = {100*epoch/epochs}%     ");

                if (popSize % 2 != 0) {
                    Console.WriteLine ("ERROR: Population size must be even for the genetic algorithm to work.");
                    return;
                }

                for (int network = 0; network < networks.Length; network++) {
                    double[] errors = new double[expected.Length];
                    for (int test = 0; test < expected.Length; test++) {
                        //Set network errors
                        double[] output = networks[network].QuickPropagate (inputs[test]);
                        double error = GetSquaredDifference (output, expected[test]);
                        errors[test] = error;

                    }

                    networks[network].error = Program.DoubleSum (errors);

                }
                SortNetworks ();
                Clone ();
                MutateHalf ();

            }

        }
    }

}