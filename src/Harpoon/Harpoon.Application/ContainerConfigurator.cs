using Harpoon.Application.Backend.Authentication;
using Harpoon.Application.Caching;
using Harpoon.Application.Notification;
using Harpoon.Application.Searching;
using Harpoon.Core;
using Harpoon.Core.Repositories;
using Harpoon.Infrastructure;
using Harpoon.Infrastructure.Repositories;
using Microsoft.Practices.Unity;

namespace Harpoon.Application
{
    public static class ContainerConfigurator
    {
        public static IUnityContainer GetContainer()
        {
            return new UnityContainer()
                .RegisterType<IUnitOfWorkFactory, UnitOfWorkFactory>()
                .RegisterType<IArticleRepository, ArticleRepository>()
                .RegisterType<IChapterRepository, ChapterRepository>()
                .RegisterType<IPersonalSettingRepository, PersonalSettingRepository>()
                .RegisterType<ITagRepository, TagRepository>()
                .RegisterType<IImageRepository, ImageRepository>()
                .RegisterType<IAuthProvider, FormAuthProvider>()
                .RegisterType<ICommentNotificationSender, CommentMailSender>()
                .RegisterType<ICacheBroker, CacheBroker>()
                .RegisterType<ISearchEngine, SearchEngine>();
        }
        
    }
}