using System;
using System.Net.Http;
using Cosmonaut;
using Cosmonaut.Extensions.Microsoft.DependencyInjection;
using Evidences.API.ResponseHandler;
using Evidences.Domain.Commands.CurrentSongCommands;
using Evidences.Domain.Commands.ReactionCommands;
using Evidences.Domain.Commands.ScoreCommands;
using Evidences.Domain.Commands.SignalRCommands;
using Evidences.Domain.Commands.SongCommands;
using Evidences.Domain.Commands.UserCommands;
using Evidences.Domain.Handlers.CommandHandlers.CurrentSongCommandHandlers;
using Evidences.Domain.Handlers.CommandHandlers.ReactionCommandHandlers;
using Evidences.Domain.Handlers.CommandHandlers.ScoreCommandHandlers;
using Evidences.Domain.Handlers.CommandHandlers.SignalRCommandHandlers;
using Evidences.Domain.Handlers.CommandHandlers.SongCommandHandlers;
using Evidences.Domain.Handlers.CommandHandlers.UserCommandHandlers;
using Evidences.Domain.Handlers.QueryHandlers.CurrentSongQueryHandlers;
using Evidences.Domain.Handlers.QueryHandlers.SongsQueryHandler;
using Evidences.Domain.Handlers.QueryHandlers.StateQueryHandlers;
using Evidences.Domain.Models;
using Evidences.Domain.Queries.CurrentSongQueries;
using Evidences.Domain.Queries.StateQuery;
using Evidences.Domain.Repositories;
using Evidences.Domain.Validator.CurrentSongCommandValidators;
using Evidences.Domain.Validator.ReactionCommandValidators;
using Evidences.Domain.Validator.ScoreCommandValidators;
using Evidences.Domain.Validator.SignalRCommandValidators;
using Evidences.Domain.Validator.SongCommandValidator;
using Evidences.Domain.Validator.UserCommandValidators;
using Evidences.Infra.Repositories;
using FluentValidation;
using FunctionMonkey.Abstractions;
using FunctionMonkey.Abstractions.Builders;
using FunctionMonkey.FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Evidences.API
{
    public class FunctionAppStartup : IFunctionAppConfiguration
    {
        public void Build(IFunctionHostBuilder builder)
        {
            builder
                .Setup((serviceCollection, commandRegistry) =>
                {
                    var cosmosDbUri = Environment.GetEnvironmentVariable("CosmosDbUri");
                    var cosmosDbKey = Environment.GetEnvironmentVariable("CosmosDbKey");
                    var cosmosSettings = new CosmosStoreSettings("EvidencesDb", new Uri(cosmosDbUri), cosmosDbKey);

                    commandRegistry.Register<StartSongCommandHandler>();
                    commandRegistry.Register<FinishSongCommandHandler>();
                    commandRegistry.Register<GetCurrentSongQueryHandler>();
                    commandRegistry.Register<SetScoreCommandHandler>();
                    commandRegistry.Register<AddSongCommandHandler>();
                    commandRegistry.Register<RemoveSongCommandHandler>();
                    commandRegistry.Register<GetSongsQueryHandler>();
                    commandRegistry.Register<AddUserCommandHandler>();
                    commandRegistry.Register<GetStateQueryHandler>();
                    commandRegistry.Register<ReactionCommandHandler>();
                    commandRegistry.Register<SignalRNegotiateCommandHandler>();

                    serviceCollection.AddCosmosStore<CurrentSong>(cosmosSettings);
                    serviceCollection.AddCosmosStore<Score>(cosmosSettings);
                    serviceCollection.AddCosmosStore<Song>(cosmosSettings);
                    serviceCollection.AddCosmosStore<User>(cosmosSettings);

                    serviceCollection.AddScoped<ICurrentSongRepository, CurrentSongRepository>();
                    serviceCollection.AddScoped<IScoreRepository, ScoreRepository>();
                    serviceCollection.AddScoped<ISongRepository, SongRepository>();
                    serviceCollection.AddScoped<IUserRepository, UserRepository>();

                    serviceCollection.AddScoped<IValidator<StartSongCommand>, StartSongCommandValidator>();
                    serviceCollection.AddScoped<IValidator<ReactionCommand>, ReactionCommandValidator>();
                    serviceCollection.AddScoped<IValidator<SetScoreCommand>, SetScoreCommandValidator>();
                    serviceCollection.AddScoped<IValidator<AddSongCommand>, AddSongCommandValidator>();
                    serviceCollection.AddScoped<IValidator<RemoveSongCommand>, RemoveSongCommandValidator>();
                    serviceCollection.AddScoped<IValidator<AddUserCommand>, AddUserCommandValidator>();
                    serviceCollection.AddScoped<IValidator<SignalRNegotiateCommand>, SignalRNegotiateCommandValidator>();
                })
                .OpenApiEndpoint(openApi => openApi
                    .Title("Evidences API")
                    .Version("1.0.0")
                    .UserInterface()
                )
                .AddFluentValidation()
                .Functions(functions => functions
                    .HttpRoute("v1/song/current/finish", route => route
                        .HttpFunction<FinishSongCommand>(AuthorizationTypeEnum.Anonymous, HttpMethod.Post)
                        .OutputTo.SignalRMessage("karaokeHub")
                    )
                    .HttpRoute("v1/song/current/start", route => route
                        .HttpFunction<StartSongCommand>(AuthorizationTypeEnum.Anonymous, HttpMethod.Post)
                        .OutputTo.SignalRMessage("karaokeHub")
                    )
                    .HttpRoute("v1/song/current", route => route
                        .HttpFunction<GetCurrentSongQuery>(AuthorizationTypeEnum.Anonymous, HttpMethod.Get)
                    )
                    .HttpRoute("v1/song/score", route => route
                        .HttpFunction<SetScoreCommand>(AuthorizationTypeEnum.Anonymous, HttpMethod.Post)
                        .Options(options => options.ResponseHandler<AcceptedResponseHandler>())
                    )
                    .HttpRoute("v1/song", route => route
                        .HttpFunction<AddSongCommand>(AuthorizationTypeEnum.Anonymous, HttpMethod.Post)
                        .OutputTo.SignalRMessage("karaokeHub")
                    )
                    .HttpRoute("v1/song", route => route
                        .HttpFunction<RemoveSongCommand>(AuthorizationTypeEnum.Anonymous, HttpMethod.Delete)
                        .OutputTo.SignalRMessage("karaokeHub")
                    )
                    .HttpRoute("v1/user", route => route
                        .HttpFunction<AddUserCommand>(AuthorizationTypeEnum.Anonymous, HttpMethod.Post)
                    )
                    .HttpRoute("v1/state", route => route
                        .HttpFunction<GetStateQuery>(AuthorizationTypeEnum.Anonymous, HttpMethod.Get)
                    )
                    .HttpRoute("v1/reaction", route => route
                        .HttpFunction<ReactionCommand>(AuthorizationTypeEnum.Anonymous, HttpMethod.Post)
                    )
                    .SignalR(signalR => signalR
                        .Negotiate<SignalRNegotiateCommand>("/v1/negotiate", AuthorizationTypeEnum.Anonymous, HttpMethod.Post)
                    )
                );
        }
    }
}