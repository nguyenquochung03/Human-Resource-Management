using HumanResourceManagement.Models;

namespace HumanResourceManagement.Pattern.Visitor
{
    public interface INhanVienVisitor
    {
        Task<bool> Visit(INhanVienVisitable visitable);
    }
}
