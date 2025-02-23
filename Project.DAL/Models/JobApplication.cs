using Project.DAL.Enums;

namespace Project.DAL.Models
{
    public class JobApplication : BaseEntity
    {
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string UserEmail { get; set; }
        public JobStatus JobStatus { get; set; } = JobStatus.Pending;
        public int UserAge { get; set; }
        public string CvUrl { get; set; }
        public string ImageUrl { get; set; }
        public int JobId { get; set; }
        public Job Job { get; set; }
    }
}
