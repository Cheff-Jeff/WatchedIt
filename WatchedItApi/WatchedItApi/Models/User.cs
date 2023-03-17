namespace WatchedItApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Password { get; set; } = "";
        public string Salt { get; set; } = "";

        public ICollection<MovieList>? MovieLists { get; set; }

        public User() { }

        public User(UserDto dto)
        {
            this.Id = 0;
            this.Name = dto.Name;
            this.Email = dto.Email;
            this.Phone = dto.Phone;
            this.Password = dto.Password;
            this.Salt = "String";
        }

        public void encrypt()
        {
            EncryptionHandler handler = new EncryptionHandler();
            byte[] salt = handler.AddSalt();
            this.Salt = Convert.ToBase64String(salt);
            this.Password = handler.hash(this.Password, salt);
        }

        public void keepOldInfo(string salt, string pass)
        {
            this.Salt = salt;
            this.Password = pass;
        }
    }
}
