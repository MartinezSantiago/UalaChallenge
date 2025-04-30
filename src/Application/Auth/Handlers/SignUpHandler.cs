using Application.Auth.Commands;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using BCrypt.Net;
using System.Threading.Tasks;
using System;
using System.Threading;

namespace Application.Auth.Handlers;

public class SignUpHandler : IRequestHandler<SignUpCommand, Guid>
{
    private readonly IUserRepository _userRepository;

    public SignUpHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Guid> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        if (await _userRepository.ExistsByUsernameAsync(request.Username))
            throw new InvalidOperationException("Username already exists.");

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
        var user = new User(request.Username, passwordHash);

        await _userRepository.CreateAsync(user);

        return user.Id;
    }
}