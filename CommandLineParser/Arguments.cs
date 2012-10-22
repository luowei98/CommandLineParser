namespace RobertLw.Tools.CommandLineParser
{
    [Description("testestesteste\n\r\tfadafdaf")]
    public class Arguments
    {
        [Description("The template solution is zip file.")]
        [Option]
        public bool IsZip { get; set; }

        [Required]
        [Description("The directory or zip file which include template solution.")]
        [Paramater]
        public string Source { get; set; }

        [Description("The directory which put new solution. Default is current directory.")]
        [Paramater]
        public string Destination { get; set; }
    }

}