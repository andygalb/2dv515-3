using System;
using System.Collections.Generic;

namespace Cluster
{
    public class Cluster
    {
        public Cluster left; //null if leaf node
        public Cluster right; //null if leaf node
        public Cluster parent;
        public Blog blog;
        public double distance; //0 if leaf node
    }

    public class Blog
    {
        public string title;
        //public Dictionary<String, Double> words;
        public List<Word> words = new List<Word>();
    }

    public class Centroid
    {
        public List<Word> words = new List<Word>();
        public List<Blog> assignments = new List<Blog>();
    }

    public class Word
    {
        public string word;
        public double count;
    }
}
