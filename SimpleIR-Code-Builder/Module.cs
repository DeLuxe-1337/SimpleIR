using System;
using System.IO;

namespace SimpleIR_Code_Builder
{
    internal class Module
    {
        public Builder builder = new Builder();
        public string Name;

        public Module(string name)
        {
            Name = name;
            builder.CreateVariable("module", Name);
        }

        public void Build()
        {
            builder.build.AppendLine("//IR Code Generation.\n");

            foreach (var builderType in builder.types) builder.build.AppendLine(builderType.ToString());

            File.WriteAllText("source.ir", builder.build.ToString());

            Console.WriteLine(builder.build.ToString());
        }
    }
}