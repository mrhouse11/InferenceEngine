using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace InferenceEngine
{
    public class TruthTable : Agent
    {
        private List<string> _rest = new List<string>();
        private int _numOfTruth;

        public TruthTable(string a, string t) : base(a, t)
        {
            Init(Tell);
            _numOfTruth = 0;
        }

        public void Init(string tell)
        /* This is the agents set up. It sets up:
         * - the list of all symbols
         * - all the clauses for the knowledge base
         * - the facts about the world          */
        {
            string[] sentances = tell.Split(';');
            for (int i = 0; i < sentances.Length-1; i++)
            {
                if (!sentances[i].Contains("=>"))                   // Checks to see if its a fact. eg b;
                {
                    if(!Agenda.Contains(sentances[i]))             // If the symbol is not already in the Symbols list
                        Agenda.Add(sentances[i]);                  // Adds this symbol to the symbols list
                    if(!Facts.Contains(sentances[i]))               // Checks if Facts already contains the symbol
                        Facts.Add(sentances[i]);                    // Adds the symbol to the list of symbols that are true
                }                                               
                else
                {
                    // If it gets here; the current string is a Clause   
                    Clauses.Add(sentances[i]);
                    string[] splitImp = sentances[i].Split("=>");   // Splits clause into pre-implication and post-implecation
                    if (!splitImp[0].Contains("&"))                 // if pre-implecation does not have &
                        for (int j = 0; j < splitImp.Length; j++)   // loop through clause
                        {
                            if (!Agenda.Contains(splitImp[j]))           // if agenda doesnt contain the symbol
                                Agenda.Add(splitImp[j]);               // add symbols
                        }
                    else
                    {                                                       // Pre-imp contains &
                        string[] splitLogic = splitImp[0].Split("&");      // split by logical separators
                        for (int j = 0; j < splitLogic.Length; j++)         // loop through strings split
                        {
                            if(!Agenda.Contains(splitLogic[j]))
                                Agenda.Add(splitLogic[j]);                    // add symbols to symbols base
                            if (!Agenda.Contains(splitImp[1]))
                                Agenda.Add(splitImp[1]);
                        }
                    }
                }
            }
        }

        public override string Execute()
        {
            string output = "";
            if (TTEntails())
            {
                // the returned true so it entails
                output = "Yes: ";
                output += NumOfTruth;
            }
            else
                output = "No";
            return output;
        }

        public bool TTEntails()
        {
            return TTCheckAll(Agenda.ToList(), new Dictionary<string, bool>());
        }

        public bool TTCheckAll(List<string> rest, Dictionary<string, bool> model)        // kb = Clauses; alpha = Ask; Agenda = rest; Model = dictionary<string, bool>
        {
            List<string> nRest = new List<string>(rest);
            if (rest.Count == 0)                      // check whether there are still more symbols to add to the model.
            {
                if (IsKBTrue(model))
                {
                    Console.WriteLine("yes");
                    if (IsAlphaTrue(model))
                    {

                        NumOfTruth++;
                        return true;
                    }
                }
               
                return true;
            }
            else
            {
                string p = nRest[0];               // first item
                nRest.RemoveAt(0);                 // removes that first item from agenda
                
                //Dictionary<string, bool> copyFalse = new Dictionary<string, bool>(model);      // creates a copy of the model
                Dictionary<string, bool> copyTrue = new Dictionary<string, bool>(model);
                model.Add(p, false);                        // Adds a new item to model
                copyTrue.Add(p, true);                      // adds the opposite value to the one given to model
                return TTCheckAll(nRest, model) && TTCheckAll(nRest, copyTrue);       // Recursive to create all models
            }
        }

        public bool AND(bool a, bool b)
        {
            return a && b;
        }

        public bool AND_MULTI(string[] symbols, Dictionary<string, bool> model)
        {
            bool value1 = model[symbols[0]];       // value 1 is first letter
            bool value2 = model[symbols[1]];       // value 2 is second letter
           
            for(int i = 2; i < symbols.Length; i++)
            {
                value1 = AND(value1, value2);       // value 1 is now the AND of the initial value 1 and value 2
                if (!value1)                        // if value1 is now false, return false
                    return false;
                value2 = model[symbols[i]];        // now make value 2 equal to the next element in the clause statement.
            }
            if (!AND(value1, value2))
                return false;
            return true;
        }

        public bool IMPLIC(bool a, bool b)
        {
            return !a || b;
        }

        public bool IsKBTrue(Dictionary<string, bool> model)  // all clauses are contained in the Clause list. Compares them to the facts as well
        {
            for(int i = 0; i < Clauses.Count; i++)
            {
                string[] impSplit = Clauses[i].Split("=>");         // split clause
                if(!Clauses[i].Contains("&"))                       // if it doesnt have an and
                {
                    if (!IMPLIC(model[impSplit[0]], model[impSplit[1]])) return false;
                }
                else
                {                                               // CLAUSE CONTAINS &
                    string[] oppSplit = impSplit[0].Split("&"); // Split by &
                    if(oppSplit.Length <=2 )                    // If length is <= 2 
                    {                                    // return false if they are not
                        if(!IMPLIC(AND(model[oppSplit[0]], model[oppSplit[1]]), model[impSplit[1]]))
                            return false;
                    }
                    else
                    {                                           // IF it gets here we know there are multiple & in the same clause
                        if (!IMPLIC(AND_MULTI(oppSplit, model), model[impSplit[1]]))
                            return false;
                    }
                }
                foreach(string symbol in Facts)
                {
                    if (!model[symbol])
                        return false;
                }
            }
            return true;
        }

        public bool IsAlphaTrue(Dictionary<string, bool> model)     // ALPHA BEING ASK
        {
            return model[Ask];
        }
        
        public int NumOfTruth
        {
            get { return _numOfTruth; }
            set { _numOfTruth = value; }
        }
    }
}
