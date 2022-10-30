using System;

namespace CalculatorApp
{
    public static class Calculator
    {
        public static double Calculations(string equation)
        {
            return CalculatorFunction(equation);
        }

        public static double CalculatorFunction(string sum)
        {
            var operatorList =
                new List<string> { "+", "-", "*", "/", "(", ")" };
            var bracketList = new List<string> { "(", ")" };

            // store individual character in array
            // using split since the equation will always have space between each character
            var equationArray = sum.Split(" ");

            var equationDictionary = new Dictionary<int, string>();
            var operators = new Dictionary<int, string>();
            var brackets = new Dictionary<int, string>();


            // Store operator, bracket and equation in dicionary for position indexing. Make sure the equation is in correct order
            for (int i = 0; i < equationArray.Length; i++)
            {
                if (operatorList.Contains(equationArray[i]))
                {
                    operators.Add(i, equationArray[i]);
                }
                if (bracketList.Contains(equationArray[i]))
                {
                    brackets.Add(i, equationArray[i]);
                }

                equationDictionary.Add(i, equationArray[i]);
            }

            while (equationDictionary.Count > 1)
            {
                double a = 0;
                double b = 0;
                bool isBracket = false;
                bool isBracketEquationLong = false;
                var key = FindHighestOperator(operators);

                if (bracketList.Contains(operators[key]))
                {
                    isBracket = true;
                }

                double tempResult = 0;
                if (isBracket)
                {
                    int newKey = FindNestedBracket(equationDictionary, key);

                    if (newKey != 0)
                    {
                        key = newKey;
                    }

                    var equation =
                        GetEquationInBracket(equationDictionary, key);

                    var equationCount = equation.Count;

                    if (equationCount > 3)
                    {
                        var currentEquationCount = equationCount + 2; // including close and open bracket (for removing purpose)
                        var currEquationOperators =
                            new Dictionary<int, string>();

                        // get sub equation operators index for parent equation
                        var subEquationOperator = new Dictionary<int, string>();
                        for (int i = key; i < key + equationCount; i++)
                        {
                            if (operatorList.Contains(equationDictionary[i]))
                            {
                                subEquationOperator
                                    .Add(i, equationDictionary[i]);
                            }
                        }

                        for (int i = 0; i < equation.Count; i++)
                        {
                            if (operatorList.Contains(equation[i]))
                            {
                                currEquationOperators.Add(i, equation[i]);
                            }
                        }

                        while (equation.Count > 1)
                        {
                            var bracketKey =
                                FindHighestOperator(currEquationOperators);

                            a = Double.Parse(equation[bracketKey - 1]);
                            b = Double.Parse(equation[bracketKey + 1]);
                            tempResult = Calculate(equation[bracketKey], a, b);

                            equation =
                                RebuildEquation(equation,
                                bracketKey - 1,
                                tempResult.ToString(),
                                isBracket,
                                isBracketEquationLong);

                            currEquationOperators.Remove (bracketKey);

                            //update operator key
                            if (currEquationOperators.Any())
                            {
                                var operatorKeyList =
                                    currEquationOperators.Keys.ToList();

                                var tempOperatorKeyList = operatorKeyList;
                                foreach (var currKey in tempOperatorKeyList)
                                {
                                    if (currKey > bracketKey)
                                    {
                                        // get value before remove
                                        var operatorSymbol =
                                            currEquationOperators[currKey];
                                        currEquationOperators.Remove (currKey);
                                        currEquationOperators
                                            .Add(currKey - 2, operatorSymbol);
                                    }
                                }
                            }
                        }
                        isBracketEquationLong = true;
                        tempResult = Double.Parse(equation.First().Value);

                        //rebuild equation
                        equationDictionary =
                            RebuildEquation(equationDictionary,
                            key,
                            tempResult.ToString(),
                            isBracket,
                            isBracketEquationLong,
                            currentEquationCount);

                        foreach(var item in subEquationOperator)
                        {
                            operators.Remove(item.Key);
                        }
                        operators.Remove (key);
                        operators.Remove(key + currentEquationCount - 1); //closing bracket
                    }
                    else
                    {
                        var tempKey = key + 2;

                        a = Double.Parse(equationDictionary[tempKey - 1]);
                        b = Double.Parse(equationDictionary[tempKey + 1]);
                        tempResult = Calculate(operators[tempKey], a, b);

                        equationDictionary =
                            RebuildEquation(equationDictionary,
                            key,
                            tempResult.ToString(),
                            isBracket,
                            isBracketEquationLong);

                        operators.Remove (key);
                        operators.Remove (tempKey);
                        operators.Remove(key + 4); // closing bracket

                        //update operator key
                        if (operators.Any())
                        {
                            var operatorKeyList = operators.Keys.ToList();

                            var tempOperatorKeyList = operatorKeyList;
                            foreach (var currKey in tempOperatorKeyList)
                            {
                                if (currKey > key)
                                {
                                    // get value before remove
                                    var operatorSymbol = operators[currKey];
                                    operators.Remove (currKey);
                                    operators.Add(currKey - 4, operatorSymbol);
                                }
                            }
                        }
                    }
                }
                else
                {
                    a = Double.Parse(equationDictionary[key - 1]);
                    b = Double.Parse(equationDictionary[key + 1]);
                    tempResult = Calculate(operators[key], a, b);

                    equationDictionary =
                        RebuildEquation(equationDictionary,
                        key - 1,
                        tempResult.ToString(),
                        isBracket,
                        isBracketEquationLong);

                    operators.Remove (key);

                    //update operator key
                    if (operators.Any())
                    {
                        var operatorKeyList = operators.Keys.ToList();

                        var tempOperatorKeyList = operatorKeyList;
                        foreach (var currKey in tempOperatorKeyList)
                        {
                            if (currKey > key)
                            {
                                // get value before remove
                                var operatorSymbol = operators[currKey];
                                operators.Remove (currKey);
                                operators.Add(currKey - 2, operatorSymbol);
                            }
                        }
                    }
                }
            }

            var result = Double.Parse(equationDictionary.First().Value);
            return result;
        }

