﻿namespace RecreateMe.Divisinos.Handlers
{
    public class CreateDivisionRequestHandler : IHandler<CreateDivisionRequest, CreateDivisionRespone>
    {
        private readonly IDivisionRepository _divisionRepository;

        public CreateDivisionRequestHandler(IDivisionRepository divisionRepository)
        {
            _divisionRepository = divisionRepository;
        }

        public CreateDivisionRespone Handle(CreateDivisionRequest request)
        {
            var division = new Division { Name = request.Name };

            _divisionRepository.Save(division);

            return new CreateDivisionRespone();
        }
    }

    public class CreateDivisionRequest
    {
        public string Name { get; set; }
    }

    public class CreateDivisionRespone
    {
    }
}