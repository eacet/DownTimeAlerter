using Microsoft.AspNetCore.Identity;
using System;

namespace DownTimeAlerter.Data.Domain.Entities {
    public class User : IdentityUser<Guid> {
        public override string Email { get => base.Email; set => base.Email = value; }
    }
}
