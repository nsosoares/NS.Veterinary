﻿using FluentValidation;
using FluentValidation.Results;
using NS.Veterinaria.Api.Models;

namespace NS.Veterinaria.Api.Validations
{
    public abstract class EntityValidation<TEntity> : AbstractValidator<TEntity> where TEntity : Entity
    {
        protected EntityValidation()
        {
            ValidationResult = new ValidationResult();
        }

        public ValidationResult ValidationResult { get; protected set; }

        public async Task<bool> RunValidationAsync(TEntity entity)
        {
            Validate();
            ValidationResult = await ValidateAsync(entity);
            return ValidationResult.IsValid;
        }

        protected abstract void Validate();
    }
}
