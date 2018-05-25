using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InferenceEngine
{
    public abstract class Agent
    {
        private string _tell;
        private string _ask;
        private List<string> _agenda;

        private List<string> _facts;
        private List<string> _clauses;
        private List<int> _count;
        private List<string> _entailed;

        public Agent(string a, string t)
        {
            // initialise variables
            _agenda = new List<string>();
            _clauses = new List<string>();
            _entailed = new List<string>();
            _facts = new List<string>();
            _count = new List<int>();
            _tell = Program.StripWhiteSpace(t);
            _ask = a;
        }

        // method which calls the main fcentails( method and returns output back to iengine
        public abstract string Execute();

        // method which checks if p appears in the premise of a given clause	
        // input : clause, p
        // output : true if p is in the premise of clause
        // Position is used to see whether the character is either in the premise
        // or in the implication of the clause
        public bool ClauseContains(string clause, string p, int position) 
        {
            string[] separatingChars = { "=>" };
            string premise = clause.Split(separatingChars, System.StringSplitOptions.RemoveEmptyEntries)[position];
            string[] conjuncts = premise.Split('&');
            // check if p is in the premise
            if (conjuncts.Length == 1)
                return premise.Equals(p);
            else
                return conjuncts.ToList().Contains(p);
        }

        // methid that returns the conjuncts contained in a clause
        public List<String> GetPremises(String clause)
        {
            // get the premise
            String premise = clause.Split("=>")[0];
            List<String> temp = new List<String>();
            String[] conjuncts = premise.Split("&");
            // for each conjunct
            for (int i = 0; i < conjuncts.Length; i++)
            {
                if (!Agenda.Contains(conjuncts[i]))
                    temp.Add(conjuncts[i]);
            }
            return temp;
        }

        public string Tell
        {
            get { return _tell; }
            set { _tell = value; }
        }
        public string Ask
        {
            get { return _ask; }
            set { _ask = value; }
        }
        public List<string> Agenda
        {
            get { return _agenda; }
            set { _agenda = value; }
        }
        public List<string> Facts
        {
            get { return _facts; }
            set { _facts = value; }
        }
        public List<string> Clauses
        {
            get { return _clauses; }
            set { _clauses = value; }
        }
        public List<int> Count
        {
            get { return _count; }
            set { _count = value; }
        }
        public List<string> Entailed
        {
            get { return _entailed; }
            set { _entailed = value; }
        }
    }
}
