using Common.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.Application.Features.CaseTask.Command.EndTask
{
    public class EndTaskCommand : ICommand
    {
        public Guid TaskId { get; set; }
    }
}
