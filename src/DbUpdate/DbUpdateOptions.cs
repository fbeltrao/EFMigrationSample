using CommandLine;

namespace DbUpdate
{
    public class DbUpdateOptions
    {
        [Option('c', "connectionString", Required = true, HelpText = "Connection string to target database")]
        public string ConnectionString { get; set; }

    }
}