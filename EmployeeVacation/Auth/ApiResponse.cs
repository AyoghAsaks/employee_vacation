namespace EmployeeVacation.Auth
{
    public class ApiResponse<T>
    {
        //public string? StatusCode { get; set; }
        public int? StatusCode { get; set; }
        public string? Message { get; set; }

        //New Additions
        public bool IsSuccess { get; set; } //New Addition
        public T? Response { get; set; } //New Addition
    }
}
