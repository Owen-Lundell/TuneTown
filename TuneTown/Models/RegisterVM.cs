namespace TuneTown.Models
{
    public class RegisterVM
    {
        //string.empty is just an initializer for an empty string value of "", just makes sure the value isnt null when it starts
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
