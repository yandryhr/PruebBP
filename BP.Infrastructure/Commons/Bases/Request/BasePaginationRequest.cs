namespace BP.Infrastructure.Commons.Bases.Request
{
    public class BasePaginationRequest
    {
        public int NumPage { get; set; }
        public int NumRecordsPage { get; set; } = 10;        
        public string Order { get; set; } = "asc";
        public string? Sort { get; set; } = null;
        private readonly int NumMaxRecordPage = 50;
        public int Records {
            get => NumRecordsPage;
            set { 
                NumRecordsPage = value > NumMaxRecordPage? NumMaxRecordPage : value;
            }
        }

    }
}
