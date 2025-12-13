using Common.Application.Abstractions.Messaging;
using Common.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.Application.Features.CaseTask.Command.CreateTask
{
    public class CreateTaskCommand : ICommand
    {
        public Guid CaseId { get; set; }
        public string Title { get; set; }
        public Guid DesignerId { get; set; }
    }
}
