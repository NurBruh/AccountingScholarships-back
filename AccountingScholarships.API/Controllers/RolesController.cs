using AccountingScholarships.Domain.Interfaces;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccountingScholarships.API.Controllers;

/// <summary>
/// Управление ролями пользователей.
/// Роли: manager_or (Менеджер ОР), department_head (Заведующий кафедры), institute_director (Директор института).
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class RolesController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public RolesController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Получить список всех пользователей с их ролями.
    /// </summary>
    [HttpGet("users")]
    public async Task<IActionResult> GetAllUsersWithRoles(CancellationToken ct)
    {
        var users = await _unitOfWork.GetAllUsersWithRolesAsync(ct);
        var result = new List<object>();
        foreach (var u in users)
        {
            var scopeName = await _unitOfWork.GetScopeNameAsync(u.ScopeType, u.ScopeId, ct);
            result.Add(new
            {
                u.Id,
                u.Username,
                u.Email,
                u.Role,
                AssignedRole = u.RoleName,
                u.ScopeType,
                u.ScopeId,
                ScopeName = scopeName
            });
        }
        return Ok(result);
    }

    /// <summary>
    /// Получить доступные роли.
    /// </summary>
    [HttpGet("available")]
    public IActionResult GetAvailableRoles()
    {
        return Ok(new[]
        {
            new { Id = 1, Name = "manager_or", Description = "Менеджер ОР — полный доступ, синхронизация", ScopeType = "global", ScopeExample = "ScopeId = null" },
            new { Id = 2, Name = "department_head", Description = "Заведующий кафедры — просмотр своей кафедры", ScopeType = "department", ScopeExample = "ScopeId = ID кафедры (1-36)" },
            new { Id = 3, Name = "institute_director", Description = "Директор института — просмотр института", ScopeType = "institute", ScopeExample = "ScopeId = ID института (1-7)" }
        });
    }

    /// <summary>
    /// Назначить роль пользователю.
    /// Примеры:
    /// - Менеджер ОР: roleId=1, scopeType="global", scopeId=null
    /// - Зав. кафедры ПИ: roleId=2, scopeType="department", scopeId=1
    /// - Директор ИАИТ: roleId=3, scopeType="institute", scopeId=2
    /// </summary>
    /// <param name="request">Данные назначения роли.</param>
    /// <param name="ct">Токен отмены.</param>
    [HttpPost("assign")]
    public async Task<IActionResult> AssignRole([FromBody] AssignRoleRequest request, CancellationToken ct)
    {
        try
        {
            var assignment = await _unitOfWork.AssignRoleAsync(
                request.UserId, request.RoleId, request.ScopeType, request.ScopeId, ct);

            var scopeName = await _unitOfWork.GetScopeNameAsync(request.ScopeType, request.ScopeId, ct);

            return Ok(new
            {
                Message = $"Роль '{assignment.Role.Name}' назначена пользователю (ID={request.UserId})",
                UserId = request.UserId,
                Role = assignment.Role.Name,
                ScopeType = request.ScopeType,
                ScopeId = request.ScopeId,
                ScopeName = scopeName
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    /// <summary>
    /// Удалить назначение роли у пользователя (сбросить на "User").
    /// </summary>
    /// <param name="userId">ID пользователя.</param>
    /// <param name="ct">Токен отмены.</param>
    [HttpDelete("assign/{userId}")]
    public async Task<IActionResult> RemoveRole(int userId, CancellationToken ct)
    {
        await _unitOfWork.RemoveRoleAssignmentAsync(userId, ct);
        return Ok(new { Message = $"Роль пользователя (ID={userId}) сброшена на 'User'" });
    }
}

/// <summary>
/// Запрос на назначение роли.
/// </summary>
public class AssignRoleRequest
{
    /// <summary>ID пользователя</summary>
    public int UserId { get; set; }

    /// <summary>ID роли: 1=manager_or, 2=department_head, 3=institute_director</summary>
    public int RoleId { get; set; }

    /// <summary>Тип scope: "global", "department", "institute"</summary>
    public string ScopeType { get; set; } = "global";

    /// <summary>ID кафедры (1-36) или института (1-7). Null для global.</summary>
    public int? ScopeId { get; set; }
}
