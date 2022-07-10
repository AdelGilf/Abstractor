using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentResultExtension
{
    public static class ResultExtension
    {
        public static List<string> GetErrors(this Result result)
        {
            List<string> errors = new List<string>();
            foreach (var error in result.Errors)
            {
                errors.Add(error.Message);
            }
            return errors;
        }

        public static List<string> GetErrors<T>(this Result<T> result)
        {
            List<string> errors = new List<string>();
            foreach (var error in result.Errors)
            {
                errors.Add(error.Message);
            }
            return errors;
        }
    }
}
