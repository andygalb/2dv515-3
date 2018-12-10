using System;
using System.Collections.Generic;

namespace ClusterServer
{
    public class KMean
    {

        int K = 5;
        int MAX_ITERATIONS = 100;

        List<Blog> blogs = new List<Blog>();

        string[] wordsList;
        double[] min;
        double[] max;

        public KMean(List<Blog> blogs, string[] wordsList, int K, int maxIterations)
        {
            this.blogs = blogs;
            this.wordsList = wordsList;
            this.K = K;
            this.MAX_ITERATIONS = maxIterations;

            ProcessBlogsForMaxOchMin();
        }

       
        public List<Centroid> KMeans()
        {
            //Generate K random centroids
            List<Centroid> centroids = new List<Centroid>();

            //K = no of centroids we want finally.
            for (int c = 0; c < K; c++)
            {
                Centroid cent = new Centroid();

                Random random = new Random();
                for (int i = 0; i < wordsList.Length; i++)
                {
                    Word word = new Word();
                    word.word = wordsList[i];
                    word.count = random.Next((int)min[i], (int)max[i]);
                    //Random word count
                    cent.words.Add(word);
                }
                centroids.Add(cent);
            }

            //Iteration loop
            for (int i = 0; i < MAX_ITERATIONS; i++)
            {
                //Clear assignments for all centroids
                foreach (Centroid c in centroids)
                {
                    c.assignments.Clear();
                }

                //Assign each blog to closest centroid
                foreach (Blog b in blogs)
                {
                    double distance = Double.MaxValue;
                    Centroid best = new Centroid();

                    //Find closest centroid
                    foreach (Centroid c in centroids)
                    {
                        double cDist = PearsonCentroid(c, b);
                        if (cDist < distance)
                        {
                            best = c;
                            distance = cDist;
                        }
                    }

                    //Assign blog to centroid
                    best.assignments.Add(b);

                }


                //Re-calculate center for each centroid
                foreach (Centroid c in centroids)
                {

                    //Find average count for each word
                    for (int k = 0; k < c.words.Count; k++)
                    {
                        double avg = 0;

                        //Iterate over all blogs assigned to this centroid
                        foreach (Blog b in c.assignments)
                        {
                            avg += b.words[k].count;
                        }
                        avg /= c.assignments.Count;

                        //Update word count for the centroid
                        c.words[k].count = avg;
                    }

                }
            }

            //End of iteration loop – all done
            return centroids;
        }

        public List<Centroid> KMeansSelfHalting()
        {
            //Generate K random centroids
            List<Centroid> centroids = new List<Centroid>();
            List<Centroid> oldCentroids = new List<Centroid>();

            //K = no of centroids we want finally.
            for (int c = 0; c < K; c++)
            {
                Centroid cent = new Centroid();

                Random random = new Random();
                for (int i = 0; i < wordsList.Length; i++)
                {
                    Word word = new Word();
                    word.word = wordsList[i];
                    word.count = random.Next((int)min[i], (int)max[i]);
                    //Random word count
                    cent.words.Add(word);
                }
                centroids.Add(cent);
            }

            //Iteration loop
            while (true)
            {
                //Clear assignments for all centroids
                foreach (Centroid c in centroids)
                {
                    c.assignments.Clear();
                }

                //Assign each blog to closest centroid
                foreach (Blog b in blogs)
                {
                    double distance = Double.MaxValue;
                    Centroid best = new Centroid();

                    //Find closest centroid
                    foreach (Centroid c in centroids)
                    {
                        double cDist = PearsonCentroid(c, b);
                        if (cDist < distance)
                        {
                            best = c;
                            distance = cDist;
                        }
                    }

                    //Assign blog to centroid
                    best.assignments.Add(b);

                }


                //Re-calculate center for each centroid
                foreach (Centroid c in centroids)
                {

                    //Find average count for each word
                    for (int k = 0; k < c.words.Count; k++)
                    {
                        double avg = 0;

                        //Iterate over all blogs assigned to this centroid
                        foreach (Blog b in c.assignments)
                        {
                            avg += b.words[k].count;
                        }
                        avg /= c.assignments.Count;

                        //Update word count for the centroid
                        c.words[k].count = avg;
                    }

                }
                if (centroids == oldCentroids) { break; }
                else { oldCentroids = centroids; }
            }

            //End of iteration loop – all done
            return centroids;
        }

        public void ProcessBlogsForMaxOchMin()
        {
            min = new double[blogs[0].words.Count];
            max = new double[blogs[0].words.Count];

            //Start by adding mins and maxs from first blog
            for (int i = 0; i < blogs[0].words.Count; i++)
            {
                min[i] = blogs[0].words[i].count;
                max[i] = blogs[0].words[i].count;
            }

            //then find true mins and maxs
            foreach (Blog b in blogs)
            {
                for (int i = 0; i < b.words.Count; i++)
                {
                    if (b.words[i].count < min[i]) { min[i] = b.words[i].count; }
                    if (b.words[i].count > max[i]) { max[i] = b.words[i].count; }
                }

            }
            return;
        }

        public float PearsonCentroid(Centroid A, Blog B)
        {

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
