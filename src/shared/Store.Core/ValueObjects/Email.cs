using Store.Shared.Core.CustomException;
using System.Text.RegularExpressions;

namespace Store.Shared.Core.ValueObjects
{
    public class Email
    {
        public const int MinLength = 5;
        public const int MaxLength = 254;

        public string EmailAddress { get; private set; }

        //EF relation
        protected Email() { }

        public Email(string email)
        {
            if (Validate(email) == false) 
                throw new DomainException("E-mail is invalid.");
            EmailAddress = email;
        }

        public static bool Validate(string email)
        {
            var regexEmail = new Regex(@"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
            return regexEmail.IsMatch(email);
        }
    }
}
