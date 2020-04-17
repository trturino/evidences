using System;
using AzureFromTheTrenches.Commanding.Abstractions;

namespace Evidences.Domain.Commands.NotifyUserCommands
{
    public class NotifyUserCommand : ICommand<bool>
    {
        public Guid UserId { get; set; }
    }
}