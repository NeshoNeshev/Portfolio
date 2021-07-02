namespace Portfolio.Services.Data
{
    using System;
    using System.Threading.Tasks;

    using Portfolio.Data.Common.Repositories;
    using Portfolio.Data.Models;

    public class CreatePositionService : ICreatePositionService
    {
        private readonly IDeletableEntityRepository<Position> positionRepository;
        private readonly IDeletableEntityRepository<Organization> organizationRepository;

        public CreatePositionService(IDeletableEntityRepository<Position> positionRepository,
            IDeletableEntityRepository<Organization> organizationRepository)
        {
            this.positionRepository = positionRepository;
            this.organizationRepository = organizationRepository;
        }

        public async Task CreateAsync(string positionName, string moreInformation, string period)
        {
            var position = new Position
            {
                Id = Guid.NewGuid().ToString(),
                PositionName = positionName,
                MoreInformation = moreInformation,
            };
            await this.positionRepository.AddAsync(position);
            await this.positionRepository.SaveChangesAsync();
        }
    }
}