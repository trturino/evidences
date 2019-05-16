using System;
using AzureFromTheTrenches.Commanding.Abstractions;
using Evidences.Domain.Models;
using FunctionMonkey.Abstractions.SignalR;

namespace Evidences.Domain.Commands.SessionCommands
{
    public class EndSessionCommand : ICommand<SignalRMessage>
    {
    }
}