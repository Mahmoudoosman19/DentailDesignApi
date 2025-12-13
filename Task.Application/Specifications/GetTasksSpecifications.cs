using Common.Domain.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Application.Features.CaseTask.Query.GetTasks;

namespace Task.Application.Specifications
{
    internal class GetTasksSpecifications : Specification<Domain.Entities.CaseTask>
    {
        public GetTasksSpecifications(GetTasksQuery query)
        {
            ApplyPaging(query.PageSize, query.PageIndex);
        }
    }
}
