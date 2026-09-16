using System.Linq;
using System.Text;

/// <summary>  
/// Class containing useful tools for development 
/// </summary>
public static class CodeUtils
{
    /// <summary>  
    /// Given a string removes all strange characters.
    /// <param name="str">string containing the string to be cleaned.</param>.
    /// </summary>
    public static string CleanString(string str)
    {
        var sb = new StringBuilder();
        foreach (var c in str.Where(c =>
                     c is >= '0' and <= '9' or >= 'A' and <= 'Z' or >= 'a' and <= 'z' or '.' or '_' or '-'))
        {
            sb.Append(c);
        }

        return sb.ToString();
    }
}