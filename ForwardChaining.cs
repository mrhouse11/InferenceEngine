using System;
using System.Collections.Generic;
using System.Text;

namespace InferenceEngine
{
    public class ForwardChaining : Agent
    {
        public ForwardChaining(string a, string t) : base(a, t)
        {
            Init(Tell);
        }

        // method which sets up initial values for forward chaining
        // takes in string representing KB and seperates symbols and 
        // clauses, calculates count etc ...
        public void Init(string tell)
        {
            string[] sentances = tell.Split(';');
            for (int i = 0; i < sentances.Length; i++)
            {
                if (!sentances[i].Contains("=>"))
                {
                    Agenda.Add(sentances[i]);
                }
                else
                {
                    // add sentances
                    Clauses.Add(sentances[i]);
                    Count.Add(sentances[i].Split('&').Length);
                }
            }
        }

        public override string Execute()
        {
            {
                string output = "";
                if (FCentails())
                {
                    // the returned true so it entails
                    output = "Yes: ";
                    // for each entiled symbol
                    for (int i = 0; i < Entailed.Count; i++)
                    {
                        if (!(Entailed[i] == ""))
                            output += Entailed[i] + ", ";
                    }
                    output += Ask;
                }
                else
                    output = "No";
                return output;
            }
        }

        // FC algorithm
        public bool FCentails()
        {
            // loop through while there are unprocessed facts
            while (Agenda.Count > 0)
            {
                // take the first item and process it
                string p = Agenda[0];
                Agenda.RemoveAt(0);
                // add to entailed :: List of symbols already processed
                Entailed.Add(p);
                if (p == Ask)
                    return true;

                // for each of the clauses...
                for (int i = 0; i < Clauses.Count; i++)
                {
                    // .... that contain p in its premise
                    if (ClauseContains(Clauses[i], p, 0))
                    {
                        // reduce count : unknown elements in each premise
                        int j = Count[i];
                        Count[i] = --j;
                        // all the elements in the premise are now known
                        if (Count[i] == 0)
                        {
                            string[] separatingChars = { "=>" };
                            string head = Clauses[i].Split(separatingChars, System.StringSplitOptions.RemoveEmptyEntries)[1];
                            if (head.Equals(Ask))
                                return true;
                            if(!Entailed.Contains(head))
                                Agenda.Add(head);
                        }
                    }
                }
            }
            // if we arrive here then ask cannot entailed
            return false;
        }
    }
}
