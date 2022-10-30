Start Time (30/10/2022 - Around 7:30PM)
End TIme (30/10/2022 - Around 12 AM)

This solution able to calculate math equation translated from string.
It supports basic calculation such as add, minus, multiple and division.

The program also able to handle brackets and nested bracket. 

The calculation is done from left to right based on the ranking of the operator.
The order is done following BODMAS which
1. Add and subtract is on the same order (left to right)
2. Mutiply and division is on the same order (left to right)
3. It will prioritize equation in brackets.

For addition and subtration, it is rank 1.
For multiplication and division, it is rank 2.
For brackets, it is rank 3.

Going from rank 3 is the highest and 1 is the lowest;

Included in the program is a unit test to make sure the result is on par with the requirement.

Short note: 
Sorry if the code is a bit messy. Didn't have much time to tidy it up as I am busy with work.
I do feel the code can be improve from code redundancy. I may update if I have the time tomorrow.
Also, the formatting is not following Visual Studio as I code with VSCode, didn't have the right extension for  Visual Studio formatting.
 
