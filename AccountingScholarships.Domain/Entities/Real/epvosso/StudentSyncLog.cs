namespace AccountingScholarships.Domain.Entities.Real.epvosso
{
    /// <summary>
    /// Лог отправки студента в ЕПВО.
    /// Статус: Pending / Success / Error
    /// </summary>
    public class StudentSyncLog
    {
        public int Id { get; set; }

        /// <summary>studentId из STUDENT_TEMP / STUDENT_SSO</summary>
        public int StudentId { get; set; }

        /// <summary>ИИН студента для удобного поиска</summary>
        public string? IinPlt { get; set; }

        /// <summary>Дата и время отправки (UTC)</summary>
        public DateTime SentAt { get; set; }

        /// <summary>Статус: Pending / Success / Error</summary>
        public string Status { get; set; } = "Pending";

        /// <summary>Тело запроса (JSON) отправленного в ЕПВО</summary>
        public string? RequestBody { get; set; }

        /// <summary>Тело ответа (JSON) от ЕПВО</summary>
        public string? ResponseBody { get; set; }

        /// <summary>Текст ошибки если Status = Error</summary>
        public string? ErrorMessage { get; set; }

        /// <summary>Эндпоинт ЕПВО куда отправляли</summary>
        public string? EpvoEndpoint { get; set; }
    }
}
