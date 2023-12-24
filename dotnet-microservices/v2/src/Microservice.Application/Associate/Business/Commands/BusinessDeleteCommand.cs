﻿using GoodToCode.Shared.Cqrs;
using GoodToCode.Shared.Validation;
using Microservice.Infrastructure;
using Microservice.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Microservice.Application
{
    public class BusinessDeleteCommand : IRequest<CommandResponse<Guid>>
    {
        public Guid Key { get; set; }

        public BusinessDeleteCommand() { }

        public BusinessDeleteCommand(Guid key)
        {
            Key = key;
        }

        public class Handler : IRequestHandler<BusinessDeleteCommand, CommandResponse<Guid>>
        {
            private readonly BusinessDeleteValidator _validator;
            private readonly List<KeyValuePair<string, string>> _errors;
            private readonly IAssociateDbContext _dbContext;

            public Handler(IAssociateDbContext context)
            {
                _dbContext = context;
                _validator = new BusinessDeleteValidator();
                _errors = new List<KeyValuePair<string, string>>();
            }

            public async Task<CommandResponse<Guid>> Handle(BusinessDeleteCommand request, CancellationToken cancellationToken)
            {
                var result = new CommandResponse<Guid>() { Errors = GetRequestErrors(request) };

                if (result.Errors.Count == 0)
                {
                    try
                    {
                        var aggregate = new AssociateAggregate(_dbContext);
                        await aggregate.BusinessDeleteAsync(request.Key);
                        result.Result = request.Key;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);

                        result.ErrorInfo = new ErrorInfo()
                        {
                            UserErrorMessage = "An internal error has occured",
                            HasException = true
                        };
                    }
                }
                return result;
            }

            private List<KeyValuePair<string, string>> GetRequestErrors(BusinessDeleteCommand request)
            {
                var issues = _validator.Validate(request.Key).Errors;

                foreach (var issue in issues)
                    _errors.Add(new KeyValuePair<string, string>(issue.PropertyName, issue.ErrorMessage));
                return _errors;
            }

        }
    }
}
