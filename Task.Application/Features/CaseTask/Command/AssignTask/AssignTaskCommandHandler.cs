using Common.Application.Abstractions.Messaging;
using Common.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Application.Specifications;
using Task.Domain.Repository_Abstraction;

namespace Task.Application.Features.CaseTask.Command.AssignTask
{
    internal class AssignTaskCommandHandler : ICommandHandler<AssignTaskCommand>
    {
        private readonly ITaskUnitOfWork _uow;

        public AssignTaskCommandHandler(ITaskUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<ResponseModel> Handle(AssignTaskCommand request, CancellationToken cancellationToken)
        {
            var task = _uow.Repository<Domain.Entities.CaseTask>().GetByIdAsync(request.TaskId);
            if (task == null)
                return ResponseModel.Failure("Task Not Found!");
            if (task.Result!.DesignerId.HasValue)
                return ResponseModel.Failure("Task is already assigned to designer!");

            task.Result.AssignDesigner(request.DesignerId);
            await _uow.CompleteAsync();
            
            return ResponseModel.Success();
        }
    }
}