        public static Dictionary<int, string>
        GetEquationInBracket(
            Dictionary<int, string> equation,
            int openBracketKey
        )
        {
            var equationInBracket = new Dictionary<int, string>();
            int index = 0;
            for (int i = openBracketKey + 1; i < equation.Count; i++)
            {
                if (equation[i] == ")") break;

                equationInBracket.Add(index, equation[i]);
                index++;
            }
            return equationInBracket;
        }

        public static int
        FindNestedBracket(Dictionary<int, string> equation, int openBracketKey)
        {
            int newKey = 0;

            for (int i = openBracketKey + 1; i < equation.Count; i++)
            {
                if (equation[i] == ")")
                {
                    break;
                }

                if (equation[i] == "(")
                {
                    newKey = i;
                    break;
                }
            }
            return newKey;
        }

        public static Dictionary<int, string>
        RebuildEquation(
            Dictionary<int, string> oldEquation,
            int firstRemovedPosition,
            string newValue,
            bool isBracket,
            bool isBracketEquationLong,
            int? nestedBracketLength = 0
        )
        {
            var newEquation = new Dictionary<int, string>();
            if (isBracket && !isBracketEquationLong)
            {
                for (int i = 0; i < oldEquation.Count; i++)
                {
                    if (firstRemovedPosition == i)
                    {
                        newEquation.Add (i, newValue);
                    }
                    else
                    {
                        if (i > firstRemovedPosition)
                        {
                            if (
                                i + 4 < oldEquation.Count // to avoid null when exceed dictionary size
                            ) newEquation.Add(i, oldEquation[i + 4]);
                        }
                        else
                        {
                            newEquation.Add(i, oldEquation[i]);
                        }
                    }
                }
            }
            else if (isBracket && isBracketEquationLong)
            {
                for (int i = 0; i < oldEquation.Count; i++)
                {
                    if (firstRemovedPosition == i)
                    {
                        newEquation.Add (i, newValue);
                    }
                    else
                    {
                        if (i > firstRemovedPosition)
                        {
                            if (
                                nestedBracketLength.HasValue &&
                                i + nestedBracketLength < oldEquation.Count // to avoid null when exceed dictionary size
                            )
                                newEquation
                                    .Add(i,
                                    oldEquation[i + nestedBracketLength.Value]);
                        }
                        else
                        {
                            newEquation.Add(i, oldEquation[i]);
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < oldEquation.Count; i++)
                {
                    if (firstRemovedPosition == i)
                    {
                        newEquation.Add (i, newValue);
                    }
                    else
                    {
                        if (i > firstRemovedPosition)
                        {
                            if (i + 2 < oldEquation.Count)
                                newEquation.Add(i, oldEquation[i + 2]);
                        }
                        else
                        {
                            newEquation.Add(i, oldEquation[i]);
                        }
                    }
                }
            }
            return newEquation;
        }

        public static int FindHighestOperator(Dictionary<int, string> operators)
        {
            // add = 1;
            // minus = 1;
            // multiple = 2;
            // divide = 2;
            // bracket = 3
            var position = 0;
            var currOperatorValue = 0;
            var operatorValue = 0;

            foreach (var item in operators)
            {
                switch (item.Value)
                {
                    case "+":
                        currOperatorValue = 1;
                        if (currOperatorValue > operatorValue)
                        {
                            operatorValue = currOperatorValue;
                            position = item.Key;
                        }
                        break;
                    case "-":
                        currOperatorValue = 1;
                        if (currOperatorValue > operatorValue)
                        {
                            operatorValue = currOperatorValue;
                            position = item.Key;
                        }
                        break;
                    case "*":
                        currOperatorValue = 2;
                        if (currOperatorValue > operatorValue)
                        {
                            operatorValue = currOperatorValue;
                            position = item.Key;
                        }
                        break;
                    case "/":
                        currOperatorValue = 2;
                        if (currOperatorValue > operatorValue)
                        {
                            operatorValue = currOperatorValue;
                            position = item.Key;
                        }
                        break;
                    case "(":
                        currOperatorValue = 3;
                        if (currOperatorValue > operatorValue)
                        {
                            operatorValue = currOperatorValue;
                            position = item.Key;
                        }
                        break;
                    case ")":
                        currOperatorValue = 3;
                        if (currOperatorValue > operatorValue)
                        {
                            operatorValue = currOperatorValue;
                            position = item.Key;
                        }
                        break;
                }
            }
            return position;
        }

        public static double Calculate(string checkOperator, double a, double b)
        {
            double result = 0;
            switch (checkOperator)
            {
                case "+":
                    result = Sum(a, b);
                    break;
                case "-":
                    result = Subtract(a, b);
                    break;
                case "*":
                    result = Multiply(a, b);
                    break;
                case "/":
                    result = Divide(a, b);
                    break;
            }
            return result;
        }

        public static double Sum(double a, double b)
        {
            return a + b;
        }

        public static double Subtract(double a, double b)
        {
            return a - b;
        }

        public static double Multiply(double a, double b)
        {
            return a * b;
        }

        public static double Divide(double a, double b)
        {
            return a / b;
        }
    }
}
