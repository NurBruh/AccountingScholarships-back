using AccountingScholarships.Domain.DTO.EpvoSso;
using MediatR;

namespace AccountingScholarships.Application.Queries.EpvoSso;

public record GetAllStudyLanguagesQuery : IRequest<IReadOnlyList<StudyLanguagesDto>>;
