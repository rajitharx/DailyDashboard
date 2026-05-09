using System;
using MediatR;

namespace DailyBoard.Application.Commands
{
    public record CreateBoardCommand(string Name, Guid OwnerId) : IRequest<Guid>;
}
