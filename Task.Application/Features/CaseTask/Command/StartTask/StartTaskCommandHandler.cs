using Common.Application.Abstractions.Messaging;
using Common.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Application.Specifications;
using Task.Domain.Repository_Abstraction;

namespace Task.Application.Features.CaseTask.Command.StartTask
{
    internal class StartTaskCommandHandler : ICommandHandler<StartTaskCommand>
    {
        private readonly ITaskUnitOfWork _uow;

        public StartTaskCommandHandler(ITaskUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<ResponseModel> Handle(StartTaskCommand request, CancellationToken cancellationToken)
        {
            var task = _uow.Repository<Domain.Entities.CaseTask>().GetByIdAsync(request.TaskId);

            if (task == null)
                return ResponseModel.Failure("Task Not Fount!");
            task.Result!.StartTask();

            await _uow.CompleteAsync();
            return ResponseModel.Success(); 
        }
    }
}
