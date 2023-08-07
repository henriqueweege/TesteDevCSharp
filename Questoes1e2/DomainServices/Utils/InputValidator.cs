namespace DomainServices.Utils;

public static class InputValidator
{
    public static bool ValidateNumberInput(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return false;
        }
        input = input.Replace('.', '0');
        input = input.Replace(',', '0');
        if (!ContainsLetters(input) && !ContainsSpecialChar(input))
        {
            return true;
        }
        return false;
    }

    public static bool ValidateDepositWithdrawnInput(string input)
    {
        if (ValidateNumberInput(input))
        {
            if (double.Parse(input) >= 0)
            {
                return true;
            }
        }

        return false;
    }

    public static bool ValidateInitialDepositOptionInput(string input)
    {

        if (!string.IsNullOrWhiteSpace(input) && input.Count() == 1 && IsSOrN(input.ToLower()) && !ContainsNumbers(input)  && !ContainsSpecialChar(input))
        {
            return true;
        }
        return false;
    }

    


    public static bool ValidateNameInput(string input)
    {
        var inputSplit = input.Trim().Split(' ');

        for (var i = 0; i < inputSplit.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(inputSplit[i]) || ContainsNumbers(inputSplit[i]) || ContainsSpecialChar(inputSplit[i]))
            {
                return false;
            }
        }

        return true;
    }

    public static bool ValidateTeamNameInput(string input)
    {
        var inputSplit = input.Trim().Split(' ');

        for (var i = 0; i < inputSplit.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(inputSplit[i]) || ContainsNumbers(inputSplit[i]))
            {
                return false;
            }
        }

        return true;
    }

    private static bool IsSOrN(string input)
        => input == "s" || input == "n";
    private static bool ContainsSpecialChar(string input)
       => input.Where(x=>(!char.IsDigit(x) && !char.IsLetter(x))).Select(x => x).Count() > 0;
    private static bool ContainsNumbers(string input)
        => input.Where(char.IsDigit).Select(x => x).Count() > 0;
    private static bool ContainsLetters(string input)
        => input.Where(x => char.IsLetter(x)).Select(x => x).Count() > 0;
}
