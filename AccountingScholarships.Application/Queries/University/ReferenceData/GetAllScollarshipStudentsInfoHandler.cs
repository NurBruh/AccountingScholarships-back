using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountingScholarships.Application.DTO.University;
using AccountingScholarships.Application.Interfaces;
using AccountingScholarships.Domain.Entities.Real.university;

namespace AccountingScholarships.Application.Queries.University.ReferenceData
{
    public class GetAllScollarshipStudentsInfoHandler : IRequestHandler<GetAllScollarshipStudentsInfo, IReadOnlyList<Scollarship_Students_InfoDto>>
    {
        private readonly ISsoRepository<Scollarship_Students_Info> _repository;
        public GetAllScollarshipStudentsInfoHandler(ISsoRepository<Scollarship_Students_Info> repository)
        {
            _repository = repository;
        }
        public async Task<IReadOnlyList<Scollarship_Students_InfoDto>> Handle(GetAllScollarshipStudentsInfo request, CancellationToken ct)
        {
            var entities = await _repository.GetAllAsync(ct);
            return entities
                .Select(e => new Scollarship_Students_InfoDto
                {
                    Id = e.Id,
                    Iin = e.Iin,
                    Full_Name = e.Full_Name,
                    Iic = e.Iic,
                    Bic = e.Bic,
                    studentID = e.studentID,
                    Updated_Date = e.Updated_Date
                })
                .ToList()
                .AsReadOnly();

        }
    }
}
