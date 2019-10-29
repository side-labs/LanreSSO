// Copyright (c) Lanre. All rights reserved.

namespace Lanre.Application.Commands.UsersCrud
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using CSharpFunctionalExtensions;
    using Domain.Users;
    using Infrastructure.Repository;
    using MediatR;

    public class UsersCrudHandlers : IRequestHandler<UserCreateCommand, UserIdResponse>,
                                     IRequestHandler<UserUpdateCommand, UserIdResponse>,
                                     IRequestHandler<UserDeleteCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<User, Guid> _userRepository;

        public UsersCrudHandlers(IUnitOfWork unitOfWork, IRepository<User, Guid> userRepository)
        {
            this._unitOfWork = unitOfWork;
            this._userRepository = userRepository;
        }

        public async Task<UserIdResponse> Handle(UserCreateCommand request, CancellationToken cancellationToken)
        {
            var user = User.Create(request.Name, request.Surname);
            if (user.IsSuccess)
            {
                this._userRepository.Add(user.Value);
                await this._unitOfWork.SaveChangesAsync();
            }

            return new UserIdResponse() { Id = user.Value.Id, Result = user};
        }

        public async Task<UserIdResponse> Handle(UserUpdateCommand request, CancellationToken cancellationToken)
        {
            var user = await this._userRepository.GetByIdAsync(request.Id);
            var resultOfChanging = Result.Combine(
                user.SetName(request.Name),
                user.SetSurname(request.Surname));
            if (resultOfChanging.IsSuccess)
            {
                this._userRepository.Update(user);
                await this._unitOfWork.SaveChangesAsync();
                return new UserIdResponse()
                {
                    Id = user.Id, 
                    Result = Result.Success<User>(user),
                };
            }

            return new UserIdResponse()
            {
                Result = resultOfChanging.ConvertFailure<User>(),
            };

        }

        public async Task<Unit> Handle(UserDeleteCommand request, CancellationToken cancellationToken)
        {
            var user = await this._userRepository.GetByIdAsync(request.Id);
            this._userRepository.Remove(user);
            await this._unitOfWork.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
