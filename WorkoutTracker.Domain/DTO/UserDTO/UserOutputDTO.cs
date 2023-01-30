namespace WorkoutMasters.Domain.DTO.UserDTOs
{
    public class UserOutputDTO
    {
        public string UserName { get; set; }
        public string Token { get; internal set; }
    }
}
