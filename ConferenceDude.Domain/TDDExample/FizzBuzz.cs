using System.Diagnostics.CodeAnalysis;

namespace ConferenceDude.Domain.TDDExample
{
    [ExcludeFromCodeCoverage]
    public class FizzBuzz
    {
        public string ToFizzBuzz(int zahl)
        {
            var result = string.Empty;

            if (zahl % 3 == 0)
            {
                result = "Fizz";
            }

            if (zahl % 5 == 0)
            {
                result += "Buzz";
            }

            if (string.IsNullOrEmpty(result))
            {
                result = zahl.ToString();
            }


            //if (zahl % 15 == 0)
            //{
            //    return "FizzBuzz";
            //}
            //if (zahl % 5 == 0)
            //{
            //    return "Buzz";
            //}
            //if (zahl % 3 == 0)
            //{
            //    return "Fizz";
            //}

            return result;
        }
    }
}