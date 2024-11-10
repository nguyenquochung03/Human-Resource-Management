using HumanResourceManagement.Response;

namespace HumanResourceManagement.Pattern.Command
{
    public class CrudControl
    {
        private static CrudControl instance;
        private ICrudCommand command;


        private CrudControl() { }

        public static CrudControl getInstance()
        {
            if (instance == null)
            {
                instance = new CrudControl();
            }
            return instance;
        }

        public void setCommand(ICrudCommand command)
        {
            this.command = command;
        }

        public Task<ApiResponse> ExecuteCmd()
        {
            return command.Execute();
        }

    }
}
