using BuildingBlocks.Domain;
using MediatR;
using Usermanagement.Domain;

namespace Usermanagement.Application;

public sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IPersonRepository _personRepository;
    private readonly IPasswordService _passwordService;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterCommandHandler(IUserRepository userRepository, IPersonRepository personRepository, IPasswordService passwordService, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _personRepository = personRepository;
        _passwordService = passwordService;
        _unitOfWork = unitOfWork;
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

        return person.Id;
    }
}
