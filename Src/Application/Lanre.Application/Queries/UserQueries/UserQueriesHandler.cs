// Copyright (c) Lanre. All rights reserved.

namespace Lanre.Application.Queries.UserQueries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Users;
    using Infrastructure.Entities;
    using Infrastructure.Repository;
    using MediatR;

    public class UserQueriesHandler : IRequestHandler<UserQuery, UserQueryResponse>,
                                      IRequestHandler<UsersQuery, IEnumerable<UserQueryResponse>>,
                                      IRequestHandler<UsersQueryPaginated, PaginatedResult<UserQueryResponse>>
    {
        private readonly IRepositoryReadOnly<User, Guid> userRepository;

        public UserQueriesHandler(IRepositoryReadOnly<User, Guid> userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<IEnumerable<UserQueryResponse>> Handle(UsersQuery request, CancellationToken cancellationToken)
        {
            var users = await this.userRepository.GetAsync();
            var usersMapped = users.Select(u => this.MapToResponse(u.Id, u.Name, u.Surname));
            return usersMapped;
        }

        public async Task<UserQueryResponse> Handle(UserQuery request, CancellationToken cancellationToken)
        {
            var user = await this.userRepository.GetByIdAsync(request.Id);

            if (user == null)
            {
                return null;
            }

            var userMapped = this.MapToResponse(user.Id, user.Name, user.Surname);
            return userMapped;
        }

        public async Task<PaginatedResult<UserQueryResponse>> Handle(UsersQueryPaginated request, CancellationToken cancellationToken)
        {
            var users = await this.userRepository.GetAsync(page: request.PageNumber, pageSize: request.PageSize, orderBy: this.GenerateOrderBy(request.OrderBy, request.OrderIsAsc));
            var usersCount = await this.userRepository.CountAsync();
            var paginatedResult = PaginatedResult<UserQueryResponse>.MapFromRequestMappingEntities<User>(request, users, u => this.MapToResponse(u.Id, u.Name, u.Surname), usersCount);

            return paginatedResult;
        }

        private UserQueryResponse MapToResponse(Guid id, string name, string surname)
        {
            return new UserQueryResponse(id, name, surname);
        }

        private OrderByCustom<User> GenerateOrderBy(string orderby, bool isAscending = true)
        {
            orderby = (orderby ?? "id").ToLowerInvariant();
            var filters = new Dictionary<string, Expression<Func<User, object>>>()
            {
                { "id", u => u.Id },
                { "name", u => u.Name },
                { "surname", u => u.Surname },
            };

            return new OrderByCustom<User>(isAscending, filters[orderby]);
        }
    }
}