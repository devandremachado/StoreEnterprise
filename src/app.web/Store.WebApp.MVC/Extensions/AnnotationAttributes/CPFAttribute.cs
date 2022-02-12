using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;
using Store.Shared.Core.ValueObjects;
using System;
using System.ComponentModel.DataAnnotations;

namespace Store.WebApp.MVC.Extensions.AnnotationAttributes
{
    public class CPFAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return CPF.Validate(value.ToString()) ? ValidationResult.Success : new ValidationResult("CPF em formato inválido");
        }
    }

    public class CPFAttributeAdapter : AttributeAdapterBase<CPFAttribute>
    {
        public CPFAttributeAdapter(CPFAttribute attribute, IStringLocalizer stringLocalizer) : base(attribute, stringLocalizer)
        {

        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-cpf", GetErrorMessage(context));
        }
        public override string GetErrorMessage(ModelValidationContextBase validationContext)
        {
            return "CPF em formato inválido";
        }
    }

    public class CPFValidationAttributeAdapterProvider : IValidationAttributeAdapterProvider
    {
        private readonly IValidationAttributeAdapterProvider _baseProvider = new ValidationAttributeAdapterProvider();

        public IAttributeAdapter GetAttributeAdapter(ValidationAttribute attribute, IStringLocalizer stringLocalizer)
        {
            if (attribute is CPFAttribute CpfAttribute)
            {
                return new CPFAttributeAdapter(CpfAttribute, stringLocalizer);
            }

            return _baseProvider.GetAttributeAdapter(attribute, stringLocalizer);
        }
    }
}
