using System.Text.RegularExpressions;

namespace RecreateMe.Login.Handlers
{
    public class RegisterUserHandle : IHandle<RegisterUserRequest, RegisterUserResponse>
    {
        private readonly IUserRepository _userRepository;

        public RegisterUserHandle(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public RegisterUserResponse Handle(RegisterUserRequest request)
        {
            if (string.IsNullOrEmpty(request.ConfirmPassword) || string.IsNullOrEmpty(request.Password) ||
                string.IsNullOrEmpty(request.LoginEmail))
                return new RegisterUserResponse(ResponseCodes.FieldsAreBlank);

            if (!Regex.IsMatch(request.LoginEmail, Constants.UserNameRegex))
                return new RegisterUserResponse(ResponseCodes.BadUserNameFormat);

            if (request.Password != request.ConfirmPassword)
                return new RegisterUserResponse(ResponseCodes.PasswordsDontMatch);

            if (request.Password.Length < Constants.MinPasswordLength || request.Password.Length > Constants.MaxPasswordLength)
                return new RegisterUserResponse(ResponseCodes.BadPasswordLength);

            if (_userRepository.AlreadyExists(request.LoginEmail))
                return new RegisterUserResponse(ResponseCodes.UserAlreadyExists);

            _userRepository.CreateUser(request.LoginEmail, request.Password);

            return new RegisterUserResponse(ResponseCodes.Success);
        }
    }

    public class RegisterUserRequest
    {
        public string LoginEmail { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class RegisterUserResponse
    {
        public readonly ResponseCodes Status;

        public RegisterUserResponse(ResponseCodes status)
        {
            Status = status;
        }
    }
}