using Harpoon.Core;
using Harpoon.Core.Entities;

namespace Harpoon.Application.ViewModels
{
    public class CommentModel
    {
        public const string MASTER_NAME_WHEN_EDIT_MODE = "Вы";

        public bool IsEditMode { get; private set; }
        public string MasterName { get; private set; }
        public Comment Comment { get; private set; }

        public void Init(Comment comment, bool isEditMode, string masterName)
        {
            ArgumentHelper.EnsureNotNull("comment", comment);
            ArgumentHelper.EnsureNotNull("isEditMode", isEditMode);
            ArgumentHelper.EnsureNotNullOrEmpty("masterName", masterName);

            Comment = comment;
            IsEditMode = isEditMode;
            MasterName = masterName;
        }

        public static CommentModel CreateNotEditable(Comment comment)
        {
            return new CommentModel
            {
                Comment = comment,
                IsEditMode = false
            };
        }

        public static CommentModel CreateEditable(Comment comment)
        {
            return new CommentModel
            {
                Comment = comment,
                IsEditMode = true,
                MasterName = MASTER_NAME_WHEN_EDIT_MODE
            };
        }

    }
}