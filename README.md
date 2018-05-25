#Inference Engine Problem:
LUKE MCWHA :: 101092956 :: SOLO GROUP

FEATURES/ BUGS/ MISSING:
- Features:
	This engine has Truth table, Forward chaining and Backwards Chaining for Horn Clause statements implemented.
	Implemented a Partial General Knowlegde Base for Truth Table.
- Bugs:
	- Neither forward chaining or backwards chaining could find a solution for GivenTest2.
	- Backwards chaining will sometimes give the entailed letters in the wrong order.
		eg for Test 4 (shown in test cases section) it will give <a, l, b, m, p, q> where it should be 
		<a, b, l, m, p, q>.
	- Backwards chaining algorithm doesn't continue to search if the current symbol is not 
		entailed by other symbols or is a fact.
	
- Missing:
	- General Knowledge Base for truth tables do not include these symbols: 


TEST CASES:
---- TEST 1 ----
TELL
p2=> p3; p3 => p1; c => e; b&e => f; f&g => h; p1=>d; p1&p3 => c; a; b; p2;
ASK
d
--RESULTS--
Expected TT: 	3
Actual TT: 	3
Expected FC: 	a, b, p2, p3, p1, d
Actual FC: 	a, b, p2, p3, p1, d
Expected BC: 	p2, p3, p1, d
Actual BC: 	p2, p3, p1, d

---- TEST 2 ----
TELL
p1 & p2 & p3 => p4; p5 & p6 => p4; p1 => p2; p1 & p2 => p3; p5 & p7 => p6; p1; p4;
ASK
p7
-- RESULTS --
Expected TT: 	3
Actual TT: 	3
Expected FC: 	Unsure
Actual FC: 	NO
Expected BC: 	Unsure
Actual BC: 	NO

---- TEST 3 ----
TELL
croaks & flies => frog; chirps & sings => canary; frog => green; canary => yellow; croaks; flies; ask green
ASK
green
--RESULTS--
Expected TT: 	10
Actual TT: 	10
Expected FC: 	croaks, flies, frog, green
Actual FC: 	croaks, flies, frog, green
Expected BC: 	croaks, flies, frog, green
Actual BC: 	croaks, flies, frog, green

---- TEST 4 ----
TELL
p=>q; l&m=>p; b&l=>m; a&p=>l; a&b=>l; a; b;
ASK
q
--RESULTS--
Expected TT:	1
Actual TT:	6
Expected FC:	a, b, l, m, p, q
Actual FC:	a, b, l, m, p, q
Expected BC:	a, b, l, m, p, q
Actual BC:	a, l, b, m, p, q

Found out: Truth table will not always give the correct answer? maybe when 2 statements entail the same thing.
		unsure on why.

Acknowledgements/ Resources:
--- Forward Chaining ---
http://snipplr.com/view/56296/ai-forward-chaining-implementation-for-propositional-logic-horn-form-knowledge-bases/	Viewed: 07/05/18
https://en.wikipedia.org/wiki/Forward_chaining		Viewed: 07/05/18
This resource helped me understand how to convert the concept of forward chaining to a program. The code I converted into C# code
to complete the forward chaining algorithm. The Forward chaining Wikipedia page helped me grasp the understanding of the concept.
Gave me information behind data structures, how to search the data and what it means.

--- Backward Chaining ---
https://en.wikipedia.org/wiki/Backward_chaining		Viewed: 14/05/18
http://snipplr.com/view/56297/ai-backward-chaining-implementation-for-propositional-logic-horn-form-knowledge-bases/	Viewed: 14/05/18
The wikipedia backwards chaining page helped my understanding of the concept by showing an example of the logic behind the algorithm.
The Implementation of backwards chaining was a stepping stone to understand how to convert the concept into C# code.
Same as the forward chaining implementation it gave me insight into data structures to use, how to search through the data and what it means.

--- Truth Table ---
https://github.com/aimacode/aima-java/blob/AIMA3e/aima-core/src/main/java/aima/core/logic/propositional/inference/TTEntails.java 	Viewed: 17-05-18
Artifical Intelligence: A Modern Approach	Viewed: 17-05-18
This github repository contains the text book pseudo-code implementation in Java. This gave me the understanding of recursion to create 
all the truth table posibilities and then test them against the knowledge base.
