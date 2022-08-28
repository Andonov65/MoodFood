using Domasna.Domain.DomainModels;
using Domasna.Repository.Interface;
using Domasna.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domasna.Services.Implementation
{
    public class BackgroundEmailSender : IBackgroundEmailSender
    {
        private readonly IEmailService _emailService;
        private readonly IRepository<EmailMessage> _emailRepository;
        
        public BackgroundEmailSender(IEmailService emailService, IRepository<EmailMessage> emailRepository)
        {
            _emailRepository = emailRepository;
            _emailService = emailService;
        }
        
        public async Task DoWork()
        {
            await _emailService.SendEmailAsync(_emailRepository.GetAll().Where(z => !z.Status).ToList());
        }
    }
}
