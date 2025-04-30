using MediatR;

namespace Application.Auth.Commands;

public class SignInCommand : IRequest<string>
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}