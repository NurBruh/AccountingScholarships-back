using MediatR;
using AccountingScholarships.Domain.DTO.University;
using AccountingScholarships.Domain.Interfaces;

namespace AccountingScholarships.Application.Queries.University.Students;


public class GetAllSsoStudentsQueryHandler : IRequestHandler<GetAllSsoStudentsQuery, IReadOnlyList<StudentWithUserDto>>
{
    private readonly IEduStudentRepository _repository;

    public GetAllSsoStudentsQueryHandler(IEduStudentRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<StudentWithUserDto>> Handle(GetAllSsoStudentsQuery request, CancellationToken cancellationToken)
    {
        var students = await _repository.GetAllAsDtoAsync(cancellationToken);

        
        var studentDtos = students
            .Select(s => new StudentWithUserDto
            {
                
                StudentID = s.StudentID,
      
                FullName = s.FullName,
                
                Year = s.Year,
                GPA = s.GPA,
                GPA_Y = s.GPA_Y,
                EctsGPA = s.EctsGPA,
                EctsGPA_Y = s.EctsGPA_Y,
                NeedsDorm = s.NeedsDorm,
                AltynBelgi = s.AltynBelgi,
                IsScholarship = s.IsScholarship,
                IsKNB = s.IsKNB,
                EntryDate = s.EntryDate,
                GraduatedOn = s.GraduatedOn,
                AcademicStatusEndsOn = s.AcademicStatusEndsOn,
                AcademicStatusStartsOn = s.AcademicStatusStartsOn,
                ScholarshipOrderNumber = s.ScholarshipOrderNumber,
                ScholarshipOrderDate = s.ScholarshipOrderDate,
                ScholarshipDateStart = s.ScholarshipDateStart,
                ScholarshipDateEnd = s.ScholarshipDateEnd,
                
            })
            .ToList()
            .AsReadOnly();

        return studentDtos;
    }
}
