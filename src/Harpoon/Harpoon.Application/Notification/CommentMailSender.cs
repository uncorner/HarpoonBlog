using System;
using System.Linq;
using System.Threading;
using Harpoon.Core;
using Harpoon.Core.Entities;
using Harpoon.Core.Repositories;
using NLog;

namespace Harpoon.Application.Notification
{
    public class CommentMailSender : ICommentNotificationSender
    {
        private static Logger log = LogManager.GetCurrentClassLogger();
        private readonly IUnitOfWorkFactory unitOfWorkFactory;
        private readonly IPersonalSettingRepository settingRepository;
        private readonly IArticleRepository articleRepository;

        public CommentMailSender(IUnitOfWorkFactory unitOfWorkFactory, IPersonalSettingRepository settingRepository,
                                 IArticleRepository articleRepository)
        {
            this.unitOfWorkFactory = unitOfWorkFactory;
            this.settingRepository = settingRepository;
            this.articleRepository = articleRepository;
        }

        public void Send(GuestComment comment, Article article, string articleUrl)
        {
            ArgumentHelper.EnsureNotNull("comment", comment);
            ArgumentHelper.EnsureNotNull("article", article);
            ArgumentHelper.EnsureNotNullOrEmpty("articleUrl", articleUrl);

            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                var setting = settingRepository.Fetch();
                var emails = articleRepository.FetchCommentatorEmails(article.Id, comment.Email);

                var targetEmails = emails.ToList();
                targetEmails.Insert(0, setting.Email);

                var content = string.Format(@"Пользователь {0} добавил комментарий к заметке <a href=""{3}"">«{2}»</a>:"
                                            + @"<br/><br/>{1}",
                                            comment.Name, comment.Content, article.Title, articleUrl);
                var mailer = SmtpMailerConfigurator.GetMailer();

                var mailerThread = new Thread(
                    () =>
                        {
                            try
                            {
                                mailer.Send(setting.Title, content, targetEmails);
                            }
                            catch (Exception ex)
                            {
                                log.Error("Не удалось выполнить email-рассылку: " + ex);
                            }
                        });

                mailerThread.Start();
            }
        }

    }
}