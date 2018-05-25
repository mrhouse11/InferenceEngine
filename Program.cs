using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InferenceEngine
{
    class Program
    {
        public static string StripWhiteSpace(string s)
        {
            List<char> result = s.ToList();
            result.RemoveAll(c => c == ' ');
            s = new string(result.ToArray());
            return s;
        }

        static void Main(string[] args)
        {
            //ForwardChaining fc = new ForwardChaining("green", "croaks&flies=>frog;chirps&sings=>canary;frog=>green;canary=>yellow;croaks;flies;");
            //Console.WriteLine(fc.Execute());

            string[] file = System.IO.File.ReadAllLines(String.Format(@"C:\Assignments\{0}", args[1]));
            string tell = file[1];
            string ask = file[3];

            if (args[0] == "TT")
            {
                TruthTable TT = new TruthTable(ask, tell);
                Console.WriteLine(TT.Execute());
            }
            else if (args[0] == "FC")
            {
                ForwardChaining FC = new ForwardChaining(ask, tell);
                Console.WriteLine(FC.Execute());
            }
            else if (args[0] == "BC")
            {
                BackwardsChaining BC = new BackwardsChaining(ask, tell);
                Console.WriteLine(BC.Execute());
            }


            
        }
    }
}
