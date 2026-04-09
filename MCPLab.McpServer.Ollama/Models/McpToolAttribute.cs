namespace MCPLab.McpServer.Ollama.Models
{
    [AttributeUsage(AttributeTargets.Class)]
    public class McpToolAttribute : Attribute
    {
        public string Name { get; }

        public McpToolAttribute(string name)
        {
            Name = name;
        }
    }
}