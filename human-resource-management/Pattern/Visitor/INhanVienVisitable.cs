namespace HumanResourceManagement.Pattern.Visitor
{
    public interface INhanVienVisitable
    {
        Task Accept(INhanVienVisitor visitor);
    }
}
