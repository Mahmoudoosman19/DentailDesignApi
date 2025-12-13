using Common.Application.Abstractions.Messaging;
using Common.Domain.Shared;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Domain.Repository_Abstraction;

namespace Task.Application.Features.CaseTask.Query.GetTaskById
{
    internal class GetTaskByIdQueryHandler : IQueryHandler<GetTaskByIdQuery, GetTaskByIdQueryResponse>
    {
        private readonly ITaskUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetTaskByIdQueryHandler(ITaskUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<ResponseModel<GetTaskByIdQueryResponse>> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        {
            var task = await _uow.Repository<Domain.Entities.CaseTask>().GetByIdAsync(request.TaskId);

            if (task == null)
                return ResponseModel.Failure<GetTaskByIdQueryResponse>("Task Not Found!");

            var result = _mapper.Map<GetTaskByIdQueryResponse>(task);

            return await System.Threading.Tasks.Task.FromResult(ResponseModel.Success(result, 1));
        }
    }
}
