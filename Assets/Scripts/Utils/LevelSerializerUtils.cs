public static class LevelSerializerUtils
{
    public static IDataSerializer<LevelData> GetLevelDataSerializer(LevelSerializerType type)
    {
        switch (type)
        {
            case LevelSerializerType.Json:
                return new LevelSerializerJson();
            case LevelSerializerType.BinaryFormatter:
                return new LevelSerializerBinaryFormatter();
            default:
                return new LevelSerializerJson();
        }
    }
}
