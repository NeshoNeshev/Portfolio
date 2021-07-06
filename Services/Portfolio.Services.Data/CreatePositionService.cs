using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Portfolio.Services.Mapping;
using Portfolio.Web.ViewModels.Administration.Dashboard;

namespace Portfolio.Services.Data
{
    using System;
    using System.Threading.Tasks;

    using Portfolio.Data.Common.Repositories;
    using Portfolio.Data.Models;

    public class CreatePositionService : ICreatePositionService
    {
        private readonly IDeletableEntityRepository<Position> positionRepository;
        private readonly IDeletableEntityRepository<Sector> sectoRepository;

        public CreatePositionService(IDeletableEntityRepository<Position> positionRepository,
            IDeletableEntityRepository<Sector> sectoRepository)
        {
            this.positionRepository = positionRepository;
            this.sectoRepository = sectoRepository;
        }

        public async Task CreateAsync(CreatePositionInputModel model)
        {
            var exist = this.positionRepository.All().Any(x => x.PositionName == model.PositionName);
            if (exist)
            {
                return;
            }

            var position = new Position
            {
                Id = Guid.NewGuid().ToString(),
                PositionName = model.PositionName,
                MoreInformation = model.MoreInformation,
                SectorId = model.SectorId,
            };
            await this.positionRepository.AddAsync(position);
            await this.positionRepository.SaveChangesAsync();
        }

        public bool FindByNameAsync(string name)
            => this.sectoRepository
                .All()
                .Any(s => s.SectorName == name);

        public async Task UpdateAsync(EditPositionInputModel input)
        {
            var position = this.positionRepository
                .All()
                .FirstOrDefault(c => c.PositionName == input.PositionName);
            
            position.PositionName = input.PositionName;
            position.Period = input.Period;
            position.MoreInformation = input.MoreInformation;

            this.positionRepository.Update(position);
            await this.positionRepository.SaveChangesAsync();
        }
        //public IEnumerable<T> GetAll<T>(int? count = null)
        //{
        //    IQueryable<Position> query = this.positionRepository.All().OrderBy(x => x.PositionName);
        //    if (count.HasValue)
        //    {
        //        query = query.Take(count.Value);
        //    }

        //    return query.To<T>().ToList();
        //}
    }
}