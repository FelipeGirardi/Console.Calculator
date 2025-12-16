using CalculatorLibrary.Models;
using Newtonsoft.Json;

namespace CalculatorLibrary.Controllers;

public class Calculator
{
    JsonWriter writer;
    public string[] operations = ["a", "s", "m", "d", "sqr", "pow", "sin", "cos", "list", "del"];
    public Calculator()
    {
        StreamWriter logFile = File.CreateText("calculatorlog.json");
        logFile.AutoFlush = true;
        writer = new JsonTextWriter(logFile);
        writer.Formatting = Formatting.Indented;
        writer.WriteStartObject();
        writer.WritePropertyName("Operations");
        writer.WriteStartArray();
    }

    public double DoOperation(double num1, string op, double num2 = 0)
    {
        double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
        string opChar = "";

        writer.WriteStartObject();
        writer.WritePropertyName("Operand1");
        writer.WriteValue(num1);
        if (op != "sr" && op != "sin" && op != "cos")
        {
            writer.WritePropertyName("Operand2");
            writer.WriteValue(num2);
        }
        writer.WritePropertyName("Operation");

        // Use a switch statement to do the math.
        switch (op)
        {
            case "a":
                result = num1 + num2;
                opChar = "+";
                writer.WriteValue("Add");
                break;
            case "s":
                result = num1 - num2;
                opChar = "-";
                writer.WriteValue("Subtract");
                break;
            case "m":
                result = num1 * num2;
                opChar = "*";
                writer.WriteValue("Multiply");
                break;
            case "d":
                // Ask the user to enter a non-zero divisor.
                if (num2 != 0)
                {
                    result = num1 / num2;
                }
                opChar = "/";
                writer.WriteValue("Divide");
                break;
            case "sqr":
                result = Math.Sqrt(num1);
                opChar = "√";
                writer.WriteValue("Square root");
                break;
            case "pow":
                result = Math.Pow(num1, num2);
                opChar = "^";
                writer.WriteValue("Power");
                break;
            case "sin":
                result = Math.Sin(num1);
                opChar = "sin";
                writer.WriteValue("Sine");
                break;
            case "cos":
                result = Math.Cos(num1);
                opChar = "cos";
                writer.WriteValue("Cosine");
                break;
            // Return text for an incorrect option entry.
            default:
                break;
        }
        writer.WritePropertyName("Result");
        writer.WriteValue(result);
        writer.WriteEndObject();

        StoreCalculation(num1, opChar, num2, result);

        return result;
    }

    public void Finish()
    {
        writer.WriteEndArray();
        writer.WriteEndObject();
        writer.Close();
    }

    public void StoreCalculation(double num1, string op, double num2, double result)
    {
        Calculation calc = new Calculation(num1, op, num2, result);
        MockDatabase.Calculations.Add(calc);
    }

    public double GetLastResult()
    {
        return MockDatabase.Calculations.Last().result;
    }
}