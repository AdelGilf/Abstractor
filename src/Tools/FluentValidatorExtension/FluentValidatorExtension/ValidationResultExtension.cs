using FluentValidation.Results;

namespace FluentValidatorExtension
{
    public static class ValidationResultExtension
    {
        public static List<string> OutputErrors(this ValidationResult result)
        {
            return result.Errors.Select(x => x.ErrorMessage).ToList();
        }
    }
}