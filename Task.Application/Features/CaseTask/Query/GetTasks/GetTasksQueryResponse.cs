using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Domain.Enums;

namespace Task.Application.Features.CaseTask.Query.GetTasks
{
    internal class GetTasksQueryResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid CaseId { get; set; }
        public Guid DesignerId { get; set; }

        public DateTime StartedAt { get; set; }
        public DateTime EndedAt { get; set; }

        public TaskStatusEnum Status { get; set; }

        public DateTime AssignedAt { get; set; }

        public string Notes { get; set; }

        public DateTime CreatedOnUtc { get; set; }
        public DateTime ModifiedOnUtc { get; set; }
        public string Model3DPath { get; set; }
    }
}
