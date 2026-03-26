using AccountingScholarships.Domain.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public record GetAllEduNationalitiesQuery : IRequest<IReadOnlyList<Edu_NationalitiesDto>>;
