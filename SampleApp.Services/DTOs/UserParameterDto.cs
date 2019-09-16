namespace SampleApp.Services.DTOs
{
    public class UserParameterDto
    {
        public UserParameterDto()
        {
            Page = 1;
            PageSize = 10;
        }

        public int Page { get; set; }
        public int PageSize { get; set; }
        public string OrderBy { get; set; }
        public string ColumnSort { get; set; }
        public string Search { get; set; }
        
    }
}
