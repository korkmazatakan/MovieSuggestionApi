using Core.Entities;

namespace Entities.Dtos
{
    public class UserForUpdateDto:IDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}