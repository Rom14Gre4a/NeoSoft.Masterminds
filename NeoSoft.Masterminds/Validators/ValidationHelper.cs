using System.Text.RegularExpressions;

namespace NeoSoft.Masterminds.Validators
{
    public static class ValidationHelper
    {

        internal static bool ProperPassword(string password)
        {
            var pattern = @"^(?=.*[A - Za - z])(?=.*\d)(?=.*[@$!% *#?&])[A-Za-z\d@$!%*#?&]{6,}$";
            var regex = new Regex(pattern);

            return regex.IsMatch(password);
        }

        internal static bool ProperName(string name)
        {
            foreach(var letter in name)
                if (!char.IsLetter(letter))
                {
                    return false;
                }
            return true;
        }


    }
}
