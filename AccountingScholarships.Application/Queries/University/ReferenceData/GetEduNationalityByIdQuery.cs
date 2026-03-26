using AccountingScholarships.Domain.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public record GetEduNationalityByIdQuery(int Id) : IRequest<Edu_NationalitiesDto?>;
