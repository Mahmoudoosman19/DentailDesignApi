using Common.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.Application.Features.CaseTask.Command.AssignTask
{
    public class AssignTaskCommand : ICommand
    {
        public Guid TaskId { get; set; }
        public Guid DesignerId { get; set; }
    }
}
