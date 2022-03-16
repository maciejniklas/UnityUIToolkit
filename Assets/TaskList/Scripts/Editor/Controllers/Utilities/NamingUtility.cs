namespace TaskList.Controllers.Utilities
{
    public static class NamingUtility
    {
        public static string NameOf(string fieldName)
        {
            fieldName = fieldName.Replace("_", string.Empty);
            
            var fieldNameCharactersArray = fieldName.ToCharArray();
            fieldNameCharactersArray[0] = char.ToUpper(fieldNameCharactersArray[0]);

            return new string(fieldNameCharactersArray);
        }
    }
}