using System.Collections.Generic;
using Harpoon.Core.Entities;

namespace Harpoon.Application.ViewModels
{
    public interface ICommentListModel
    {
        IEnumerable<Comment> Comments { get; }
        bool IsEditMode { get; }
        string MasterName { get; }
        bool HasComments { get; }
        int CommentCount { get; }
        string ArticleTitle { get; }
    }
}