using System.Text;

namespace STUDY.ConsoleApp.Phonebook;

public static class Helpers
{
    public static List<string> SeparateString(this List<string> input)
    {
        var output = new List<string>();
        StringBuilder builder = new StringBuilder();
        
        foreach (var item in input)
        {
            foreach (var c in item)
            {
                var character = c;
                if (char.IsUpper(character) && builder.Length > 1)
                {
                    builder.Append(' ');
                    character = char.ToLower(c);
                }
                builder.Append(character);
            }
            output.Add(builder.ToString());
            builder.Clear();
        }
        return output;
    }
}