namespace CourseWorkDB_DudasVI.General
{
    /// <summary>
    /// For cloning entities
    /// </summary>
    public static class ExtensionMethod
    {
        public static T Clone<T>(this T source)
        {
            var obj = new System.Runtime.Serialization.DataContractSerializer(typeof(T));
            using (var stream = new System.IO.MemoryStream())
            {
                obj.WriteObject(stream, source);
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return (T)obj.ReadObject(stream);
            }
        }
    }
}