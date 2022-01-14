using VersionKeeper.Models;

namespace VersionKeeper.WPF.Messages
{
    public class SelectProjectMessage
    {
        public Project Project { get; }

        public SelectProjectMessage(Project project)
        {
            Project = project;
        }
    }
}