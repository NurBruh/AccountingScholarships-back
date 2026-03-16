using AccountingScholarships.Domain.DTO.EpvoSso;
using MediatR;

namespace AccountingScholarships.Application.Queries.EpvoSso;

public record GetStudyFormByIdQuery(int Id) : IRequest<EpvoStudyFormsDto?>;
