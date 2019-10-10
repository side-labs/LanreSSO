// Copyright (c) Lanre. All rights reserved.

namespace Lanre.Infrastructure.ControllersCore
{
    using System.Collections.Generic;
    using System.Linq;
    using Lanre.Infrastructure.Entities;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class ValidationResultModel
    {
        public ValidationResultModel(ModelStateDictionary modelState)
        {
            this.Message = "Validation Failed";
            this.Errors = modelState.Keys
                    .SelectMany(key => modelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage)))
                    .ToList();
        }

        public string Message { get; }

        public List<ValidationError> Errors { get; }
    }
}
