using Common.Application.Abstractions.Messaging;
using Common.Domain.Repositories;
using Common.Domain.Shared;
using IdentityHelper.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Domain.Entities;
using Task.Domain.Repository_Abstraction;

namespace Task.Application.Features.CaseTask.Command.CreateTask
{
    internal class CreateTaskCommandHandler : ICommandHandler<CreateTaskCommand>
    {
        private readonly ITaskUnitOfWork _uow;
        private readonly ITokenExtractor _tokenExtractor;

        public CreateTaskCommandHandler(ITaskUnitOfWork uow,ITokenExtractor tokenExtractor)
        {
            _uow = uow;
            _tokenExtractor = tokenExtractor;
        }
        public async Task<ResponseModel> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            //if (!_tokenExtractor.IsUserAuthenticated())
            //    return ResponseModel.Failure("User is not authenticated!");

            //var permissions = _tokenExtractor.GetPermissions();
            //if (!permissions.Contains("AddTask"))
            //    return ResponseModel.Failure("You do not have permission to add task.");

            var task = new Domain.Entities.CaseTask(
                    caseId: request.CaseId,
                    title: request.Title
            );
            task.AssignDesigner(request.DesignerId);

            await _uow.Repository<Domain.Entities.CaseTask>().AddAsync(task, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);

            return ResponseModel.Success(task.Id);
        }
    }
}
