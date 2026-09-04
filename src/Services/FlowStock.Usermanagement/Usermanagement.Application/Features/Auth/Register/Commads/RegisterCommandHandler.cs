using BuildingBlocks.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using Usermanagement.Domain;

namespace Usermanagement.Application;

public sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IPersonRepository _personRepository;
    private readonly IPasswordService _passwordService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<RegisterCommandHandler> _logger;

    public RegisterCommandHandler(IUserRepository userRepository, IPersonRepository personRepository, IPasswordService passwordService, 
    IUnitOfWork unitOfWork , ILogger<RegisterCommandHandler> logger)
    {
        _userRepository = userRepository;
        _personRepository = personRepository;
        _passwordService = passwordService;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Guid> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        User existingUser = await _userRepository.GetByUsernameAsync(request.Username.Trim().ToLower(), cancellationToken);
        if (existingUser is not null)
            throw new DomainExceptions("This username is already exist");

        Person person = Person.Create(request.FirstName, request.LastName, request.NationalCode);

        person.CreateUser(request.Username,Email.Create(request.Email),_passwordService.Hash(request.Password),request.Mobile);

        await _personRepository.AddAsync(person, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("An user with username {Username} is created for the person {FirstName} - {LastName}"
        , request.Username,request.FirstName,request.LastName);

        return person.Id;
    }
}
