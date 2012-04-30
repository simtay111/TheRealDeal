namespace RecreateMe.Login.Handlers
{
    public class LoginRequestHandle : IHandle<LoginRequest, LoginResponse>
    {
        private readonly IUserRepository _userRepository;

        public LoginRequestHandle(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public LoginResponse Handle(LoginRequest request)
        {
            if (_userRepository.FoundUserByNameAndPassword(request.Username, request.Password))
                return new LoginResponse(ResponseCodes.Success);

            return new LoginResponse(ResponseCodes.Failure);
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponse
    {
        public ResponseCodes Status { get; set; }

        public LoginResponse(ResponseCodes status)
        {
            Status = status;
        }
    }
}