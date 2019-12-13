using System;
using System.Collections.Generic;
using System.Threading;

namespace GeneticAlgo {
    class Population {
        Network[] networks;
        int n;
        public Population(int n, int[] dimensions) {
            networks = new Network[n];
            this.n = n;
            for (int i = 0; i < n; i++) {
                networks[i] = new Network(dimensions);
            }
        }
        public void Propagate(double[] inputs) {
            for (int i = 0; i < n; i++) {
                networks[i].Propagate(inputs);
            }
        }

        public void Forward(double[] inputs, double[] expected) {
            for (int i = 0; i < n; i++) {
                networks[i].Forward(inputs, expected);
            }
        }
        public void PrintNetworks(int delay = 100) {
            for (int i = 0; i < n; i++) {
                Console.WriteLine($"Network {i}:\n");
                networks[i].PrintValues();
                Console.WriteLine("\n\n");
                Thread.Sleep(delay);
                Console.Clear();
            }
        }

        public void Mutate(double rate = 0.5) { //Mutates worst half
            for (int i = n/2; i < n; i++) {
                networks[i].Mutate(rate);
            }
        }

        public Network NaturalSelection(double[][] inputs, double[][] expected, int epochs = 10) {
            for (int e = 0; e < epochs; e++) {
                for (int test = 0; test < inputs.Length; test++) {
                    for(int i = 0; i < n; i++){
                        networks[i].Forward(inputs[test], expected[test]);
                        SortNetworks();
                    }
                }
            }
            return networks[0];
        }

        public void SortNetworks() {
            List<Network> l = new List<Network>(networks);
            l = Sort(l);
            networks = l.ToArray();
        }

        private List<Network> Merge(List<Network> a, List<Network> b) {
            int i = 0; int k = 0;
            List<Network> c = new List<Network>();
            while (i < a.Count && k < b.Count) {
                if (a[i].Error < b[k].Error) {
                    c.Add(a[i]);
                    i++;
                } else {
                    c.Add(b[k]);
                    k++;
                }
            }
            c.AddRange(a.GetRange(i, a.Count - i));
            c.AddRange(b.GetRange(k, b.Count - k));
            return c;
        }

        private List<Network> Sort(List<Network> l) {
            if (l.Count == 1) return l;
            List<Network> a = Sort(l.GetRange(0, l.Count / 2));
            List<Network> b = Sort(l.GetRange(l.Count / 2, l.Count - (l.Count / 2)));
            return Merge(a, b);
        }


    }

}