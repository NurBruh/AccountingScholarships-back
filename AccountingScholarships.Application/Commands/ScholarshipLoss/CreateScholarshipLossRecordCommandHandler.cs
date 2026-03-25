using AccountingScholarships.Domain.DTO;
using AccountingScholarships.Domain.Entities.Testing.Scholarships;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Commands.ScholarshipLoss;

public class CreateScholarshipLossRecordCommandHandler
    : IRequestHandler<CreateScholarshipLossRecordCommand, ScholarshipLossRecordDto>
{
    private readonly IScholarshipLossRepository _repository;

    public CreateScholarshipLossRecordCommandHandler(IScholarshipLossRepository repository)
    {
        _repository = repository;
    }

    public async Task<ScholarshipLossRecordDto> Handle(
        CreateScholarshipLossRecordCommand request, CancellationToken cancellationToken)
    {
        var data = request.Data;

        var entity = new ScholarshipLossRecord
        {
            FirstName = data.FirstName,
            LastName = data.LastName,
            MiddleName = data.MiddleName,
            IIN = data.IIN,
            LostDate = data.LostDate,
            OrderNumber = data.OrderNumber,
            OrderDate = data.OrderDate,
            Reason = data.Reason,
            ScholarshipName = data.ScholarshipName
        };

        var created = await _repository.AddAsync(entity, cancellationToken);

        return new ScholarshipLossRecordDto
        {
            Id = created.Id,
            FirstName = created.FirstName,
            LastName = created.LastName,
            MiddleName = created.MiddleName,
            IIN = created.IIN,
            LostDate = created.LostDate,
            OrderNumber = created.OrderNumber,
            OrderDate = created.OrderDate,
            Reason = created.Reason,
            ScholarshipName = created.ScholarshipName,
            CreatedAt = created.CreatedAt
        };
    }
}
