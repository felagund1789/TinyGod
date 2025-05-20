public class Utils
{
    public static int ParseInt(string text)
    {
        if (int.TryParse(text, out int number))
            return number;

        return 0;
    }
}