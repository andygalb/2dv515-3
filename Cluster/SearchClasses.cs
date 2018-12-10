using System;
using System.Collections.Generic;
using System.Text;

namespace SearchEngine
{
    public class Score
    {

        public Score(Page page, Double score)
        {
            this.p = page;
            this.score = score;
        }

        public Score(Page page, Double contentScore, Double locationScore, Double pageRankScore, Double score)
        {
            this.p = page;
            this.score = score;
            this.contentScore = contentScore;
            this.locationScore = locationScore;
            this.pageRankScore = pageRankScore;
        }

        public Page p;
        public Double score;
        public Double contentScore;
        public Double locationScore;
        public Double pageRankScore;
    }

    public class Scores
    {
        public List<double> content = new List<double>();
        public List<double> location = new List<double>();
        public List<double> pageRank = new List<double>();
    }

    public class PageDB
    {
        public Dictionary<String, int> wordToId = new Dictionary<string, int>();
        public List<Page> pages = new List<Page>();
    }

    public class Page
    {
        public String url;
        public double pageRank = 1.0;
        public double contentScore = 0;
        public double locationScore = 0;
        public List<int> words;
        public List<String> links;

        public bool HasLinkTo(Page p)
        {
            var urlArray = p.url.Split("\\");
            var linkTitle = "/wiki/" + urlArray[urlArray.Length - 1];
            if (links.Contains(linkTitle)) return true;
            else return false;
        }
    }
}
