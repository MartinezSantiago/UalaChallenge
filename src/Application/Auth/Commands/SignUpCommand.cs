using MediatR;
using System;

namespace Application.Auth.Commands;

public class SignUpCommand : IRequest<Guid>
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}