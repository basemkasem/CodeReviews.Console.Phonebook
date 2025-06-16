using System.Text;

namespace STUDY.ConsoleApp.Phonebook;

public static class Helpers
{
    public static string SeparateString(this string input)
    {
        StringBuilder output = new StringBuilder();

        foreach (var c in input)
        {
            var character = c;
            if (char.IsUpper(character) && output.Length > 1)
            {
                output.Append(' ');
                character = char.ToLower(c);
            }

            output.Append(character);
        }
        
        return output.ToString();
    }
}