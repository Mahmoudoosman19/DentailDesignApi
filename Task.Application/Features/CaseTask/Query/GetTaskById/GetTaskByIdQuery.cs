using Common.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.Application.Features.CaseTask.Query.GetTaskById
{
    public class GetTaskByIdQuery : IQuery<GetTaskByIdQueryResponse>
    {
        public Guid TaskId { get; set; }
    }
}
