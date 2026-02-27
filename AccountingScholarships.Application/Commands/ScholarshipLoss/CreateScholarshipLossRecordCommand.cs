using AccountingScholarships.Domain.DTO;
using MediatR;

namespace AccountingScholarships.Application.Commands.ScholarshipLoss;

public record CreateScholarshipLossRecordCommand(CreateScholarshipLossRecordDto Data) 
    : IRequest<ScholarshipLossRecordDto>;
