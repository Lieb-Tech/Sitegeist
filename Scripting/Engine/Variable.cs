namespace Sitegeist.Scripting.Engine
{
    public class Variable
    {
        string name = string.Empty;
        public string Name { 
            get => name; 
            set
            {
                value = value.Trim();
                if (!value.StartsWith("{"))
                    value = "{" + value;

                if (!value.EndsWith("}"))
                    value += "}";

                name = value;
            }
        }
        public string Value { get; set; }
        public Variable()
        {

        }
        
    }
}
