using System;
using System.Collections.Generic;
using System.Text;

namespace InferenceEngine
{
    public class BackwardsChaining : Agent
    {
        public BackwardsChaining(string a, string t) : base(a, t)
        {
            Init(Tell);
        }

        public void Init(string tell)
        {

            Agenda.Add(Ask);
            string[] sentances = tell.Split(';');
            for (int i = 0; i < sentances.Length; i++)
            {
                if (!sentances[i].Contains("=>"))
                {
                    Facts.Add(sentances[i]);
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
            string output = "";
            if (BCentails())
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

        public bool BCentails()
        {
            while (Agenda.Count > 0)
            {
                // take the first item and process it
                string q = Agenda[Agenda.Count -1];
                Agenda.RemoveAt(Agenda.Count-1);

                if(q != Ask)
                    if(!Entailed.Contains(q))
                        Entailed.Insert(0, q);

                if (!(Facts.Contains(q)))
                {
                    List<string> prem = new List<string>();
                    // for each of the clauses...
                    for (int i = 0; i < Clauses.Count; i++)
                    {
                        // .... that contain p in its premise
                        if (ClauseContains(Clauses[i], q, 1))
                        {
                            List<string> temp = GetPremises(Clauses[i]);
                            for(int j = 0; j < temp.Count; j++)
                            {
                                prem.Add(temp[j]);
                            }                            
                        }
                    }
                    if (prem.Count == 0)
                        return false;
                    else
                    {
                        for(int i = 0; i < prem.Count; i++)
                        {
                            if (!Entailed.Contains(prem[i]))
                                Agenda.Add(prem[i]);
                        }
                    }
                }
            }
            // while end
            return true;
        }
    }
}
