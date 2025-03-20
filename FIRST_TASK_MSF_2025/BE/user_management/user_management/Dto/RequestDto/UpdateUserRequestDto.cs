namespace user_management.Dto.RequestDto
{
    public class UpdateUserRequestDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string WorkUnit { get; set; }
        public string ResponsibleUnit { get; set; }
        public Boolean Status { get; set; } = true;
    }
}
