namespace api_app.Entities
{
    public class Assigned_Activity
    {
        public int Developed_ActivityId { get; set; }
        public string UserId { get; set; }
        public User user { get; set; }
        public Developed_Activity Developed_Activity { get; set; }
    }
}
