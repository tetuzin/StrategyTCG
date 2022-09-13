using Ch120.Manager.Master;

namespace Ch120.Demo
{
    public class DemoMasterManager : MasterManager<DemoMasterManager>
    {
        // Daoクラスの名前リスト（新しく作成したDaoクラスはここに記述する）
        protected static readonly string[] DAO_CLASS_NAME = {
            "DemoMainDao",
        };
        
        // Daoクラスの名前空間
        protected readonly string DAO_CLASS_NAMESPACE = "Ch120.Demo.";
        
        // DAOクラス名の配列を返す
        protected override string[] GetDaoClassNameList()
        {
            return DAO_CLASS_NAME;
        }
        
        // DAOクラスの名前空間を返す
        protected override string GetDaoClassNamespace()
        {
            return DAO_CLASS_NAMESPACE;
        }
    }
}