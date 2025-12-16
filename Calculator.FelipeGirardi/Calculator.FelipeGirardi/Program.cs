using CalculatorLibrary.Controllers;

class Program
{
    static void Main(string[] args)
    {
        bool endApp = false;
        int nTimesUsed = 0;

        // Display title as the C# console calculator app.
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");

        Calculator _calculator = new Calculator();
        CalculationHistory _calcHistory = new CalculationHistory();

        while (!endApp)
        {
            // Declare variables and set to empty.
            // Use Nullable types (with ?) to match type of System.Console.ReadLine
            string? numInput1 = "";
            string? numInput2 = "";
            double result = 0;

            // Ask the user to type the first number.
            Console.WriteLine($"Number of times calculator was used = {nTimesUsed}\n");
            // Ask the user to choose an operator.
            Console.WriteLine("Choose an action from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.WriteLine("\tsqr - Square root");
            Console.WriteLine("\tpow - Power");
            Console.WriteLine("\tsin - Sine");
            Console.WriteLine("\tcos - Cosine");
            Console.WriteLine("\tlist - Calculations list");
            Console.WriteLine("\tdel - Delete calculations list");

            Console.Write("Your option? ");

            string? op = Console.ReadLine();
            // Validate input is not null, and matches the pattern
            while(op == null || !_calculator.operations.Contains(op))
            {
                Console.WriteLine("This is not valid input. Please enter one of the commands above: ");
                op = Console.ReadLine();
            }

            if (op == "list")
            {
                _calcHistory.ShowCalculationHistory();
            } else if (op == "del")
            {
                _calcHistory.DeleteCalculationHistory(); 
            }
            else
            {
                double cleanNum1 = 0;
                bool didUseResult = false;

                // asks if user wants to use result from previous calculation
                if (!_calcHistory.IsCalculationListEmpty())
                {
                    Console.Write("Would you like to use the result from the last calculation as the first operand? (y/n) ");
                    string? useResult = Console.ReadLine();
                    while(useResult == null || (useResult.ToLower() != "y" && useResult.ToLower() != "n"))
                    {
                        Console.Write("This is not valid input. Please enter (y/n): ");
                        useResult = Console.ReadLine();
                    }

                    if (useResult.ToLower() == "y")
                    {
                        cleanNum1 = _calculator.GetLastResult();
                        didUseResult = true;
                        Console.WriteLine($"Number: {cleanNum1.ToString("0.##")}");
                    }
                }
                if (!didUseResult)
                {
                    // Ask the user to type the first number.
                    Console.Write("Type a number, and then press Enter: ");
                    numInput1 = Console.ReadLine();

                    while (!double.TryParse(numInput1, out cleanNum1))
                    {
                        Console.Write("This is not valid input. Please enter a numeric value: ");
                        numInput1 = Console.ReadLine();
                    }
                }

                double cleanNum2 = 0;
                // square root, sine and cosine only need 1 operand - skip operand 2 if that's the case
                if (op != "sqr" && op != "sin" && op != "cos")
                {
                    // Ask the user to type the second number.
                    Console.Write("Type another number, and then press Enter: ");
                    numInput2 = Console.ReadLine();

                    while (!double.TryParse(numInput2, out cleanNum2))
                    {
                        Console.Write("This is not valid input. Please enter a numeric value: ");
                        numInput2 = Console.ReadLine();
                    }
                }

                try
                {
                    result = (op != "sqr" && op != "sin" && op != "cos") ? 
                        _calculator.DoOperation(cleanNum1, op, cleanNum2) : 
                        _calculator.DoOperation(cleanNum1, op);

                    if (double.IsNaN(result))
                    {
                        Console.WriteLine("This operation will result in a mathematical error.\n");
                    }
                    else
                    {
                        Console.WriteLine($"Your result: {result.ToString("0.##")}\n");
                        nTimesUsed++;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                }
                Console.WriteLine("------------------------\n");
            }

            // Wait for the user to respond before closing.
            Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
            if (Console.ReadLine() == "n") endApp = true;

            Console.WriteLine("\n"); // Friendly linespacing.
        }

        _calculator.Finish();
        return;
    }
}