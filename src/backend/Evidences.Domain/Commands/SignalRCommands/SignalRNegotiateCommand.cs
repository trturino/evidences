using AzureFromTheTrenches.Commanding.Abstractions;
using FunctionMonkey.Commanding.Abstractions;
using System;

namespace Evidences.Domain.Commands.SignalRCommands
{
    public class SignalRNegotiateCommand : ICommand<SignalRNegotiateResponse>
    {
        public Guid UserId { get; set; }
    }
}
