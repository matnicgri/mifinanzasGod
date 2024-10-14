using FluentValidation;
using Mapster;
using MediatR;
using Mifinanazas.God.Applicattion.Dtos.Domain;
using Mifinanazas.God.Applicattion.Dtos.Req;
using Mifinanazas.God.Applicattion.Dtos.Res;
using Mifinanazas.God.Applicattion.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mifinanazas.God.Applicattion.Features.Game.Commands
{
    public class MoveOptionsCommandHandler : IRequestHandler<MoveOptionsCommand, ResultObject<bool>>
    {        
        private readonly IMovementsRepository _movementsRepository;
        private readonly IValidator<MoveOptionsCommand> _validator;
        public MoveOptionsCommandHandler(IMovementsRepository movementsRepository,
                                         IValidator<MoveOptionsCommand> validator
                                         )
        {
            _movementsRepository = movementsRepository;
            _validator = validator;
        }
        public async Task<ResultObject<bool>> Handle(MoveOptionsCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);

            List<MoveOptionReqDto> lReq = request.moveOptions.Select(x => x.Adapt<MoveOptionReqDto>()).ToList();

            bool result = await _movementsRepository.Update(lReq);

            return new ResultObject<bool> { success = true, error = "", data = result };
        }
    }
}
