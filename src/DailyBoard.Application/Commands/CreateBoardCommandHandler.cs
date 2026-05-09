using System;
using System.Threading;
using System.Threading.Tasks;
using DailyBoard.Application.Repositories;
using DailyBoard.Domain;
using MediatR;

namespace DailyBoard.Application.Commands
{
    public class CreateBoardCommandHandler : IRequestHandler<CreateBoardCommand, Guid>
    {
        private readonly IBoardRepository _repo;
        public CreateBoardCommandHandler(IBoardRepository repo)
        {
            _repo = repo;
        }

        public async Task<Guid> Handle(CreateBoardCommand request, CancellationToken cancellationToken)
        {
            var board = Board.Create(request.Name, request.OwnerId);
            await _repo.AddAsync(board, cancellationToken);
            return board.Id;
        }
    }
}
