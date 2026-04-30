using AccountingScholarships.Application.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Commands.EpvoSso;

public class SavePreviewToTempCommandHandler
    : IRequestHandler<SavePreviewToTempCommand, int>
{
    private readonly ISyncPreviewRepository _repository;

    public SavePreviewToTempCommandHandler(ISyncPreviewRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> Handle(
        SavePreviewToTempCommand request, CancellationToken cancellationToken)
    {
        return await _repository.SaveToTempAsync(
            request.Iins,
            cancellationToken);
    }
}
