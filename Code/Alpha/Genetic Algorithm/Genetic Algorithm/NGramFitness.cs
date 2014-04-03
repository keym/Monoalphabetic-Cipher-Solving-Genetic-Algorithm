﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetic_Algorithm
{
    class NGramFitness : IFitness
    {
        private IChromosome[] population;
        private ICipher cipher;
        private List<ngram> Search;

        public IChromosome[] Population
        {
            get
            {
                return population;
            }
            set
            {
                population = value;
            }
        }

        public ICipher Cipher
        {
            get
            {
                return cipher;
            }
            set
            {
                cipher = value;
            }
        }

        public NGramFitness(IChromosome[] population, ICipher cipher)
        {
            Population = population;
            Cipher = cipher;
        }

        public IChromosome[] calcFitness()
        {
            readfiles();
            foreach (IChromosome c in Population)
            {
                string ciphertext = generateCipherText(c, Cipher);
                c.Fitness = generateFitness(ciphertext);
            }

            return Population;
        }

        public string generateCipherText(IChromosome c, ICipher cipher)
        {
            char[] ciphertext = new char[340];
            int i = 0;
            foreach (char allele in c.Alleles)
            {
                
                foreach (int letter in Cipher.Cipher[i])
                {
                    ciphertext[letter - 1] = allele;
                    
                }
                i++;
            }
            return new string(ciphertext);
        }

        public double generateFitness(string ciphertext)
        {
            string copy = ciphertext;
            double fitness = 0;

            foreach (ngram s in Search)
            {
                double instances = (ciphertext.Length - copy.Replace(s.Search, "").Length) / s.Search.Length;

                fitness = fitness + (instances * s.Value);
            }

            return fitness;
        }

        public void readfiles()
        {
            string line;
            int count = 0;
            double max2l = 1;
            double max3l = 1;
            Search =  new List<ngram>();

            System.IO.StreamReader file = new System.IO.StreamReader(@"count_2l.txt");
            while ((line = file.ReadLine()) != null)
            {

                string[] values = line.Split(',');

                if (count == 0)
                {
                    max2l = Convert.ToDouble(values[1]);
                }

                Search.Add(new ngram(values[0], Convert.ToDouble(values[1]) / max2l));
                count++;
            }
            file.Close();

            count = 0;
            file = new System.IO.StreamReader(@"count_3l.txt");
            while ((line = file.ReadLine()) != null)
            {
                string[] values = line.Split(',');
                if (count == 0)
                {
                    max3l = Convert.ToDouble(values[1]);
                }
                Search.Add(new ngram(values[0], Convert.ToDouble(values[1]) / max2l));
                count++;
            }
            file.Close();
        }
    }

    class ngram
    {
        private string search;

        public string Search
        {
            get { return search; }
            set { search = value; }
        }
        private double value;

        public double Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public ngram(string search, double value)
        {
            Search = search;
            Value = value;
        }
    }
}