using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace R03PointCardApp.Identity
{
    public interface IMicrosoftAuthService
    {
        void Initialize();
        Task<User> OnSignInAsync();
        Task OnSignOutAsync();
    }
}
