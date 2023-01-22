namespace Harpoon.Application.ErrorHandlers
{
    public interface IErrorHandler
    {
        void Handle(IErrorContext context);
    }
}