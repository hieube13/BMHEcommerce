using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BMHEcommerce.Admin.Catalog.Manufacturers
{
    public class CreateUpdateManufacturersDtoValidator : AbstractValidator<CreateUpdateManufacturerDto>
    {
        public CreateUpdateManufacturersDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Code).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Slug).NotEmpty().MaximumLength(50);
        }
    }
}
