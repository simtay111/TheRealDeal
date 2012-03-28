namespace RecreateMe.Scheduling.Handlers
{
    public class DeletePickUpGameRequestHandler : IHandler<DeletePickUpGameRequest, DeletePickUpGameResponse>
    {
        private readonly IPickUpGameRepository _pickUpGameRepository;

        public DeletePickUpGameRequestHandler(IPickUpGameRepository pickUpGameRepository)
        {
            _pickUpGameRepository = pickUpGameRepository;
        }

        public DeletePickUpGameResponse Handle(DeletePickUpGameRequest request)
        {
            var game = _pickUpGameRepository.GetPickUpGameById(request.GameId);

            if (game.Creator != request.ProfileId)
                return new DeletePickUpGameResponse {Status = ResponseCodes.NotCreator};

            _pickUpGameRepository.DeleteGame(request.GameId);

            return new DeletePickUpGameResponse {Status = ResponseCodes.Success};
        }
    }

    public class DeletePickUpGameRequest
    {
        public string GameId { get; set; }

        public string ProfileId { get; set; }
    }

    public class DeletePickUpGameResponse
    {
        public ResponseCodes Status { get; set; }
    }
}