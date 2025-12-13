using Common.Application.Abstractions.Messaging;
using Common.Domain.Shared;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Application.Specifications;
using Task.Domain.Repository_Abstraction;

namespace Task.Application.Features.CaseTask.Query.GetTasks
{
    internal class GetTasksQueryHandler : IQueryHandler<GetTasksQuery, IReadOnlyList<GetTasksQueryResponse>>
    {
        private readonly ITaskUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetTasksQueryHandler(ITaskUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<ResponseModel<IReadOnlyList<GetTasksQueryResponse>>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
        {
            (var tasks,var count) = _uow.Repository<Domain.Entities.CaseTask>().GetWithSpec(new GetTasksSpecifications(request));

            if (count == 0)
                return ResponseModel.Failure<IReadOnlyList<GetTasksQueryResponse>>("No Tasks Found!");

            var result = _mapper.Map<IReadOnlyList<GetTasksQueryResponse>>(tasks);

            return await System.Threading.Tasks.Task.FromResult(ResponseModel.Success(result,count));
        }
    }
}
