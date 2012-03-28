namespace RecreateMe.Scheduling.Handlers
{
    public class DeletePickUpGameRequestHandler : IHandler<DeletePickUpGameRequest, DeletePickUpGameResponse>
    {
        private readonly IPickUpGameRepository _PickUpGameRepository;

        public DeletePickUpGameRequestHandler(IPickUpGameRepository PickUpGameRepository)
        {
            _PickUpGameRepository = PickUpGameRepository;
        }

        public DeletePickUpGameResponse Handle(DeletePickUpGameRequest request)
        {
            _PickUpGameRepository.DeleteGame(request.GameId);

            return new DeletePickUpGameResponse();
        }
    }

    public class DeletePickUpGameRequest
    {
        public string GameId { get; set; }
    }

    public class DeletePickUpGameResponse
    {
    }
}