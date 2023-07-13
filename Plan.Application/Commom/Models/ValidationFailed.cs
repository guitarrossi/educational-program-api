using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plan.Application.Commom.Models
{
    public class ValidationFailed
    {
        public List<ValidationFailure> Errors { get; set; }

        public ValidationFailed(List<ValidationFailure> errors)
        {
            Errors = errors;
        }
    }
}
