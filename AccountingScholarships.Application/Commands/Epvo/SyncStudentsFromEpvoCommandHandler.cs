using AccountingScholarships.Domain.Interfaces;
using AccountingScholarships.Domain.Entities;
using MediatR;

namespace AccountingScholarships.Application.Commands.Epvo;

public class SyncStudentsFromEpvoCommandHandler : IRequestHandler<SyncStudentsFromEpvoCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEpvoRepository _epvoRepository;

    public SyncStudentsFromEpvoCommandHandler(IUnitOfWork unitOfWork, IEpvoRepository epvoRepository)
    {
        _unitOfWork = unitOfWork;
        _epvoRepository = epvoRepository;
    }

    public async Task<int> Handle(SyncStudentsFromEpvoCommand request, CancellationToken cancellationToken)
    {
        var epvoStudents = await _epvoRepository.GetAllAsync(cancellationToken);
        var syncedCount = 0;

        await _unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            foreach (var epvo in epvoStudents)
            {
                var existing = await _unitOfWork.Students.GetByIINAsync(epvo.IIN, cancellationToken);

                if (existing is null)
                {
                    var student = new Student
                    {
                        FirstName = epvo.FirstName,
                        LastName = epvo.LastName,
                        MiddleName = epvo.MiddleName,
                        IIN = epvo.IIN,
                        DateOfBirth = epvo.DateOfBirth,
                        Faculty = epvo.Faculty,
                        Speciality = epvo.Speciality,
                        Course = epvo.Course,
                        IsActive = epvo.IsActive,
                        iban = epvo.iban,
                        CreatedAt = DateTime.UtcNow
                    };

                    await _unitOfWork.Students.AddAsync(student, cancellationToken);
                    syncedCount++;
                }
                else
                {
                    existing.FirstName = epvo.FirstName;
                    existing.LastName = epvo.LastName;
                    existing.MiddleName = epvo.MiddleName;
                    existing.Faculty = epvo.Faculty;
                    existing.Speciality = epvo.Speciality;
                    existing.Course = epvo.Course;
                    existing.IsActive = epvo.IsActive;
                    existing.iban = epvo.iban;
                    existing.UpdatedAt = DateTime.UtcNow;

                    await _unitOfWork.Students.UpdateAsync(existing, cancellationToken);
                    syncedCount++;
                }
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitTransactionAsync(cancellationToken);
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }

        return syncedCount;
    }
}
