namespace DaoThingi.SqlThingis
{
    class GrgParameter
    {
        public GrgParameter(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; }

        public object Value { get; }

        public override string ToString()
        {
            return $"GrgParameter  Name: {Name},   Value: {Value}  ";
        }
    }
}
