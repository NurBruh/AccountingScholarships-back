using MediatR;

namespace AccountingScholarships.Application.Commands.Epvo;

/// <summary>
/// Команда массовой отправки изменённых студентов в ЕПВО.
/// Сравнивает данные Посредника (SSO) с ЕПВО и отправляет только тех, у кого есть расхождения — в виде массива.
/// </summary>
public record SyncChangedStudentsToEpvoCommand : IRequest<int>;
