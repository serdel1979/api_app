namespace api_app.DTO
{

    public class ActivityDTO
    {
        public string Description { get; set; }

    }
    public class UserStaffDTO
    {
        public string UserId { get; set; }
        public string Entry_time { get; set; }
        public string Departure_time { get; set; }
        public List<ActivityDTO> Activities { get; set; }

    }

    public class ConfirmStaffDTO
    {
        public int ProjectId { get; set; }
        public string UserId { get; set; }
        public List<UserStaffDTO> Staff{ get; set; }
    }
}
