
namespace ChatCommonLib.Models
{
    public class User
    {
        public virtual List<Message>? MessagesTo { get; set; } = [];
        public virtual List<Message>? MessagesFrom { get; set; } = [];
        public int Id { get; set; }
        public string? FullName { get; set; }

    }
}
