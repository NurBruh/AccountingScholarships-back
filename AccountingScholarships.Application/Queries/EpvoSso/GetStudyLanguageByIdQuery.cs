using AccountingScholarships.Domain.DTO.EpvoSso;
using MediatR;

namespace AccountingScholarships.Application.Queries.EpvoSso;

public record GetStudyLanguageByIdQuery(int Id) : IRequest<StudyLanguagesDto?>;
