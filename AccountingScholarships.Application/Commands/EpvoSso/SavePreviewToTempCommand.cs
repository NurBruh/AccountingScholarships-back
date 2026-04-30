using MediatR;

namespace AccountingScholarships.Application.Commands.EpvoSso;

public record SavePreviewToTempCommand : IRequest<int>
{
    public List<string> Iins { get; set; } = new();
}
