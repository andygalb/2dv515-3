using System;
using System.Collections.Generic;

namespace Cluster
{
    public class Hierarchical
    {
        List<Cluster> clusters = new List<Cluster>();
        List<Blog> blogs = new List<Blog>();

        public Hierarchical()
        {
            CreateInitialClusters();
        }

        public void HierarchicalCluster(List<Cluster> clusters, List<Blog> blogs){

            this.clusters = clusters;
            this.blogs = blogs;

            int count = 0;

            while (clusters.Count > 1)
            {
                Console.WriteLine($"Iteration {count}\t No. Clusters: {clusters.Count}");
                Iterate();
                count++;
            }

        }

       public void CreateInitialClusters()
        {
            foreach (Blog b in blogs)
            {
                Cluster cluster = new Cluster();
                cluster.blog = b;
                clusters.Add(cluster);
            }
        }

        public void Iterate()
        {
            //Find two closest nodes
            double closest = Double.MaxValue;
            Cluster A = new Cluster();
            Cluster B = new Cluster();
            foreach (Cluster cA in clusters)
            {
                foreach (Cluster cB in clusters)
                {
                    double distance = Pearson(cA.blog, cB.blog);
                    if (distance < closest && cA != cB)
                    {
                        //New set of closest nodes found
                        closest = distance;
                        A = cA;
                        B = cB;
                    }
                }
            }

            //Merge the two clusters
            Console.WriteLine($"Merging {A.blog.title} and {B.blog.title}");
            Cluster nC = Merge(A, B, closest);
            //Add new cluster
            clusters.Add(nC);
            //Remove old clusters
            clusters.Remove(A);
            clusters.Remove(B);
        }

        public Cluster Merge(Cluster A, Cluster B, double distance)
        {

            Cluster P = new Cluster();
            //Fill data
            P.left = A;
            A.parent = P;
            P.right = B;
            B.parent = P;
            //Merge blog data by averaging word counts for each word
            Blog nB = new Blog();

            for (int i = 0; i < A.blog.words.Count; i++)
            {
                Word wA = A.blog.words[i];
                Word wB = B.blog.words[i];
                double cnt = (wA.count + wB.count) / 2;
                Word newWord = new Word();
                newWord.word = wA.word;
                newWord.count = cnt;
                nB.words.Add(newWord);
            }

            //Set blog to new cluster
            P.blog = nB;
            //Set distance
            P.distance = distance;
            //Return new cluster
            return P;
        }


        public float Pearson(Blog A, Blog B)
        {
            if (B.words.Count != 706) { throw new Exception("Incorrect number of words"); }
            //Init variables
            double sum1 = 0, sum2 = 0, sum1sq = 0, sum2sq = 0, pSum = 0;

            //Counter for number of matching products
            int n = 0;

            for (int i = 0; i < A.words.Count; i++)
            {
                Word wA = A.words[i];
                Word wB = B.words[i];
                sum1 += wA.count;
                sum2 += wB.count;
                sum1sq += wA.count * wA.count; //rA*rA
                sum2sq += wB.count * wB.count; //rB*rB
                pSum += wA.count * wB.count;
                n += 1;

            }


            //No ratings in common – return 0
            if (n == 0) return 0;

            //Calculate Pearson
            double num = pSum - ((sum1 * sum2) / n);
            float den = (float)Math.Sqrt((sum1sq - (sum1 * sum1 / n)) * (sum2sq - (sum2 * sum2 / n)));
            return (float)num / den;
        }


    }
}
