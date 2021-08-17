﻿using SaintSender.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SaintSender.Core.Services
{
    public class InboxService : IInbox
    {
        private List<Mail> SortMail(List<Mail> mails)
        {
            return mails.OrderByDescending(mail => mail.Date).ToList() as List<Mail>;
        }

        public List<Mail> CreateMails(List<MimeKit.MimeMessage> inbox)
        {
            List<Mail> mails = new List<Mail>();
            foreach (var item in inbox)
            {
                mails.Add(new Mail()
                {
                    Subject = item.Subject,
                    From = item.From.ToString(),
                    Date = item.Date.DateTime,
                });
            }

            return SortMail(mails);
        }
    }
}
