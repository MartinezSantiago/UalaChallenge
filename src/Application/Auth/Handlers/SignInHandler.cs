using Application.Auth.Commands;
using Domain.Repositories;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System;
using Application.Common.Exceptions;
using Application.Common.Interfaces;

namespace Application.Auth.Handlers;

public class SignInHandler : IRequestHandler<SignInCommand, string>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;

    public SignInHandler(IUserRepository userRepository, IJwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    public async Task<string> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByUsernameAsync(request.Username);
        if (user == null)
            throw new UnauthorizedException("Invalid credentials.");

        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
        if (!isPasswordValid)
            throw new UnauthorizedException("Invalid credentials.");

        return _jwtService.GenerateToken(user);
    }
}
