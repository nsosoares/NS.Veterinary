using FluentValidation.Results;

namespace NS.Veterinary.Api.Extensions
{
    public static class ValidationResultExtensions
    {
        public static Dictionary<string, string[]> ToDictionary(this List<ValidationFailure> errors)
        {
            return errors
                .GroupBy(x => x.PropertyName)
                .ToDictionary(x => x.Key, x => x.Select(y => y.ErrorMessage).Distinct().ToArray());
        }
    }
}
