namespace Project.DAL.Models
{
    public class JobCategory : BaseEntity
    {
        public string Name { get; set; }
        public List<Job> Jobs { get; set; }
    }
}
