using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

public class PostTweetCommand : IRequest<Guid>
{
    public Guid UserId { get; set; }

    [Required]
    [MaxLength(280)]
    public string Content { get; set; } = string.Empty;
}
