using Ch120.Dao;

namespace Ch120.Demo
{
    public class DemoMainDao : BaseDao<DemoMainModel>
    {
        // Jsonファイルのあるパス名を返す
        protected override string GetJsonPath()
        {
            return DemoResourceConst.JSON_PATH;
        }

        // JSONファイル名を返す
        protected override string GetJsonFile()
        {
            return DemoResourceConst.DEMO_MAIN_JSON;
        }
    }
}