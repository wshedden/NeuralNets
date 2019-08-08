using System;
using System.Collections.Generic;

namespace Neural_Networks {
    public class Genome{
        List<double> strand;
        private double[][][] weights;
        private double[][] biases;
        public Genome(double[][][] weights, double[][] biases){
            this.weights = weights;
            this.biases = biases;
            
            foreach(double[][] i in weights){
                foreach(double[] j in i){
                    foreach(double k in j){
                        strand.Add(k);
                    }
                }
            }
            foreach(double[] i in biases){
                foreach(double j in i){
                    strand.Add(j);
                }
            }
            
            
        }

        private void Synthesise(){
            List<double> strandTemp = strand;
            for(int i = 0; i < weights.Length; i++){
                for(int j = 0; j < weights[i].Length; j++){
                    for(int k = 0; k < weights[i][j].Length; k++){
                        weights[i][j][k] = strand[0];
                        strand.RemoveAt(0);
                    }
                }
            }

            for(int i = 0; i < biases.Length; i++){
                for(int j = 0; j < biases[i].Length; j++){
                    biases[i][j] = strand[0];
                    strand.RemoveAt(0);
                }
            }
            strand = strandTemp;
        }

        public void Mutate(double mutationRate=0.1, int mutationSeed=1){
            Random random = new Random(Seed:mutationSeed);
            for(int i = 0; i < strand.Count; i++){
                if(random.NextDouble() < mutationRate){
                    
                }
            }
            Synthesise();

        }

        public double[][][] Weights{
            get{
                return weights;
            }
        }
        public double[][] Biases{
            get{
                return biases;
            }
        }




    }


}