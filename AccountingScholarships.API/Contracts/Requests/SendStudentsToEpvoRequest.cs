namespace AccountingScholarships.API.Contracts.Requests
{
    public class SendStudentsToEpvoRequest
    {
        public IList<string> IINs { get; set; } = new List<string>();
    }
}
