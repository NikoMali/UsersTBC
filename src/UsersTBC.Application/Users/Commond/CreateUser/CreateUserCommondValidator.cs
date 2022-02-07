using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using UsersTBC.Application.Models;

namespace UsersTBC.Application.Users.Commond.CreateUser
{
    class CreateUserCommondValidator: AbstractValidator<UserRequestModel>
    {
        public CreateUserCommondValidator()
        {
            RuleFor(v => v.FirstName)
                .MaximumLength(200)
                .NotEmpty();

            RuleFor(v => v.LastName)
                .MaximumLength(200)
                .NotEmpty();

        }
    }
}
