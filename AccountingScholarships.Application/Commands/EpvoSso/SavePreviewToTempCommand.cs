using AccountingScholarships.Application.DTO.EpvoSso;
using MediatR;

namespace AccountingScholarships.Application.Commands.EpvoSso;

public record SavePreviewToTempCommand : IRequest<int>
{
    public List<EpvoStudentTempDto> Items { get; set; } = new();
    public string SessionId { get; set; } = string.Empty;
}
