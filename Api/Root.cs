using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Result
    {
        public string definition { get; set; }
        public string partOfSpeech { get; set; }
        public List<string> synonyms { get; set; }
        public List<string> typeOf { get; set; }
        public List<string> hasTypes { get; set; }
        public List<string> derivation { get; set; }
        public List<string> examples { get; set; }
    }

    public class Syllables
    {
        public int count { get; set; }
        public List<string> list { get; set; }
    }

    public class Pronunciation
    {
        public string all { get; set; }
    }

    public class Root
    {
        public string word { get; set; }
        public List<Result> results { get; set; }
        public Syllables syllables { get; set; }
        public Pronunciation pronunciation { get; set; }
        public double frequency { get; set; }
    }
}
