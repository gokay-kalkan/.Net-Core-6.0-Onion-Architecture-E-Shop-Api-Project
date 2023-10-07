

using Microsoft.AspNetCore.Identity;

namespace Application.CustomizeIdentityErrorDescriber
{
    public class CustomIdentityErrorDescriber:IdentityErrorDescriber
    {
        public override IdentityError DefaultError()
        {
            return new IdentityError
            {
                Code = nameof(DefaultError),
                Description = "Bir kimlik doğrulama hatası oluştu."
            };
        }

        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError
            {
                Code = "Kullanılan Email Adresi",
                Description = $"{email} adresi zaten kullanılıyor."
            };
        }

        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError
            {
                Code = "Kayıtlı Kullanıcı Adı",
                Description = $"{userName} kullanıcı adı zaten kullanılıyor."
            };
        }



        public override IdentityError PasswordRequiresUpper()
        {
            return new IdentityError
            {
                Code = "Büyük Harf Zorunluluğu",
                Description = "Şifrede en az bir büyük harf kullanılması zorunludur."
            };
        }
        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError
            {
                Code = "Şifre Uzunluğu",
                Description = $"Şifre en az {length} karakter uzunluğunda olmalıdır."
            };
        }
    }
}
