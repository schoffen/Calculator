using System.Globalization;

namespace CalculatorLibrary;
using System.Diagnostics;
using Newtonsoft.Json;

public class Calculator
{
    JsonWriter jsonWriter;
    
    public Calculator()
    {
        StreamWriter logFile = File.CreateText("calculatorlog.json");
        Console.WriteLine(Path.GetFullPath("calculatorLog.json"));
        logFile.AutoFlush = true;
        jsonWriter = new JsonTextWriter(logFile);
        jsonWriter.Formatting = Formatting.Indented;
        jsonWriter.WriteStartObject();
        jsonWriter.WritePropertyName("Operations");
        jsonWriter.WriteStartArray();
    }
    
    public double DoOperation(double num1, double num2, string op)
    {
        double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
        jsonWriter.WriteStartObject();
        jsonWriter.WritePropertyName("Operand1");
        jsonWriter.WriteValue(num1);
        jsonWriter.WritePropertyName("Operand2");
        jsonWriter.WriteValue(num2);
        jsonWriter.WritePropertyName("Operation");
        // Use a switch statement to do the math.
        switch (op)
        {
            case "a":
                result = num1 + num2;
                jsonWriter.WriteValue("Add");
                break;
            case "s":
                result = num1 - num2;
                jsonWriter.WriteValue("Subtract");
                break;
            case "m":
                result = num1 * num2;
                jsonWriter.WriteValue("Multiply");
                break;
            case "d":
                // Ask the user to enter a non-zero divisor.
                if (num2 != 0)
                {
                    result = num1 / num2;
                }
                jsonWriter.WriteValue("Divide");
                break;
            // Return text for an incorrect option entry.
            default:
                break;
        }
        jsonWriter.WritePropertyName("Result");
        jsonWriter.WriteValue(result);
        jsonWriter.WriteEndObject();

        return result;
    }

    public void Finish()
    {
        jsonWriter.WriteEndArray();
        jsonWriter.WriteEndObject();
        jsonWriter.Close();
    }
}