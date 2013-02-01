using System;
using System.Text;
using System.Configuration;

namespace ConfigurationConsole
{
    // Before compiling this application, 
    // remember to reference the System.Configuration assembly in your project. 
    #region CustomSection class

    // Define a custom section. This class is used to
    // populate the configuration file.
    // It creates the following custom section:
    //  <CustomSection name="Contoso" url="http://www.contoso.com" port="8080" />.
    public sealed class CustomSection : ConfigurationSection
    {
        public CustomSection()
        {

        }


        [ConfigurationProperty("name",
            DefaultValue = "Contoso",
            IsRequired = true,
            IsKey = true)]
        public string Name
        {
            get
            {
                return (string)this["name"];
            }
            set
            {
                this["name"] = value;
            }
        }

        [ConfigurationProperty("url",
            DefaultValue = "http://www.contoso.com",
            IsRequired = true)]
        [RegexStringValidator(@"\w+:\/\/[\w.]+\S*")]
        public string Url
        {
            get
            {
                return (string)this["url"];
            }
            set
            {
                this["url"] = value;
            }
        }

        [ConfigurationProperty("port",
            DefaultValue = (int)8080,
            IsRequired = false)]
        [IntegerValidator(MinValue = 0,
            MaxValue = 8080, ExcludeRange = false)]
        public int Port
        {
            get
            {
                return (int)this["port"];
            }
            set
            {
                this["port"] = value;
            }
        }
    }

    #endregion

   
}