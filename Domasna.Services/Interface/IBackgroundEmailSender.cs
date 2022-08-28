using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domasna.Services.Interface
{
    public interface IBackgroundEmailSender
    {
        Task DoWork();
    }
}
